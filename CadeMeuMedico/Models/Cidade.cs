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
    public class Cidade
    {
        ConnectionStringSettings getString = WebConfigurationManager.ConnectionStrings["ConexaoCadeMeuMedico"] as ConnectionStringSettings;

        public int IDCidade { get; set; }
        public string Nome { get; set; }

        public List<Cidade> GetAllCidade()
        {
            List<Cidade> listaCidade = new List<Cidade>();
            if (getString != null)
            {
                string sSql = "SELECT * FROM Cidades";
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
                            Cidade cidade = new Cidade();
                            cidade.IDCidade = Convert.ToInt16(read["IDCidade"]);
                            cidade.Nome = read["Nome"].ToString();
                            listaCidade.Add(cidade);
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
            return listaCidade;
        }

        public Cidade GetCidadesByID(int ID)
        {
            Cidade cidade = new Cidade();
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlDataReader read = null;
                    SqlCommand cmd = new SqlCommand("SelecionarCidadePorID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDCidade", ID);
                    try
                    {
                        conn.Open();
                        read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        while (read.Read())
                        {
                            cidade.IDCidade = Convert.ToInt16(read["IDCidade"]);
                            cidade.Nome = read["Nome"].ToString();
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Erro: " + ex.Message);
                    }
                }
            }
            return cidade;
        }

        public void AddCidade(Cidade cidade)
        {
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("InserirCidade", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nome", cidade.Nome);
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

        public void UpdateCidade(Cidade cidade)
        {
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("AtualizarCidade", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDCidade", cidade.IDCidade);
                    cmd.Parameters.AddWithValue("@Nome", cidade.Nome);
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

        public void DeleteCidadesByID(int ID)
        {
            Cidade cidade = new Cidade();
            if (getString != null)
            {
                using (SqlConnection conn = new SqlConnection(getString.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("ExcluirCidadePorID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDCidade", ID);
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