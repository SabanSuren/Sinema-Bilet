using CinemaTicketProject.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CinemaTicketProject.Controllers
{
    public class HomeController : Controller
    {
        SinemaBiletEntities entities = new SinemaBiletEntities();

        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(Musteri kullanici)
        {
            if (ModelState.IsValid)
            {
                using (SinemaBiletEntities entities = new SinemaBiletEntities())
                {
                    //var kul = entities.Musteri.Single(x => x.MusteriEmail == kullanici.MusteriEmail && x.MusteriSifre == kullanici.MusteriSifre);
                    var kul = entities.Musteri.Where(x => x.MusteriEmail == kullanici.MusteriEmail && x.MusteriSifre == kullanici.MusteriSifre).FirstOrDefault();
                    if (kul != null)
                    {
                        Session["MusteriId"] = kul.MusteriId.ToString();
                        Session["MusteriAd"] = kul.MusteriAdi.ToString();
                        return RedirectToAction("Film");
                    }
                }
                Session["Hata"] = "Kullanıcı Adı veya Şifre Yanlış!";

            }

            return View("UserLogin");
        }

        public ActionResult Biletler(int? id)
        {
            return View(entities.Bilet.Where(b => b.MusteriId == id).ToList());
        }

        public ActionResult BiletEkle(int? Id)
        {
            if(Session["MusteriId"]!=null)
            {
                Bilet blt = new Bilet();
                var x = Session["MusteriId"];
                blt.MusteriId = Convert.ToInt32(x);
                blt.salonlar = entities.Salon.ToList();
                //var item = entities.Film.Where(a => a.FilmId == Id).FirstOrDefault();
                // blt.Film.FilmAdi = item.FilmAdi;
                //blt.Film.VizyonTarihi = item.VizyonTarihi;
                blt.FilmId = Id;

                return View(blt);
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }

        public JsonResult SalonBilgileri(int id)
        {
            Bilet blt = new Bilet();
            blt.secimKoltuklar = entities.Koltuk.Where(k => entities.Bilet.Where(b => b.KoltukNum == k.KoltukNum && b.SalonId==id).Any()).ToList();
            blt.koltuklar = entities.Koltuk.ToList();
            return Json(blt, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BiletEkle(Bilet b)
        {
            if (b.KoltukNum != null || b.FilmId != null)
            {
                b.FilmAdi = entities.Film.Find(b.FilmId).FilmAdi;
                entities.Bilet.Add(b);
                entities.SaveChanges();
                return RedirectToAction("Film");
            }

            else
            {
                ViewBag.Error = "Lütfen bir koltuk seçin.";
                return View(b);
            }
        }

        public ActionResult BiletSil(int id)
        {
            return View(entities.Bilet.Find(id));
        }
        [HttpPost]
        public ActionResult BiletSilmeOnay(Bilet b)
        {
            entities.Bilet.Remove(b);
            entities.SaveChanges();
            return RedirectToAction("Biletler");
        }

        public ActionResult Detay(Film id)
        {
            return View();
        }

        public ActionResult MusteriKayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MusteriKayit(Musteri kullanici)
        {
            entities.Musteri.Add(kullanici);
            entities.SaveChanges();
            Response.Redirect("/Home/UserLogin");
            return View();
        }

        public ActionResult UserLogout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            Response.Redirect("~/Home/UserLogin");
            return View();
        }

        public ActionResult Anasayfa()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Hakkımızda";

            return View();
        }

        public ActionResult İletişim()
        {
            ViewBag.Message = "İletişim";

            return View();
        }
        public PartialViewResult Film()
        {
            return PartialView(entities.Film.ToList());

        }
        public PartialViewResult FilmTur(int? TurID)
        {
            return PartialView(entities.Film.Where(x => x.TurId == TurID).ToList());
        }
        public PartialViewResult TurFilmListe(int TurID)
        {
            return PartialView(entities.Film.Where(x => x.TurId == TurID).ToList());
        }
        public ActionResult FilmDetay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film films = entities.Film.FirstOrDefault(x => x.FilmId == id);
            if (films == null)
            {
                return HttpNotFound();
            }
            return View(films);

        }
        public PartialViewResult Salon()
        {
            return PartialView(entities.Salon.ToList());

        }
        public ActionResult SalonDetay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon saln = entities.Salon.FirstOrDefault(x => x.SalonId == id);
            if (saln == null)
            {
                return HttpNotFound();
            }
            return View(saln);

        }

        public ActionResult FilmButon()
        {
            return View();
        }
        //public ActionResult SalonDetay(string id)
        //{
        //    return View(entities.Salon.FirstOrDefault(x => x.SalonAdi == id));
        //}
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}
    }

    class IActionResult
    {
    }
}