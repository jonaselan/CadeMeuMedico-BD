using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CadeMeuMedico.Models
{
    public class Especialidade {
        ConnectionStringSettings getString = WebConfigurationManager.ConnectionStrings["ConexaoCadeMeuMedico"] as ConnectionStringSettings;

        public int IDEspecialidade { get; set; }
        public string Nome { get; set; }

        public List<Especialidade> GetAllEspecialidades()
        {
            List <Especialidade> listaEspecialidade = new List<Especialidade>();
            if (getString != null)
            {
                string sSql = "Select * from Especialidades";
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
                            Especialidade especialidade = new Especialidade();
                            especialidade.IDEspecialidade = Convert.ToInt16(read["IDEspecialidade"]);
                            especialidade.Nome = read["Nome"].ToString();
                            listaEspecialidade.Add(especialidade);
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
            return listaEspecialidade;
        }

        public Especialidade GetEspecialidadesByID(int ID)
        {
            Especialidade especialidade = new Especialidade();
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlDataReader read = null;
                    SqlCommand cmd = new SqlCommand("SelecionarEspecialidadePorID", conn);
                    //SqlCommand cmd = new SqlCommand("Select * from Medicos where IDMedico =  " + ID, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDEspecialidade", ID);
                    try
                    {
                        conn.Open();
                        read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        while (read.Read())
                        {
                            especialidade.IDEspecialidade = Convert.ToInt16(read["IDEspecialidade"]);
                            especialidade.Nome = read["Nome"].ToString();
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Erro: " + ex.Message);
                    }
                }
            }
            return especialidade;
        }

        public void AddEspecialidade(Especialidade especialidade)
        {
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("InserirEspecialidade", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nome", especialidade.Nome);
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

        public void UpdateEspecialidade(Especialidade especialidade)
        {
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("AtualizarEspecialidade", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDEspecialidade", especialidade.IDEspecialidade);
                    cmd.Parameters.AddWithValue("@Nome", especialidade.Nome);
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

        public void DeleteEspecialidadesByID(int ID)
        {
            Especialidade especialidade = new Especialidade();
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ExcluirEspecialidadePorID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDEspecialidade", ID);
                    try
                    {
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