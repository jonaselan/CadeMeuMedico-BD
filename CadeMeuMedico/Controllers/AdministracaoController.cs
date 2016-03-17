using CadeMeuMedico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadeMeuMedico.Controllers
{
    [Authorize]
    public class AdministracaoController : Controller
    {
        Medico medico = new Medico();
        Especialidade especiliadade = new Especialidade();
        Cidade cidade = new Cidade();
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Medicos()
        {
            if (Request.IsAuthenticated)
            {
                IEnumerable<Medico> model = medico.GetAllMedicos();
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Especialidades()
        {
            if (Request.IsAuthenticated)
            {
                IEnumerable<Especialidade> model = especiliadade.GetAllEspecialidades();
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult CriarEspecialidade()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult CriarEspecialidade(Especialidade especialidade)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (!ModelState.IsValid) return View();
                especiliadade.AddEspecialidade(especialidade);
                return RedirectToAction("Especialidades");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AtualizarEspecialidade(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (id.ToString() == null) return View("NotFound");
                Especialidade model = especiliadade.GetEspecialidadesByID(id);
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

        [HttpPost]
        public ActionResult AtualizarEspecialidade(Especialidade Eespecialidade)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (!ModelState.IsValid) return View("NotFound");
                especiliadade.UpdateEspecialidade(Eespecialidade);
                return RedirectToAction("Especialidades");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeletarEspecialidade(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (id.ToString() == null) return View("NotFound");
                var model = especiliadade.GetEspecialidadesByID(id);
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

        [HttpPost]
        public ActionResult DeletarEspecialidade(int id, FormCollection collection)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (id.ToString() == null) return View("NotFound");
                var model = especiliadade.GetEspecialidadesByID(id);
                if (model == null)
                    return View("NotFound");
                else
                    model.DeleteEspecialidadesByID(id);
                return RedirectToAction("Especialidades");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
        }

        public ActionResult Cidades()
        {
            if (Request.IsAuthenticated)
            {
                IEnumerable<Cidade> model = cidade.GetAllCidade();
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult CriarCidade()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult CriarCidade(Cidade cidade)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (!ModelState.IsValid) return View();
                cidade.AddCidade(cidade);
                return RedirectToAction("Cidades");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AtualizarCidade(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (id.ToString() == null) return View("NotFound");
                Cidade model = cidade.GetCidadesByID(id);
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

        [HttpPost]
        public ActionResult AtualizarCidade(Cidade Ecidade)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (!ModelState.IsValid) return View("NotFound");
                cidade.UpdateCidade(Ecidade);
                return RedirectToAction("Cidades");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeletarCidade(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (id.ToString() == null) return View("NotFound");
                var model = cidade.GetCidadesByID(id);
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

        [HttpPost]
        public ActionResult DeletarCidade(int id, FormCollection collection)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (id.ToString() == null) return View("NotFound");
                var model = cidade.GetCidadesByID(id);
                if (model == null)
                    return View("NotFound");
                else
                    model.DeleteCidadesByID(id);
                return RedirectToAction("Cidades");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
        }

        public ActionResult CriarMedico()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult CriarMedico(Medico medico)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (!ModelState.IsValid) return View();
                medico.AddMedico(medico);
                return RedirectToAction("Medicos");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AtualizarMedico(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (id.ToString() == null) return View("NotFound");
                Medico model = medico.GetMedicosByID(id);
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

        [HttpPost]
        public ActionResult AtualizarMedico(Medico Emedico)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (!ModelState.IsValid) return View("NotFound");
                medico.UpdateMedico(Emedico);
                return RedirectToAction("Medicos");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeletarMedico(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
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

        [HttpPost]
        public ActionResult DeletarMedico(int id, FormCollection collection)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (id.ToString() == null) return View("NotFound");
                var model = medico.GetMedicosByID(id);
                if (model == null)
                    return View("NotFound");
                else
                    model.DeleteMedicosByID(id);
                return RedirectToAction("Medicos");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
        }
    }
}