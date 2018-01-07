using asp_at_ariel.Domain;
using asp_at_ariel.Models;
using asp_at_ariel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_at_ariel.Controllers
{
    public class PessoaController : Controller
    {
        DataAcess database = new DataAcess();

        public ActionResult Index()
        {
            return View(database.getListaPessoas());
        }
        
        public ActionResult TodosAmigos()
        {
            return View(database.getListaPessoas());
        }

        public ActionResult Details(int id)
        {
            PessoaModel PessoaEncontrada = database.BuscarPessoa(id);
            return View(PessoaEncontrada);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PessoaModel p)
        {
            if(database.adicionarPessoa(p))
            {

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(int id)
        {
            PessoaModel PessoaEncontrada = database.BuscarPessoa(id);
            return View(PessoaEncontrada);
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        public ActionResult Edit(PessoaModel pessoa)
        { 
            if(database.AtualizarPessoa(pessoa))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(int id)
        {
            PessoaModel pessoaEncontrada = database.BuscarPessoa(id);
            return View(pessoaEncontrada);
        }

        // POST: Pessoa/Delete/5
        [HttpPost]
        public ActionResult Delete(PessoaModel pessoa)
        {
            try
            {
                database.RemoverPessoa(pessoa);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BuscarPessoa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuscarPessoa(string nome)
        {
            try
            {
                return View(database.BuscarNome(nome));
            }catch(Exception ex)
            {
                return View();
            }
        }
    }
}
