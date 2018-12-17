using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOdasi.Entity;
using OOdasi.Repository;
using OOdasi.Common;
using OgretmenlerOdasiProjeDeneme1.Areas.Admin.Models.ResultModel;
using OgretmenlerOdasiProjeDeneme1.Areas.Admin.Models.ViewModel;
using OgretmenlerOdasiProjeDeneme1.Models.VM;
using System.Web.Security;
using PagedList;
using Microsoft.AspNet.Identity;

namespace OgretmenlerOdasiProjeDeneme1.Controllers
{
    public class UserIlanController : Controller
    {
        SehirlerRepository sr = new SehirlerRepository();
        BranslarRepository br = new BranslarRepository();
        SiniflarRepository sir = new SiniflarRepository();
        IlanlarRepository Ir = new IlanlarRepository();
        UserRepository ur = new UserRepository();
        InstanceResult<Ilanlar> result = new InstanceResult<Ilanlar>();
        InstanceResult<UserInfo> rrr = new InstanceResult<UserInfo>();
        // GET: UserIlan
        [Authorize]
        public ActionResult Ilanlarim(int? page)
        {
            int id = int.Parse(User.Identity.Name);
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            result.resultList = Ir.List();
            return View(result.resultList.ProcessResult.Where(t => t.UserId == id).OrderByDescending(t => t.IlanTarih).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        [Authorize]
        public ActionResult AddIlanUser()
        {
            IlanViewModel pwm = new IlanViewModel();
            List<SelectListItem> sehList = new List<SelectListItem>();
            List<SelectListItem> bransList = new List<SelectListItem>();
            List<SelectListItem> sinifList = new List<SelectListItem>();
            foreach (Sehirler item in sr.List().ProcessResult)
            {
                sehList.Add(new SelectListItem { Value = item.SehirId.ToString(), Text = item.SehirAdi });
            }
            foreach (Branslar item in br.List().ProcessResult)
            {
                bransList.Add(new SelectListItem { Value = item.BransId.ToString(), Text = item.BransAdi });
            }
            foreach (Siniflar item in sir.List().ProcessResult)
            {
                sinifList.Add(new SelectListItem { Value = item.SinifId.ToString(), Text = item.SinifAdi });
            }
            //rrr.Tresult = ur.GetObjById(id);
            pwm.sehirList = sehList;
            pwm.bransList = bransList;
            pwm.sinifList = sinifList;
            pwm.Ilanlar = null;
            return View(pwm);
        }
        
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddIlanUser(IlanViewModel model)
        {
            int id = int.Parse(User.Identity.Name);
            model.Ilanlar.UserId = id;
            result.resultint = Ir.Insert(model.Ilanlar);
            if (result.resultint.ProcessResult > 0)
            {
                model.Ilanlar.UserInfo.ilanSayisi++;
                result.resultint = ur.Update(model.Ilanlar.UserInfo);
                ViewBag.Mesaj = result.resultint.UserMessage;
                return RedirectToAction("Ilanlarim");
            }
            return View(result.Tresult.ProcessResult);
        }
        [Authorize]
        public ActionResult Profilim()
        {
            int id = int.Parse(User.Identity.Name);
            rrr.resultList = ur.List();
            return View(rrr.resultList.ProcessResult.Where(t=> t.UserId == id));
        }
        [Authorize]
        [HttpGet]
        public ActionResult ProfilDuzenle(int id)
        {
            rrr.Tresult = ur.GetObjById(id);
            return View(rrr.Tresult.ProcessResult);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfilDuzenle(UserInfo model)
        {
            rrr.resultint = ur.Update(model);
            if (rrr.resultint.IsSucceeded)
            {
                return RedirectToAction("Profilim");
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult SifreDegistir(int id)
        {
            rrr.Tresult = ur.GetObjById(id);
            return View(rrr.Tresult.ProcessResult);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SifreDegistir(UserInfo model)
        {
            rrr.resultint = ur.Update(model);
            if (rrr.resultint.IsSucceeded)
            {
                return RedirectToAction("Profilim");
            }
            return View(model);
        }


        [Authorize]
        [HttpGet]
        public ActionResult FotoDuzenle(int id)
        {
            rrr.Tresult = ur.GetObjById(id);
            return View(rrr.Tresult.ProcessResult);
        }

        [Authorize]
        [HttpPost]
        public ActionResult FotoDuzenle(UserInfo model, HttpPostedFileBase photoPath)
        {
            string PhotoName = model.UserPhoto;
            if (photoPath.ContentLength > 0 & photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + PhotoName);
                photoPath.SaveAs(path);
            }
            model.UserPhoto = PhotoName;
            rrr.resultint = ur.Update(model);
            if (rrr.resultint.IsSucceeded)
            {
                return RedirectToAction("Profilim");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            result.resultint = Ir.Delete(id);
            return RedirectToAction("Ilanlarim", new { @mesaj = result.resultint.UserMessage });
        }

        [Authorize]
        [HttpGet]
        public ActionResult IlanDuzenle(int id)
        {
            result.Tresult = Ir.GetObjById(id);
            return View(result.Tresult.ProcessResult);
        }

        [Authorize]
        [HttpPost]
        public ActionResult IlanDuzenle(Ilanlar model)
        {
            result.resultint = Ir.Update(model);
            if (result.resultint.IsSucceeded)
            {
                return RedirectToAction("Ilanlarim");
            }
            return View(model);
        }
        [Authorize]
        public ActionResult ProfilimOgrc()
        {
            int id = int.Parse(User.Identity.Name);
            rrr.resultList = ur.List();
            return View(rrr.resultList.ProcessResult.Where(t => t.UserId == id)
                .Where(t=> t.RoleId==3));
        }
        [Authorize]
        [HttpGet]
        public ActionResult ProfilDuzenleOgrc(int id)
        {
            rrr.Tresult = ur.GetObjById(id);
            return View(rrr.Tresult.ProcessResult);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfilDuzenleOgrc(UserInfo model)
        {
            rrr.resultint = ur.Update(model);
            if (rrr.resultint.IsSucceeded)
            {
                return RedirectToAction("ProfilimOgrc");
            }
            return View(model);
        }
    }
}