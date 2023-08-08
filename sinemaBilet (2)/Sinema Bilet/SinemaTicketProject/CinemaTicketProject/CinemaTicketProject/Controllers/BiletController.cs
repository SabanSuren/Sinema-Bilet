using CinemaTicketProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaTicketProject.Controllers
{
    public class BiletController : Controller
    {
        SinemaBiletEntities entities = new SinemaBiletEntities();
        // GET: Bilet
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult BiletAl(int Id)
        //{
        //    Bilet bilet = entities.Bilet.FirstOrDefault(x => x.BiletId == Id);

        //}

        public ActionResult Bilets()
        {
            return View();

        }
        [HttpGet]
        public ActionResult Biletim()
        {
            var getBilet = entities.Bilet.ToList().OrderByDescending(a => a.FilmId);
            return View(getBilet);

        }

        //[HttpPost]
        //public ActionResult Bilets(Bilet blt)
        //{

        //entities.Bilet.Add(blt);
        //entities.SaveChanges();

        //List<BiletFilmListele> productList = (List<BiletFilmListele>)Session["cart"];
        //Bilet blet = new Bİlet();

        //Bilet.kullanıcı_id = int.Parse((string)Session["UserID"]);


        //Bilet.satis_id = blt.BiletId;
        ////entities.sepet.Add(sepet);
        //entities.SaveChanges();

        //foreach (var product in productList)
        //{
        //    product.sepet_id = sepet.sepet_id;
        //    product.urunler = null;
        //    entities.urunler.FirstOrDefault(x => x.urun_id == product.urun_id).urun_stok -= product.adet;
        //}

        //entities.SaveChanges();
        //Session["sum"] = null;
        //Session["totalcount"] = null;
        //Session["cart"] = null;
        //Response.Redirect("/Home/Anasayfa");
        //return View("Home/Anasayfa");
        //}





    }
}
