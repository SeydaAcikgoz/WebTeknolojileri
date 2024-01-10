using Antlr.Runtime.Misc;
using Newtonsoft.Json.Linq;
using SaglikApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SaglikApplication.Controllers
{
    public class HomeController : Controller
    {
        
        SaglikkDBEntities ent = new SaglikkDBEntities();  //her yerde bu entitiy kullanılacak
        int tc;
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginKontrol(int hastaTC, int sifre)
        {
            List<Giris> model = ent.Giris.ToList();

            foreach (var item in model)
            {
                if (item.TC == hastaTC && item.Sifre == sifre)
                {
                    // Oturumda hastaTC'yi sakla
                    Session["HastaTC"] = hastaTC;

                    return RedirectToAction("Index");
                }
            }

            TempData["ErrorMessage"] = "Kullanıcı adı veya şifre hatalı. Yeniden Deneyiniz!";
            return RedirectToAction("Login");
        }
        /*
        public ActionResult LoginKontrol(int hastaTC, int sifre)
        {
            tc = hastaTC;
            List<Giris> model = new List<Giris>();
            model = ent.Giris.ToList();

            foreach (var item in model)
            {
                if(item.TC  == hastaTC)
                {
                    if (item.Sifre  == sifre)
                    {
                        tc = hastaTC;
                        return RedirectToAction("Index");
                        
                    }
                }
            }
            
            TempData["ErrorMessage"] = "Kullanıcı adı veya şifre hatalı.Yeniden Deneyiniz!";
            return RedirectToAction("Login");
        }
        */
        [HttpPost]
        
        public ActionResult YeniHasta()
        {
            YeniHastaClass model = new YeniHastaClass();

            model.hastas = ent.Hasta.ToList();
            model.logins = ent.Giris.ToList();


            return View(model);
        }

        [HttpPost]

        public ActionResult YeniHastaEkle(int hastaTC, int sifre)
        {

            List<Hasta> model2 = new List<Hasta>();
            model2 = ent.Hasta.ToList();

            List<Giris> model = new List<Giris>();
            model = ent.Giris.ToList();

            Giris r = new Giris();


            foreach (var item in model)
            {
                if (item.TC == hastaTC)
                {
                    TempData["ErrorMessage"] = "Zaten bu TC ye kayıtlı kullanıcı var";
                    return RedirectToAction("Login");
                }
            }
            int index = 0;
            foreach (var item in model2)
            {
                if (item.TC == hastaTC)
                {
                    index++;
                }
            }

            if (index==0)
            {
                TempData["ErrorMessage"] = "Bu TC ye ait kullanıcı bulunamadı.";
                return RedirectToAction("Login");
            }

            r.TC = hastaTC;
            r.Sifre = sifre;

            ent.Giris.Add(r);
            ent.SaveChanges();

            return RedirectToAction("Index");
        }

        
        public ActionResult Index()
        {
            // Oturumdan hastaTC'yi al
            int tc = (int)Session["HastaTC"];

            // tc değerine göre verileri çek
            List<Hasta> model2 = ent.Hasta.Where(r => r.TC == tc).ToList();
            return View(model2);

        }

        public ActionResult Randevular()
        {
            // Oturumdan hastaTC'yi al
            int tc = (int)Session["HastaTC"];

            // tc değerine göre verileri çek
            List<Randevu> model2 = ent.Randevu.Where(r => r.TC == tc).ToList();
            return View(model2);
        }

        public ActionResult Tahliller()
        {
            // Oturumdan hastaTC'yi al
            int tc = (int)Session["HastaTC"];

            // tc değerine göre verileri çek
            List<Tahlil> model2 = ent.Tahlil.Where(r => r.TC == tc).ToList();
            return View(model2);

        }

        public ActionResult Doktorlar()
        {
            List<Doktor> model = new List<Doktor>();
            model = ent.Doktor.ToList();
            return View(model);
        }

        public ActionResult Hastaneler()
        {
            List<Hastane> model = new List<Hastane>();
            model = ent.Hastane.ToList();
            return View(model);
        }

        public ActionResult Ilaclar()
        {
            // Oturumdan hastaTC'yi al
            int tc = (int)Session["HastaTC"];

            // tc değerine göre verileri çek
            List<Ilac> model2 = ent.Ilac.Where(r => r.TC == tc).ToList();
            return View(model2);
            
        }

        public ActionResult YeniRandevu()
        {
            YeniRandevuClass model = new YeniRandevuClass();

            model.hastas = ent.Hasta.ToList();
            model.doktors = ent.Doktor.ToList();
            model.polikliniks = ent.Poliklinik.ToList();

            return View(model);
        }

        public ActionResult YeniRandevuEkle(int doktorID, DateTime tarih)
        {
            List<Randevu> model = new List<Randevu>();
            List<Doktor> model2 = new List<Doktor>();
            model = ent.Randevu.ToList();
            int size = 0;
            size = ent.Randevu.ToList().Count;

            

            Randevu r = new Randevu();

            int tc = (int)Session["HastaTC"];
            r.ID = size + 1;
            r.TC = tc;
            r.Dr_ID = doktorID;
            r.Tarih = tarih;

            foreach (var item in model2)
            {
                if (item.ID == doktorID)
                {
                    r.Polklinik_ID = item.Poliklinik_ID;
                }
            }
            

            ent.Randevu.Add(r);
            ent.SaveChanges();

            return RedirectToAction("Randevular");
        }



        public ActionResult KayitSil(int id)
        {
            Randevu silinecekRandevu = new Randevu();

            silinecekRandevu=ent.Randevu.Where(a=>a.ID == id).FirstOrDefault();
            ent.Randevu.Remove(silinecekRandevu);
            ent.SaveChanges();  //databasede değişim yapar

            return RedirectToAction("Randevular");
        }

        public ActionResult KayitDüzenle(int id)
        {
            DuzenleClass model=new DuzenleClass();
            // Oturumdan hastaTC'yi al
            int tc = (int)Session["HastaTC"];

            model.hastas = ent.Hasta.Where(r => r.TC == tc).ToList();
            model.doktors = ent.Doktor.ToList();
            model.polikliniks = ent.Poliklinik.ToList();


            model.duzenlenecekKayit = ent.Randevu.Find(id);

            return View(model);
        }

       

        [HttpPost]
        public ActionResult KayitDuzenle(int id,int hastaTC, int doktorID, DateTime tarih, int poliklinikID)
        {
            Randevu randevu = new Randevu();
            randevu = ent.Randevu.Find(id);

            int tc = (int)Session["HastaTC"];


            randevu.ID = id;
            randevu.TC = hastaTC;
            randevu.Dr_ID = doktorID;
            randevu.Tarih = tarih.Date;
            randevu.Polklinik_ID = poliklinikID;

            ent.SaveChanges();
            return RedirectToAction("Randevular");
        }

        public JsonResult GetDoktorlarByPoliklinikID(int poliklinikID)
        {
            var doktorlar = ent.Doktor.Where(d => d.Poliklinik_ID == poliklinikID).ToList();
            return Json(doktorlar, JsonRequestBehavior.AllowGet);
        }
    }
}