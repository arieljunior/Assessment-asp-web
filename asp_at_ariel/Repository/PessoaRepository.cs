using asp_at_ariel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace asp_at_ariel.Repository
{
    public class PessoaRepository
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ariel\Desktop\teste-de-performance-3-asp.net\asp_tp3_ariel\App_Data\Pessoa_db.mdf;Integrated Security=True";

        public List<PessoaModel> GetAllPessoas()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var commandText = "SELECT * FROM Pessoa";
                var selectCommand = new SqlCommand(commandText, connection);
                //int teste = connection.ConnectionTimeout;
   
                PessoaModel pessoa = null;
                var pessoas = new List<PessoaModel>();
                try
                {
                    connection.Open();
                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            pessoa = new PessoaModel();
                            pessoa.Id = (int)reader["Id"];
                            pessoa.Nome = reader["Nome"].ToString();
                            pessoa.Sobrenome = reader["Sobrenome"].ToString();
                            pessoa.DataNascimento = Convert.ToDateTime(reader["Nascimento"]);
                            pessoas.Add(pessoa);
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
                return pessoas;
            }
        }

        public void salvarPessoa(PessoaModel pessoa)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Pessoa (Nome,Sobrenome,Nascimento) VALUES (@nome, @sobrenome, @nascimento)";
                var cmd = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@sobrenome", pessoa.Sobrenome);
                    cmd.Parameters.AddWithValue("@nascimento", pessoa.DataNascimento);
                    cmd.ExecuteNonQuery();
                }catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public void DeletarPessoa(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Pessoa WHERE  Id = @pessoaId";
                var cmd = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@pessoaId", id);
                    cmd.ExecuteNonQuery();
                }catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool AtualizarPessoa(PessoaModel p)
        {
            bool isSucess = false;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Pessoa SET Nome = @nome, Sobrenome = @sobrenome, Nascimento = @dataNasc WHERE Id = @id";
                var cmd = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id", p.Id);
                    cmd.Parameters.AddWithValue("@nome", p.Nome);
                    cmd.Parameters.AddWithValue("@sobrenome", p.Sobrenome);
                    cmd.Parameters.AddWithValue("@dataNasc", p.DataNascimento);
                    cmd.ExecuteNonQuery();
                    isSucess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return isSucess;
        }

        public PessoaModel ProcurarPessoa(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var commandText = $"SELECT * FROM Pessoa WHERE Id = {id}";
                var selectCommand = new SqlCommand(commandText, connection);
                PessoaModel pessoa = null;
                try
                {
                    connection.Open();
                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            pessoa = new PessoaModel();
                            pessoa.Id = (int)reader["Id"];
                            pessoa.Nome = reader["Nome"].ToString();
                            pessoa.Sobrenome = reader["Sobrenome"].ToString();
                            pessoa.DataNascimento = Convert.ToDateTime(reader["Nascimento"]);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
                return pessoa;
            }
        }

        public List<PessoaModel> GetPessoasPorNome(string nome)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var commandText = $"SELECT * FROM Pessoa WHERE Nome LIKE '{nome.ToLower()}'";
                var selectCommand = new SqlCommand(commandText, connection);
                PessoaModel pessoa = null;
                var pessoas = new List<PessoaModel>();
                try
                {
                    connection.Open();
                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            pessoa = new PessoaModel();
                            pessoa.Id = (int)reader["Id"];
                            pessoa.Nome = reader["Nome"].ToString();
                            pessoa.Sobrenome = reader["Sobrenome"].ToString();
                            pessoa.DataNascimento = Convert.ToDateTime(reader["Nascimento"]);
                            pessoas.Add(pessoa);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
                return pessoas;
            }
        }

        public  List<PessoaModel> GetAniversariantes()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var commandText = "SELECT * FROM Pessoa";
                var selectCommand = new SqlCommand(commandText, connection);
                PessoaModel pessoa = null;
                var pessoas = new List<PessoaModel>();
                DateTime DataAtual = DateTime.Today;
                try
                {
                    connection.Open();
                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            if(Convert.ToDateTime(reader["Nascimento"]).Month == DataAtual.Month)
                            {
                                if (Convert.ToDateTime(reader["Nascimento"]).Day == DataAtual.Day)
                                {
                                    pessoa = new PessoaModel();
                                    pessoa.Id = (int)reader["Id"];
                                    pessoa.Nome = reader["Nome"].ToString();
                                    pessoa.Sobrenome = reader["Sobrenome"].ToString();
                                    pessoa.DataNascimento = Convert.ToDateTime(reader["Nascimento"]);
                                    pessoas.Add(pessoa);
                                }
                            }
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
                return pessoas;
            }
        }

    }
}