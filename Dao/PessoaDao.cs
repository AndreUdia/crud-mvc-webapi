using System;
using System.Data;
using System.Data.SqlClient;
using wsteste.Models;
using System.Collections.Generic;
using System.Linq;

namespace wsteste.Dao
{
    public class PessoaDao
    {
        string strConexao = "Server=localhost; Database=TRABFINAL; Trusted_Connection=True;";
        public List<Pessoa> GetListaDePessoas()
        {
            List<Pessoa> lista = new List<Pessoa>();

            try
            {
                using (SqlConnection conexao = new SqlConnection(strConexao))
                {
                    string consulta = "SELECT nome, cpf, idade FROM Pessoa";
                    SqlCommand comando =  new SqlCommand(consulta, conexao);
                    conexao.Open();
                    SqlDataReader leitor = comando.ExecuteReader();
                    while(leitor.Read())
                    {
                        lista.Add(new Pessoa(
                                    leitor["nome"].ToString(),
                                    leitor["cpf"].ToString(),
                                    (int)leitor["idade"]));
                    }
                    leitor.Close();
                }
                Console.WriteLine("Lista de pessoas obtidas com sucesso!");
            }
            catch
            {
                Console.WriteLine("Erro sql ao obter lista de pessoas!");
            }

            return lista;
        }
        public Pessoa GetPessoaPorCpf(string cpf)
        {
            return GetListaDePessoas().Where(p => p.cpf == cpf).FirstOrDefault();
        }
        public bool GetListaDePessoas(out List<Pessoa> lista)
        {
            lista = new List<Pessoa>();

            try
            {
                using (SqlConnection conexao = new SqlConnection(strConexao))
                {
                    string consulta = "SELECT nome, cpf, idade FROM Pessoa";
                    SqlCommand comando =  new SqlCommand(consulta, conexao);
                    conexao.Open();
                    SqlDataReader leitor = comando.ExecuteReader();
                    while(leitor.Read())
                    {
                        lista.Add(new Pessoa(
                                    leitor["nome"].ToString(),
                                    leitor["cpf"].ToString(),
                                    (int)leitor["idade"]));
                    }
                    leitor.Close();
                }
                Console.WriteLine("Lista de pessoas obtidas com sucesso!");
            }
            catch
            {
                Console.WriteLine("Erro sql ao obter lista de pessoas!");
                return false;
            }

            if(lista.Count > 0)
            {
                return true;
            }
            return false;
        }
        public bool CadastrarNovaPessoa(Pessoa pessoa, out string message)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(strConexao))
                {
                    string consulta = "INSERT INTO Pessoa VALUES(@nome, @cpf, @idade)";
                    SqlCommand comando =  new SqlCommand(consulta, conexao);
                    conexao.Open();
                    comando.Parameters.Add(new SqlParameter("@nome", pessoa.nome));
                    comando.Parameters.Add(new SqlParameter("@cpf", pessoa.cpf));
                    comando.Parameters.Add(new SqlParameter("@idade", pessoa.idade));
                    comando.ExecuteNonQuery();
                }
                message = "Pessoa cadastrada com sucesso";
            }
            catch
            {
                message = "Erro ao cadastrar pessoa";
                return false;
            }
            return true;
        }
        public bool ExcluirPessoaPorCpf(string cpf, out string message)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(strConexao))
                {
                    string consulta = "DELETE FROM Pessoa WHERE cpf = @cpf";
                    SqlCommand comando =  new SqlCommand(consulta, conexao);
                    conexao.Open();
                    comando.Parameters.Add(new SqlParameter("@cpf", cpf));
                    comando.ExecuteNonQuery();
                }
                message = "Pessoa excluída com sucesso";
            }
            catch
            {
                message = "Erro sql ao excluir pessoa";
                return false;
            }
            return true;
        }
        public bool AtualizaPessoa(Pessoa pessoa, out string message)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(strConexao))
                {
                    string consulta = "UPDATE Pessoa" + 
                                      " SET nome = @nome," +
                                      //"      cpf = @cpf," + por usar cpf como chave não aceito atualizar
                                      " idade = @idade" +
                                      " WHERE cpf = @cpf";
                    SqlCommand comando =  new SqlCommand(consulta, conexao);
                    Console.WriteLine(consulta);
                    conexao.Open();
                    comando.Parameters.Add(new SqlParameter("@nome", pessoa.nome));
                    comando.Parameters.Add(new SqlParameter("@cpf", pessoa.cpf));
                    comando.Parameters.Add(new SqlParameter("@idade", pessoa.idade));
                    comando.ExecuteNonQuery();
                }
                message = "Dados da pessoa alterado com sucesso!";
            }
            catch
            {
                message = "Não foi possível atualizar os dados da pessoa!";
                return false;
            }
            return true;
        }
    }
}