using asp_at_ariel.Models;
using asp_at_ariel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_at_ariel.Domain
{
    public class DataAcess
    {
        private static List<PessoaModel> ListaPessoas = new List<PessoaModel>();
        private PessoaRepository repositorio = new PessoaRepository();

        public bool adicionarPessoa(PessoaModel p)
        {
            if((p.Nome != "") && (p.DataNascimento != null && p.DataNascimento != DateTime.Parse("01/01/00001") && p.DataNascimento != DateTime.Parse("31/12/9999") ))
            {
                PessoaModel pessoa = new PessoaModel()
                {
                    Nome = p.Nome,
                    Sobrenome = p.Sobrenome,
                    DataNascimento = p.DataNascimento
                };

                repositorio.salvarPessoa(pessoa);
            }
            else
            {
                return false;
            }

            return true;
        }

        public List<PessoaModel> getListaPessoas()
        {
            return repositorio.GetAllPessoas();
        }

        public PessoaModel BuscarPessoa(int id) 
        {
            return repositorio.ProcurarPessoa(id);
        }

        public bool AtualizarPessoa(PessoaModel pessoa)
        {
            return repositorio.AtualizarPessoa(pessoa);
        }

        public void RemoverPessoa(PessoaModel pessoa)
        {
            repositorio.DeletarPessoa(pessoa.Id);
        }

        public List<PessoaModel> BuscarNome(string nome)
        {
            return repositorio.GetPessoasPorNome(nome);
        }

        public List<PessoaModel> GetAniversariantes()
        {
            return repositorio.GetAniversariantes();
        }
    }
}