using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace CadeMeuMedico.Models
{
    public class Medico {
        ConnectionStringSettings getString = WebConfigurationManager.ConnectionStrings["ConexaoCadeMeuMedico"] as ConnectionStringSettings;

        [Key]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "ID")]
        public int IDMedico { get; set; }
        
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CRM { get; set; }
        
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(80, MinimumLength = 3)]
        public string Nome { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Atende Por Convênio?")]
        public bool AtendePorConvenio { get; set; }
        [Display(Name = "Tem Clínica?")]
        public bool TemClinica { get; set; }
        
        [DataType(DataType.Url)]
        [Display(Name = "Web Site")]
        public string WebSiteBlog { get; set; }
        [Display(Name = "ID da Cidade")]
        public int IDCidade { get; set; }
        [Display(Name = "ID da Especialidade")]
        public int IDEspecialidade { get; set; }

        public List<Medico> GetAllMedicos()
        {
            List<Medico> listaMedico = new List<Medico>();
            if (getString != null)
            {
                string sSql = "Select * from Medicos";
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlDataReader read = null;
                    SqlCommand cmd = new SqlCommand(sSql, conn);

                    try
                    {
                        conn.Open();
                        read = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (read.Read())
                        {
                            Medico medico = new Medico();
                            medico.IDMedico = Convert.ToInt16(read["IDMedico"]);
                            medico.CRM = read["CRM"].ToString();
                            medico.Nome = read["Nome"].ToString();
                            medico.Endereco = read["Endereco"].ToString();
                            medico.Bairro = read["Bairro"].ToString();
                            medico.Email = read["Email"].ToString();
                            medico.AtendePorConvenio = Convert.ToBoolean(read["AtendePorConvenio"]);
                            medico.TemClinica = Convert.ToBoolean(read["TemClinica"]);
                            medico.WebSiteBlog = read["WebSiteBlog"].ToString();
                            medico.IDCidade = Convert.ToInt16(read["IDCidade"]);
                            medico.IDEspecialidade = Convert.ToInt16(read["IDEspecialidade"]);
                            listaMedico.Add(medico);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }


                }
            }
            return listaMedico;
        }

        public Medico GetMedicosByID(int ID)
        {
            Medico medico = new Medico();
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlDataReader read = null;
                    SqlCommand cmd = new SqlCommand("SelecionarMedicoPorID", conn);
                    //SqlCommand cmd = new SqlCommand("Select * from Medicos where IDMedico =  " + ID, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDMedico", ID);
                    try
                    {
                        conn.Open();
                        read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        while (read.Read()) {
                            medico.IDMedico = Convert.ToInt16(read["IDMedico"]);
                            medico.CRM = read["CRM"].ToString();
                            medico.Nome = read["Nome"].ToString();
                            medico.Endereco = read["Endereco"].ToString();
                            medico.Bairro = read["Bairro"].ToString();
                            medico.Email = read["Email"].ToString();
                            medico.AtendePorConvenio = Convert.ToBoolean(read["AtendePorConvenio"]);
                            medico.TemClinica = Convert.ToBoolean(read["TemClinica"]);
                            medico.WebSiteBlog = read["WebSiteBlog"].ToString();
                            medico.IDCidade = Convert.ToInt16(read["IDCidade"]);
                            medico.IDEspecialidade = Convert.ToInt16(read["IDEspecialidade"]);
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Erro: " + ex.Message);
                    }
                }
            }
            return medico;
        }

        public List<Medico> GetAllMedicosByEspecialidade(int ID)
        {
            List<Medico> listaMedico = new List<Medico>();
            if (getString != null)
            {
                string sSql = "SELECT * FROM Medicos WHERE IDEspecialidade = " + ID;
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlDataReader read = null;
                    SqlCommand cmd = new SqlCommand(sSql, conn);

                    try
                    {
                        conn.Open();
                        read = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (read.Read())
                        {
                            Medico medico = new Medico();
                            medico.IDMedico = Convert.ToInt16(read["IDMedico"]);
                            medico.CRM = read["CRM"].ToString();
                            medico.Nome = read["Nome"].ToString();
                            medico.Endereco = read["Endereco"].ToString();
                            medico.Bairro = read["Bairro"].ToString();
                            medico.Email = read["Email"].ToString();
                            medico.AtendePorConvenio = Convert.ToBoolean(read["AtendePorConvenio"]);
                            medico.TemClinica = Convert.ToBoolean(read["TemClinica"]);
                            medico.WebSiteBlog = read["WebSiteBlog"].ToString();
                            medico.IDCidade = Convert.ToInt16(read["IDCidade"]);
                            medico.IDEspecialidade = Convert.ToInt16(read["IDEspecialidade"]);
                            listaMedico.Add(medico);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }


                }
            }
            return listaMedico;
        }

        public void AddMedico(Medico medico)
        {
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("InserirMedico", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CRM", medico.CRM);
                    cmd.Parameters.AddWithValue("@Nome", medico.Nome);
                    cmd.Parameters.AddWithValue("@Endereco", medico.Endereco);
                    cmd.Parameters.AddWithValue("@Bairro", medico.Bairro);
                    cmd.Parameters.AddWithValue("@Email", medico.Email);
                    cmd.Parameters.AddWithValue("@AtendePorConvenio", medico.AtendePorConvenio);
                    cmd.Parameters.AddWithValue("@TemClinica", medico.TemClinica);
                    cmd.Parameters.AddWithValue("@WebSiteBlog", medico.WebSiteBlog);
                    cmd.Parameters.AddWithValue("@IDCidade", medico.IDCidade);
                    cmd.Parameters.AddWithValue("@IDEspecialidade", medico.IDEspecialidade);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        /*public void AddMedico(Medico medico) {
            SqlConnection conn = new SqlConnection(getString.ConnectionString);
            try {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Medicos values (@CRM, @Nome, @Endereco, @Bairro, @Email, @AtendePorConvenio, @TemClinica, @WebSiteBlog, @IDCidade, @IDEspecialidade)", conn);

                cmd.Parameters.AddWithValue("@CRM", medico.CRM);
                cmd.Parameters.AddWithValue("@Nome", medico.Nome);
                cmd.Parameters.AddWithValue("@Endereco", medico.Endereco);
                cmd.Parameters.AddWithValue("@Bairro", medico.Bairro);
                cmd.Parameters.AddWithValue("@Email", medico.Email);
                cmd.Parameters.AddWithValue("@AtendePorConvenio", medico.AtendePorConvenio);
                cmd.Parameters.AddWithValue("@TemClinica", medico.TemClinica);
                cmd.Parameters.AddWithValue("@WebSiteBlog", medico.WebSiteBlog);
                cmd.Parameters.AddWithValue("@IDCidade", medico.IDCidade);
                cmd.Parameters.AddWithValue("@IDEspecialidade", medico.IDEspecialidade);
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            { conn.Close(); }
        }
        
         */

        public void UpdateMedico(Medico medico)
        {
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("AtualizarMedico", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDMedico", medico.IDMedico);
                    cmd.Parameters.AddWithValue("@CRM", medico.CRM);
                    cmd.Parameters.AddWithValue("@Nome", medico.Nome);
                    cmd.Parameters.AddWithValue("@Endereco", medico.Endereco);
                    cmd.Parameters.AddWithValue("@Bairro", medico.Bairro);
                    cmd.Parameters.AddWithValue("@Email", medico.Email);
                    cmd.Parameters.AddWithValue("@AtendePorConvenio", medico.AtendePorConvenio);
                    cmd.Parameters.AddWithValue("@TemClinica", medico.TemClinica);
                    cmd.Parameters.AddWithValue("@WebSiteBlog", medico.WebSiteBlog);
                    cmd.Parameters.AddWithValue("@IDCidade", medico.IDCidade);
                    cmd.Parameters.AddWithValue("@IDEspecialidade", medico.IDEspecialidade);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void DeleteMedicosByID(int ID)
        {
            Medico medico = new Medico();
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ExcluirMedicoPorID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDMedico", ID);
                    try {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Erro: " + ex.Message);
                    }
                }
            }
        }
    }
}
