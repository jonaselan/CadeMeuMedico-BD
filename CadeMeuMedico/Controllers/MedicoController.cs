using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CadeMeuMedico.Models;

namespace CadeMeuMedico.Controllers
{
    public class MedicoController : Controller
    {
        private Medico medico = new Medico();
        
        // GET: Medico
        public ActionResult Index()
        {
            var model = medico.GetAllMedicos();
            if (model == null)
                return View("NotFound");
            else
                return View(model);
        }

        // GET: Medico/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        // GET: Medico/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Medico/Create
        [HttpPost]
        public ActionResult Create(Medico medico)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                medico.AddMedico(medico);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Medico/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (id.ToString() == null) return View("NotFound");
                var model = medico.GetMedicosByID(id);
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
        
        // POST: Medico/Edit/5
        [HttpPost]
        public ActionResult Edit(Medico Emedico)
        {
             try
             {
                 if (!ModelState.IsValid) return View("NotFount");
                 //Chama o método da classe Medicos que atualizar o registro no banco de dados medico.UpdateMedico(pmedico);
                 medico.UpdateMedico(Emedico);
                 return RedirectToAction("Index");
             }
             catch
             {
                return View();
             }
         }
        
        // GET: Medico/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (id.ToString() == null) return View("NotFound");
                var model = medico.GetMedicosByID(id);
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
        
        // POST: Medico/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (id.ToString() == null) return View("NotFound");
                var model = medico.GetMedicosByID(id);
                if (model == null)
                    return View("NotFound");
                else
                    model.DeleteMedicosByID(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
        }
   
    }
}