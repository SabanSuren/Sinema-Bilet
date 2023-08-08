using CinemaTicketProject.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CinemaTicketProject.Controllers
{
    public class AdminController : Controller
    {
        SinemaBiletEntities entities = new SinemaBiletEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminGiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminGiris(Admin admin)
        {
            if (ModelState.IsValid)
            {
                using (SinemaBiletEntities entities = new SinemaBiletEntities())
                {
                    var usr = entities.Admin.Where(x => x.AdminEmail == admin.AdminEmail && x.AdminSifre == admin.AdminSifre).FirstOrDefault();
                    if (usr != null)
                    {
                        Session["AdminId"] = usr.AdminId.ToString();
                        Session["AdminAd"] = usr.AdminAd.ToString();
                        Response.Redirect("/Admin/Film");
                    }
                }
                Session["Hata"] = "Kulllanıcı Adı ve ya Şifre Yanlış!";
            }

            return View("AdminGiris");
        }
        public ActionResult AdminLogout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            Response.Redirect("~/Admin/AdminGiris");
            return View();
        }
        public ActionResult Film()
        {
            return View(entities.Film.ToList());
        }
        
        public ActionResult FilmEkle()
        {
            ViewBag.Kategoriler = entities.FilmTur.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult FilmEkle(Film filmler, HttpPostedFileBase imgfile)
        {
            Film di = new Film();
            string path = uploadimage(imgfile);
            if(path.Equals("-1"))
            {

            }
            else
            {
                di.FilmAdi = filmler.FilmAdi;
                di.FilmAciklamasi = filmler.FilmAciklamasi;
                di.FilmSure = filmler.FilmSure;
                di.FilmFiyat = filmler.FilmFiyat;
                di.ResimYol = path;
                entities.Film.Add(di);
                entities.SaveChanges();
            ViewBag.msg = "Veritabanına eklendi....";
                
            }
            return RedirectToAction("Film");
            
        }
        [HttpPost]
        public string uploadimage(HttpPostedFileBase file)
        {
            string path = "-1";
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("/Images/") + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "" + Path.GetFileName(file.FileName);
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }
            return path;
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
        public ActionResult FilmDuzenle(int id)
        {
            var movieToEdit = (from m in entities.Film where m.FilmId == id select m).First();

            return View(movieToEdit);
        }
       

      

        [HttpPost]
        public ActionResult FilmDuzenle(Film movieToEdit)
        {


            var originalMovie = (from m in entities.Film
                                 where m.FilmId == movieToEdit.FilmId
                                 select m).First();
            if (!ModelState.IsValid)
                return View(originalMovie); // return with movie information

            entities.Entry(originalMovie).CurrentValues.SetValues(movieToEdit);
            entities.SaveChanges();
            ViewBag.sonuc = "Kayıt Güncelle";

            return RedirectToAction("Film"); ;
         
        }


        
        public ActionResult FilmSil(int? FilmID)
        {
            Film films = entities.Film.FirstOrDefault(x => x.FilmId == FilmID);
            entities.Film.Remove(films);
            entities.SaveChanges();
            return RedirectToAction("Film");
        }
        public ActionResult FilmTur()
        {
            return View(entities.FilmTur.ToList());
        }
        public ActionResult FilmTurEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FilmTurEkle(FilmTur turler)
        {
            entities.FilmTur.Add(turler);
            entities.SaveChanges();
            return RedirectToAction("FilmTur");
        }

        public ActionResult FilmTurSil(int turlerID)
        {
            Film filmler = entities.Film.FirstOrDefault(x => x.TurId == turlerID);
            if (filmler == null)
            {
                FilmTur turler = entities.FilmTur.FirstOrDefault(x => x.TurId == turlerID);
                entities.FilmTur.Remove(turler);
                entities.SaveChanges();
            }
            else if (filmler != null)
            {
                Session["Turhata"] = "Lütfen Önce Ture Ait Filmleri Siliniz.";
            }
            return RedirectToAction("FilmTur");
        }
        //public ActionResult Görsel()
        //{
        //    return View(entities.Resim.ToList());
        //}

        //public ActionResult GörselEkle(int? FilmId)
        //{

        //    return View(FilmId);
        //}

        //[HttpPost]
        //public ActionResult GörselEkle(int fId, HttpPostedFileBase fileUpload)
        //{
        //    if (fileUpload != null)
        //    {
        //        Image img = Image.FromStream(fileUpload.InputStream);
        //        //Bitmap resimBoyut = new Bitmap(img, Ayarlar.Ayarlar.UrunBoyut);
        //        string uzanti = "/Görsel/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

        //        //resimBoyut.Save(Server.MapPath(uzanti));
        //        Resim Görsel = new Resim
        //        {
        //            ResimYl = uzanti,
        //            FilmId = fId
        //        };
        //        if (entities.Resim.FirstOrDefault(x => x.FilmId == fId && x.ResimBit == false) != null)
        //        {
        //            Görsel.ResimBit = true;
        //        }
        //        else
        //        {
        //            Görsel.ResimBit = false;
        //        }

        //        entities.Resim.Add(Görsel);
        //        entities.SaveChanges();

        //    }
        //    return View(fId);
        //}

        public ActionResult Salon()
        {
            return View(entities.Salon.ToList());
        }

        public ActionResult SalonEkle()
        {
            ViewBag.Kategoriler = entities.Salon.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult SalonEkle(Salon salonlar, HttpPostedFileBase imgfile)
        {
            Salon d = new Salon();
            string path = uploadimage(imgfile);


            if (path.Equals("-1"))
            {

            }
            else
            {
                d.SalonAdi = salonlar.SalonAdi;
                d.SalonSlogan = salonlar.SalonSlogan;
                d.SalonAciklamasi = salonlar.SalonAciklamasi;
                d.SalonResim = path;
                entities.Salon.Add(d);
                entities.SaveChanges();

                ViewBag.msg = "data added....";
                
            }

            return RedirectToAction("Salon");
            
        }
        public ActionResult SalonSil(int? SalonID)
        {
            Salon saln = entities.Salon.FirstOrDefault(x => x.SalonId == SalonID);
            entities.Salon.Remove(saln);
            entities.SaveChanges();
            return RedirectToAction("Salon");
        }




    }
}