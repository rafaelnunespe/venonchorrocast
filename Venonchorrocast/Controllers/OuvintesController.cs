using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Venonchorrocast.Models;
using Venonchorrocast.Repositories;
using Xceed.Wpf.Toolkit;

namespace Venonchorrocast.Controllers
{
    public class OuvintesController : Controller
    {
        private ConexaoBancoDeDados db = new ConexaoBancoDeDados();

        // GET: Ouvintes
        public ActionResult Index()
        {
            try
            {
                return View(db.Ouvinte.ToList());
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Ouvintes/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return View("Error");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Ouvinte ouvinte = db.Ouvinte.Find(id);
                if (ouvinte == null)
                {
                    return View("Error");
                    //return HttpNotFound();
                }
                return View(ouvinte);
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.Ouvinte where temp.id_ouvinte == Id select temp.Foto;
            byte[] cover = q.First();
            return cover;
        }

        // GET: Ouvintes/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        // POST: Ouvintes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ouvinte,Nome,Apelido,Email,Senha,Foto")] Ouvinte ouvinte)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    try
                    {
                        HttpPostedFileBase file = Request.Files["ImageData"];
                        OuvinteRepository service = new OuvinteRepository();
                        int i = service.UploadImageInDataBase(file, ouvinte);
                        if (i == 1)
                        {
                            Session["usuarioLogadoID"] = ouvinte.id_ouvinte.ToString();
                            Session["nomeUsuarioLogado"] = ouvinte.Email.ToString();
                            Session["apelidoLogado"] = ouvinte.Apelido.ToString();
                            return RedirectToAction("Valido");
                        }
                        return View(ouvinte);

                    }
                    catch (Exception)
                    {
                        db.Ouvinte.Add(ouvinte);
                        db.SaveChanges();
                        Session["usuarioLogadoID"] = ouvinte.id_ouvinte.ToString();
                        Session["nomeUsuarioLogado"] = ouvinte.Email.ToString();
                        Session["apelidoLogado"] = ouvinte.Apelido.ToString();
                        return RedirectToAction("Valido");
                    }
                }
                catch (Exception)
                {
                    TempData["mensagemErro"] = "Email já cadastrado, favor utilizar outro email ou tentar realizar o Login";
                    return RedirectToAction("Create");
                }

            }

            return View(ouvinte);
        }


        // GET: Ouvintes/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                ViewData["IdDetails"] = id;
                if (id == null)
                {
                    return View("Error");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Ouvinte ouvinte = db.Ouvinte.Find(id);
                if (ouvinte == null)
                {
                    return View("Error");
                    //return HttpNotFound();
                }
                return View(ouvinte);
            }
            catch (Exception)
            {
                return View("Error");
            }
           
        }

        // POST: Ouvintes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_ouvinte,Nome,Apelido,Email,Senha,Foto")] Ouvinte ouvinte)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpPostedFileBase file = Request.Files["ImageData"];
                    OuvinteRepository service = new OuvinteRepository();
                    int i = service.UploadImageInDataBase(file, ouvinte);
                    if (i == 1)
                    {
                        return RedirectToAction("Details", "Ouvintes", new { id = ouvinte.id_ouvinte });
                    }
                    return View(ouvinte);
                }
                catch (Exception)
                {
                    db.Entry(ouvinte).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", "Ouvintes", new { id = ouvinte.id_ouvinte });
                }
            }

            return View(ouvinte);
        }

        // GET: Ouvintes/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return View("Error");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Ouvinte ouvinte = db.Ouvinte.Find(id);
                if (ouvinte == null)
                {
                    return View("Error");
                    //return HttpNotFound();
                }
                return View(ouvinte);
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        // POST: Ouvintes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Ouvinte ouvinte = db.Ouvinte.Find(id);
                db.Ouvinte.Remove(ouvinte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErroDelete"] = "Para excluir um ouvinte sera preciso apagar todos os comentarios realizados pelo mesmo!";
                Ouvinte ouvinte = db.Ouvinte.Find(id);
                return View(ouvinte);
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email,Senha")] Ouvinte ouvinte)
        {
            var v = db.Ouvinte.Where(model => model.Email.Equals(ouvinte.Email) && model.Senha.Equals(ouvinte.Senha)).FirstOrDefault();
            if (v != null)
            {
                Session["usuarioLogadoID"] = v.id_ouvinte.ToString();
                Session["nomeUsuarioLogado"] = v.Email.ToString();
                Session["apelidoLogado"] = v.Apelido.ToString();
                return RedirectToAction("Valido");
            }
            TempData["ErroLogin"] = "Acesso negado, favor verificar as credenciais e tentar novamente!";
            return View(ouvinte);
        }


        public ActionResult Valido()
        {
            if (Session["usuarioLogadoID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session["usuarioLogadoID"] = null;
            Session["nomeUsuarioLogado"] = null;
            Session["apelidoLogado"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Voltar()
        {
            return RedirectToAction("Index", "Home");
        }


    }

}
