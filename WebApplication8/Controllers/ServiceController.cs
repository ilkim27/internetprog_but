using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication8.Controllers;
using WebApplication8.Models;
using WebApplication8.ViewModel
namespace WebApplication8.Controllers
{
    public class ServiceController : ApiController
    {
        DB01Entities db = new DB01Entities();
        sonucmodel sonuc = new sonucmodel();

        [HttpGet]
        [Route("api/mesajliste")]
        public List<mesaj> mesajliste()
        {
            List<mesaj> liste = db.mesajlar.Select(x => new mesaj()
            {
                adiSoyadi = x.adiSoyadi,
                msj = x.msj,
                numara = x.numara
            }).ToList();
            return liste;

        }
        [HttpGet]
        [Route("api/mesajlarbyid/{msjid}")]
        public mesaj mesajlarbyid(string msjid)
        {
            mesaj kayit = db.mesajlar.Where(s => s.msj == msjid).Select(x => new mesaj()
            {
                adiSoyadi = x.adiSoyadi,
                msj = x.msj,
                numara = x.numara
            }).FirstOrDefault();
            return kayit;

        }
        [HttpGet]
        [Route("api/mesajekle")]
        public sonucmodel mesajekle(mesaj model)
        {
            mesaj yeni = new mesaj();
            yeni.msj = Guid.NewGuid().ToString();
            yeni.numara = model.numara;
            yeni.adiSoyadi = model.adiSoyadi;
            db.mesajlar.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Eklendi";
            return sonuc;
        }

        [HttpGet]
        [Route("api/kisilerliste")]
        public List<kisi> kisilerlistele()
        {
            List<kisi> liste = db.kisiler.Select(x => new kisi()
            {
                numara = x.numara,
                adı = x.adı,
                soyadı = x.soyadı,
                mail = x.mail,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/kisilerbyid/{kisiid}")]
        public kisi kisibyid(string numara)
        {
            kisi kayit = db.kisiler.Where(s => s.numara == numara).Select(x => new kisi()
            {
                numara = x.numara,
                adı = x.adı,
                soyadı = x.soyadı,
                mail = x.mail,
            }).SingleOrDefault();
            return kayit;
        }
        [HttpGet]
        [Route("api/kisiekle")]
        public sonucmodel kisiekle(kisi model)
        {
            if (db.kisiler.Count(s => s.numara == model.numara) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Numara Daha Önce Kullanılmıştır!!";
            }
            kisi yeni = new kisi();
            yeni.numara = Guid.NewGuid().ToString();
            yeni.adı = model.adı;
            yeni.soyadı = model.soyadı;
            yeni.mail = model.mail;
            db.kisi.add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kişi Eklendi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/kisiduzenle")]
        public sonucmodel kisiduzenle(kisi model)
        {
            kisiler kayit = db.kisiler.Where(s => s.numara == model.numara).SingleOrDefault();
            if (kayit== null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kisi Bulunamadı";
                return sonuc;
            }
            kayit.numara = model.numara;
            kayit.adı = model.adı;
            kayit.soyadı = model.soyadı;
            kayit.mail = model.mail;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kisi Düzenlendi";
            return sonuc; 
        }
        [HttpGet]
        [Route("api/kisisil")]
        public sonucmodel kisisil(string kisiler)
        {
            kisiler kayit = db.kisiler.Where(s => s.numara == numara).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kisi Bulunamadı";
                return sonuc;
            }
            db.kisiler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kisi Silindi";
            return sonuc;
        }
    }
}
