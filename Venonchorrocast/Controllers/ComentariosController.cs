using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Venonchorrocast.Models;

namespace Venonchorrocast.Controllers
{
    public class ComentariosController : Controller
    {
        private ConexaoBancoDeDados db = new ConexaoBancoDeDados();

        // GET: Comentarios
        public ActionResult Index(int? id)
        {
            try
            {
                var comentario = from s in db.Comentario.Include(c => c.Episodio).Include(c => c.Ouvinte) select s;
                comentario = comentario.Where(s => s.fk_id_episodio == id);
                Session["id_ep"] = id;
                return View(comentario.ToList());
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        // GET: Comentarios/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Comentario comentario = db.Comentario.Find(id);
        //    if (comentario == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(comentario);
        //}

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


        // GET: Comentarios/Create
        public ActionResult Create()
        {
            try
            {
                if(Session["usuarioLogadoID"] != null)
                {
                    ViewBag.fk_id_episodio = new SelectList(db.Episodio, "id_episodio", "Titulo");
                    ViewBag.fk_id_ouvinte = new SelectList(db.Ouvinte, "id_ouvinte", "Nome");
                    return View();
                }
                else
                {
                    TempData["ErroComentar"] = "Comentarios permitidos apenas para usuarios cadastrados e logados!";
                    return RedirectToAction("Index", new { id = Session["id_ep"] });
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: Comentarios/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_comentario,Conteudo,Hora_Public,fk_id_episodio,fk_id_ouvinte")] Comentario comentario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    comentario.fk_id_episodio = int.Parse(Session["id_ep"].ToString());
                    comentario.fk_id_ouvinte = int.Parse(Session["usuarioLogadoID"].ToString());
                    comentario.Hora_Public = System.DateTime.Now;
                    db.Comentario.Add(comentario);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = Session["id_ep"] });
                }


                return View(comentario);
            }
            catch (Exception)
            {
                return View("Error");
            }
            

        }


        // GET: Comentarios/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Comentario comentario = db.Comentario.Find(id);
        //    if (comentario == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.fk_id_episodio = new SelectList(db.Episodio, "id_episodio", "Titulo", comentario.fk_id_episodio);
        //    ViewBag.fk_id_ouvinte = new SelectList(db.Ouvinte, "id_ouvinte", "Nome", comentario.fk_id_ouvinte);

        //    return View(comentario);
        //}

        //// POST: Comentarios/Edit/5
        //// Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        //// obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id_comentario,Conteudo,Hora_Public,fk_id_episodio,fk_id_ouvinte")] Comentario comentario)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        comentario.fk_id_episodio = int.Parse(TempData["id_ep"].ToString());
        //        comentario.fk_id_ouvinte = int.Parse(Session["usuarioLogadoID"].ToString());
        //        comentario.Hora_Public = System.DateTime.Now;
        //        db.Entry(comentario).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index", new { id = TempData["id_ep"] });
        //    }

        //    return RedirectToAction("Index", new { id = TempData["id_ep"] });
        //}

        // GET: Comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return View("Error");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Comentario comentario = db.Comentario.Find(id);
                if (comentario == null)
                {
                    return View("Error");
                    //return HttpNotFound();
                }
                return View(comentario);
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Comentario comentario = db.Comentario.Find(id);
                db.Comentario.Remove(comentario);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Session["id_ep"] });
            }
            catch (Exception)
            {
                return View("Error");
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
    }
}
