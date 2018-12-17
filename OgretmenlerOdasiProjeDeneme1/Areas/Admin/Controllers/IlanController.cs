using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using OOdasi.Common;
using OOdasi.Entity;
using OOdasi.Repository;
using OgretmenlerOdasiProjeDeneme1.Areas.Admin.Models.ResultModel;
using OgretmenlerOdasiProjeDeneme1.Areas.Admin.Models.ViewModel;
using OgretmenlerOdasiProjeDeneme1.Models;

namespace OgretmenlerOdasiProjeDeneme1.Areas.Admin.Controllers
{
    [LoginControl]
    public class IlanController : Controller
    {
       
        SehirlerRepository sr = new SehirlerRepository();
        BranslarRepository br = new BranslarRepository();
        SiniflarRepository sir = new SiniflarRepository();
        IlanlarRepository Ir = new IlanlarRepository();
        UserRepository ur = new UserRepository();
        InstanceResult<Ilanlar> result = new InstanceResult<Ilanlar>();
        // GET: Home
        [Authorize]
        public ActionResult List()
        {
            result.resultList = Ir.List();
            return View(result.resultList.ProcessResult);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            
            result.resultint = Ir.Delete(id);
            return RedirectToAction("List", new { @mesaj = result.resultint.UserMessage });
        }
        [Authorize]
        public ActionResult AddIlan()
        {
            
            IlanViewModel pwm = new IlanViewModel();
            List<SelectListItem> sehList = new List<SelectListItem>();
            List<SelectListItem> bransList = new List<SelectListItem>();
            List<SelectListItem> sinifList = new List<SelectListItem>();
            List<SelectListItem> isimList = new List<SelectListItem>();
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
            foreach (UserInfo item in ur.List().ProcessResult)
            {
                isimList.Add(new SelectListItem { Value = item.UserId.ToString(), Text = item.UserName+" "+item.UserLastname });
            }
            pwm.isimList = isimList;
            pwm.sehirList = sehList;
            pwm.bransList = bransList;
            pwm.sinifList = sinifList;
            pwm.Ilanlar = null;
            return View(pwm);
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddIlan(IlanViewModel model)
        {
            result.resultint = Ir.Insert(model.Ilanlar);            
            if (result.resultint.ProcessResult > 0)
            {               
                model.Ilanlar.UserInfo.ilanSayisi++;
                result.resultint = ur.Update(model.Ilanlar.UserInfo);
                ViewBag.Mesaj = result.resultint.UserMessage;
                return RedirectToAction("List");
            }
            return View();
        }
    }
}