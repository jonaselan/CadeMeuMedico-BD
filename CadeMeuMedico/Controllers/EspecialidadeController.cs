using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CadeMeuMedico.Models;

namespace CadeMeuMedico.Controllers
{
    public class EspecialidadeController : Controller
    {
        private Especialidade especialidade = new Especialidade();

        // GET: Especialidade
        public ActionResult Index()
        {
            var model = especialidade.GetAllEspecialidades();
            if (model == null)
                return View("NotFound");
            else
                return View(model);
        }

        // GET: Especialidade/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Especialidade/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Especialidade/Create
        [HttpPost]
        public ActionResult Create(Especialidade especialidade)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                especialidade.AddEspecialidade(especialidade);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Especialidade/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (id.ToString() == null) return View("NotFound");
                var model = especialidade.GetEspecialidadesByID(id);
                if (model == null)
                    return View("NotFound");
                else
                    return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
        }

        // POST: Especialidade/Edit/5
        [HttpPost]
        public ActionResult Edit(Especialidade Eespecialidade)
        {
            try
            {
                if (!ModelState.IsValid) return View("NotFount");
                //Chama o método da classe Especialidades que atualizar o registro no banco de dados especialidade.UpdateEspecialidade(pespecialidade);
                especialidade.UpdateEspecialidade(Eespecialidade);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Especialidade/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (id.ToString() == null) return View("NotFound");
                var model = especialidade.GetEspecialidadesByID(id);
                if (model == null)
                    return View("NotFound");
                else
                    return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
        }

        // POST: Especialidade/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (id.ToString() == null) return View("NotFound");
                var model = especialidade.GetEspecialidadesByID(id);
                if (model == null)
                    return View("NotFound");
                else
                    model.DeleteEspecialidadesByID(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
        }

    }
}