using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Venonchorrocast.Models;

namespace Venonchorrocast.Controllers
{
    public class HomeController : Controller
    {
        private ConexaoBancoDeDados db = new ConexaoBancoDeDados();
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


        public ActionResult Contact()
        {
            ViewBag.Message = "Através das redes sociais:";

            return View();
        }
    }
}