using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Venonchorrocast.Models;
using Venonchorrocast.Repositories;

namespace Venonchorrocast.Controllers
{
    public class EpisodiosController : Controller
    {
        private ConexaoBancoDeDados db = new ConexaoBancoDeDados();

        // GET: Episodios
        public ActionResult Index()
        {
            try
            {
                var episodios = from s in db.Episodio select s;
                episodios = episodios.OrderByDescending(s => s.id_episodio);
                return View(episodios.ToList());
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        public ActionResult Episodios(string searchString)
        {
            try
            {
                ViewData["CurrentFilter"] = searchString;
                var episodios = from s in db.Episodio select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    episodios = episodios.Where(s => s.Titulo.Contains(searchString)
                                           || s.Participantes.Contains(searchString));
                }
                episodios = episodios.OrderByDescending(s => s.id_episodio);
                return View(episodios.ToList());
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        // GET: Episodios/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return View("Error");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Episodio episodio = db.Episodio.Find(id);
                if (episodio == null)
                {
                    return View("Error");
                    //return HttpNotFound();
                }
                Session["nome_episodio"] = episodio.Titulo;
                ViewData["linkPlay"] = episodio.Link;
                return View(episodio);
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
            var q = from temp in db.Episodio where temp.id_episodio == Id select temp.Arte;
            byte[] cover = q.First();
            return cover;
        }


        // GET: Episodios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Episodios/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_episodio,Titulo,Descriçao,Duraçao,Link,Arte,Data_Public,Qntde_Play,Qntde_Download,Participantes")] Episodio episodio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpPostedFileBase file = Request.Files["ImageData"];
                    EpisodioRepository service = new EpisodioRepository();
                    int i = service.UploadImageInDataBase(file, episodio);
                    if (i == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(episodio);

                }
                catch (Exception)
                {
                    db.Episodio.Add(episodio);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(episodio);
        }

        // GET: Episodios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodio episodio = db.Episodio.Find(id);
            if (episodio == null)
            {
                return View("Error");
                //return HttpNotFound();
            }
            return View(episodio);
        }

        // POST: Episodios/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_episodio,Titulo,Descriçao,Duraçao,Link,Arte,Data_Public,Qntde_Play,Qntde_Download,Participantes")] Episodio episodio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpPostedFileBase file = Request.Files["ImageData"];
                    EpisodioRepository service = new EpisodioRepository();
                    int i = service.EditImageInDataBase(file, episodio);
                    if (i == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(episodio);
                }
                catch (Exception)
                {

                    db.Entry(episodio).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(episodio);
        }

        // GET: Episodios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodio episodio = db.Episodio.Find(id);
            if (episodio == null)
            {
                return View("Error");
                //return HttpNotFound();
            }
            return View(episodio);
        }

        // POST: Episodios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Episodio episodio = db.Episodio.Find(id);
                db.Episodio.Remove(episodio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErroDelete"] = "Para excluir um episodio sera preciso apagar todos os comentarios realizados no mesmo!";
                Episodio episodio = db.Episodio.Find(id);
                return View(episodio);
            }

        }

        public ActionResult Play(int? id)
        {
            Episodio episodio = db.Episodio.Find(id);
            if (ModelState.IsValid)
            {
                try
                {
                    int play = Convert.ToInt32(episodio.Qntde_Play);
                    int download = Convert.ToInt32(episodio.Qntde_Download);
                    play++;
                    download++;
                    episodio.Qntde_Play = play;
                    episodio.Qntde_Download = download;
                    db.Entry(episodio).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = episodio.id_episodio });
                }
                catch (Exception)
                {
                    db.Entry(episodio).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Episodios");
                }
            }
            return View(episodio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
