using CadeMeuMedico.Models.Conta;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace CadeMeuMedico.Membership
{
    public sealed class CadeMeuMedicoMembershipProvider : MembershipProvider
    {
        private int newPasswordLength = 8;  
        private string connectionString;  
        private string applicationName;  
        private bool enablePasswordReset;  
        private bool enablePasswordRetrieval;  
        private bool requiresQuestionAndAnswer;  
        private bool requiresUniqueEmail;  
        private int maxInvalidPasswordAttempts;  
        private int passwordAttemptWindow;  
        private MembershipPasswordFormat passwordFormat;  
        private int minRequiredNonAlphanumericCharacters;  
        private int minRequiredPasswordLength;  
        private string passwordStrengthRegularExpression;  
        private MachineKeySection machineKey;

        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }

            set
            {
                applicationName = value;
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                return enablePasswordReset;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return enablePasswordRetrieval;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return maxInvalidPasswordAttempts;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return minRequiredNonAlphanumericCharacters;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return minRequiredPasswordLength;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return passwordAttemptWindow;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return passwordFormat;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return passwordStrengthRegularExpression;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return requiresQuestionAndAnswer;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return requiresUniqueEmail;
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                string configPath = "~/web.config";
                Configuration NexConfig = WebConfigurationManager.OpenWebConfiguration(configPath);
                MembershipSection section = (MembershipSection)NexConfig.GetSection("system.web/membership");
                ProviderSettingsCollection settings = section.Providers;
                NameValueCollection membershipParams = settings[section.DefaultProvider].Parameters;
                config = membershipParams;
            }

            if (name == null || name.Length == 0)
            {
                name = "CadeMeuMedicoMembershipProvider";
            }

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Cadê Meu Médico Membership Provider");
            }

            base.Initialize(name, config);
            applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredAlphaNumericCharacters"], "1"));
            minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], String.Empty));
            enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));

            string temp_format = config["passwordFormat"];
            if (temp_format == null)
            {
                temp_format = "Hashed";
            }

            switch (temp_format)
            {
                case "Hashed":
                    passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Formato de senha não suportado.");
            }
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if ((connectionStringSettings == null) || (connectionStringSettings.ConnectionString.Trim() == String.Empty))
            {
                throw new ProviderException("Connection String não pode estar vázia");
            }

            connectionString = connectionStringSettings.ConnectionString;
            System.Configuration.Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            machineKey = cfg.GetSection("system.web/machineKey") as MachineKeySection;

            if (machineKey.ValidationKey.Contains("AutoGenerate"))
            {
                if (PasswordFormat != MembershipPasswordFormat.Clear)
                {
                    throw new ProviderException("Senhas Hashed ou Encrypted não são suportadas com chaves auto geradas.");
                }
            }
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if(String.IsNullOrEmpty(configValue))
            {
                return defaultValue;
            }

            return configValue;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword)) return false;

            if (oldPassword == newPassword) return false;

            CadeMeuMedicoMembershipUser user = GetUser(username);

            if (user == null) return false;

            ContextoUsuario db = new ContextoUsuario();
            var RawUser = (from u in db.Usuarios
                           where u.Login == user.UserName
                           select u).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(RawUser.Senha)) return false;

            RawUser.Senha = EncodePassword(newPassword);

            db.SaveChanges();

            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public CadeMeuMedicoMembershipUser CreateUser(
                string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status,
                string nome
            )
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if ((RequiresUniqueEmail && (GetUserNameByEmail(email) != String.Empty)))
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            CadeMeuMedicoMembershipUser cadeMeuMedicoMembershipUser = GetUser(username);

            if (cadeMeuMedicoMembershipUser == null)
            {
                try
                {
                    using (ContextoUsuario _db = new ContextoUsuario())
                    {
                        Usuario user = new Usuario();
                        user.Nome = nome;
                        user.Login = username;
                        user.Senha = EncodePassword(password);
                        user.Email = email.ToLower();

                        _db.Usuarios.Add(user);

                        _db.SaveChanges();

                        status = MembershipCreateStatus.Success;
                        return GetUser(username);
                    }
                }
                catch
                {
                    status = MembershipCreateStatus.ProviderError;
                }
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if ((RequiresUniqueEmail && (GetUserNameByEmail(email) != String.Empty)))
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser membershipUser = GetUser(username, false);

            if (membershipUser == null)
            {
                try
                {
                    using (ContextoUsuario _db = new ContextoUsuario())
                    {
                        Usuario user = new Usuario();
                        user.Nome = "";
                        user.Login = username;
                        user.Senha = EncodePassword(password);
                        user.Email = email.ToLower();

                        _db.Usuarios.Add(user);

                        _db.SaveChanges();

                        status = MembershipCreateStatus.Success;

                        return GetUser(username, false);
                    }
                }
                catch
                {
                    status = MembershipCreateStatus.ProviderError;
                }
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            bool ret = false;

            using (ContextoUsuario _db = new ContextoUsuario())
            {
                try
                {
                    Usuario user = (from u in _db.Usuarios
                                    where u.Login == username
                                    select u).FirstOrDefault();

                    if (user != null)
                    {
                        _db.Usuarios.Remove(user);
                        _db.SaveChanges();
                        ret = true;
                    }
                }
                catch
                {
                    ret = false;
                }
            }

            return ret;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            using (ContextoUsuario _db = new ContextoUsuario())
            {
                try
                {
                    var pass = (from p in _db.Usuarios
                                where p.Login == username
                                select p.Senha).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(pass))
                        return UnEncodePassword(pass);
                }
                catch { }
            }

            return null;
        }

        private bool CheckPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);
                    break;
                default:
                    break;
            }

            if (pass1 == pass2) { return true; }

            return false;
        }

        private string UnEncodePassword(string encodedPassword)
        {
            string password = encodedPassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password = Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Senha no formato (HMACSHA1) não implementado");
                default:
                    throw new ProviderException("Formato de senha não suportado.");
            }

            return password;
        }

        private string EncodePassword(string password)
        {
            string encodedPassword = password;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    byte[] encryptedPass = EncryptPassword(Encoding.Unicode.GetBytes(password));
                    encodedPassword = Convert.ToBase64String(encryptedPass);
                    break;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1();
                    hash.Key = HexToByte(machineKey.ValidationKey);
                    encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException("Formato de senha não suportado.");
            }

            return encodedPassword;
        }

        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return returnBytes;
        }

        public CadeMeuMedicoMembershipUser GetUser(string username)
        {
            CadeMeuMedicoMembershipUser cadeMeuMedicoMembershipUser = null;
            using (ContextoUsuario _db = new ContextoUsuario())
            {
                try
                {
                    Usuario user = (from u in _db.Usuarios
                                    where u.Login == username
                                    select u).FirstOrDefault();

                    if (user != null)
                    {
                        cadeMeuMedicoMembershipUser = new CadeMeuMedicoMembershipUser(
                            this.Name,
                            user.Login,
                            null,
                            user.Email,
                            "",
                            "",
                            true,
                            false,
                            default(DateTime),
                            DateTime.Now,
                            DateTime.Now,
                            default(DateTime),
                            default(DateTime),
                            user.Nome);
                    }
                }
                catch{}
            }

            return cadeMeuMedicoMembershipUser;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser membershipUser = null;
            using (ContextoUsuario _db = new ContextoUsuario())
            {
                try
                {
                    Usuario user = (from u in _db.Usuarios
                                    where u.Login == username
                                    select u).FirstOrDefault();
                    if (user != null)
                    {
                        membershipUser = new MembershipUser(
                            this.Name,
                            user.Login,
                            null,
                            user.Email,
                            "",
                            "",
                            true,
                            false,
                            default(DateTime),
                            DateTime.Now,
                            DateTime.Now,
                            default(DateTime),
                            default(DateTime));
                    }
                }
                catch{}
            }

            return membershipUser;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            string username = null;
            using (ContextoUsuario _db = new ContextoUsuario())
            {
                try
                {
                    Usuario user = (from u in _db.Usuarios
                                    where u.Email == email
                                    select u).FirstOrDefault();

                    if (user != null)
                    {
                        username = user.Login;
                    }
                }
                catch { }
            }

            return username;
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            using (ContextoUsuario _db = new ContextoUsuario())
            {
                try
                {
                    Usuario userToEdit = (from u in _db.Usuarios
                                    where u.Login == user.UserName
                                    select u).FirstOrDefault();

                    if (userToEdit != null)
                    {}
                }
                catch
                {
                }
            }
        }

        public void UpdateCadeMeuMedicoUser(CadeMeuMedicoMembershipUser user)
        {
            using (ContextoUsuario _db = new ContextoUsuario())
            {
                try
                {
                    Usuario userToEdit = (from u in _db.Usuarios
                                          where u.Login == user.UserName
                                          select u).FirstOrDefault();

                    if (userToEdit != null)
                    {
                        userToEdit.Nome = user.Nome;
                        userToEdit.Email = user.Email;
                        _db.SaveChanges();
                    }
                }
                catch
                {
                }
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;

            using (ContextoUsuario _db = new ContextoUsuario())
            {
                try
                {
                    Usuario user = (from u in _db.Usuarios
                                    where u.Login == username
                                    select u).FirstOrDefault();

                    if (user != null)
                    {
                        string storedPassword = user.Senha;
                        if (CheckPassword(password, storedPassword))
                        {
                            isValid = true;
                        }
                    }
                }
                catch
                {
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}