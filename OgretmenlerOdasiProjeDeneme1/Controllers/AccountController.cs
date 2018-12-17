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
using Microsoft.AspNet.Identity;

namespace OgretmenlerOdasiProjeDeneme1.Controllers
{

    public class AccountController : Controller
    {
        SehirlerRepository sr = new SehirlerRepository();
        BranslarRepository br = new BranslarRepository();
        SiniflarRepository sir = new SiniflarRepository();
        IlanlarRepository Ir = new IlanlarRepository();
        UserRepository ur = new UserRepository();
        InstanceResult<Ilanlar> result = new InstanceResult<Ilanlar>();
        // GET: Account
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {

                using (OgretmenlerOdasiDBEntities context = new OgretmenlerOdasiDBEntities())
                {
                    var user = context.UserInfoes.FirstOrDefault(x => x.UserMail == model.Email && x.UserPass == model.Password);
                    if (user != null)
                    {

                        //FormsAuthentication.SetAuthCookie(user.UserName, true);
                        //UserInfo u = (UserInfo)Session["UserInfo"];                        
                        FormsAuthentication.SetAuthCookie(user.UserId.ToString(),  true);
                        
                        return RedirectToAction("Index", "Home");
                        
                    }
                }
            }
            
            ViewBag.Message = "Kullanici adi veya Parola Yanlış";
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ListOgretmen", "Home");
        }

        [HttpGet]
        public ActionResult UserUyeOl()
        {
            UserViewModel pwm = new UserViewModel();
            List<SelectListItem> sehList = new List<SelectListItem>();
            List<SelectListItem> bransList = new List<SelectListItem>();
            foreach (Sehirler item in sr.List().ProcessResult)
            {
                sehList.Add(new SelectListItem { Value = item.SehirId.ToString(), Text = item.SehirAdi });
            }
            foreach (Branslar item in br.List().ProcessResult)
            {
                bransList.Add(new SelectListItem { Value = item.BransId.ToString(), Text = item.BransAdi });
            }
            pwm.sehirList = sehList;
            pwm.bransList = bransList;
            pwm.Userlar = null;
            return View(pwm);
        }

        //ogretmen üye kaydı
        [HttpPost]
        public ActionResult UserUyeOl(UserViewModel model, HttpPostedFileBase photo)
        {
            string PhotoName = "";
            if (photo.ContentLength > 0 & photo != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + PhotoName);
                photo.SaveAs(path);
            }
            else
            {
                ViewBag.mes = "Lütfen Fotoğraf Seçiniz";
            }
            model.Userlar.UserPhoto = PhotoName;
            model.Userlar.RoleId = 2;
            result.resultint = ur.Insert(model.Userlar);
            if (result.resultint.ProcessResult > 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult UserUyeOlogrenci()
        {
            UserViewModel pwm = new UserViewModel();
            List<SelectListItem> sehList = new List<SelectListItem>();
            List<SelectListItem> bransList = new List<SelectListItem>();
            foreach (Sehirler item in sr.List().ProcessResult)
            {
                sehList.Add(new SelectListItem { Value = item.SehirId.ToString(), Text = item.SehirAdi });
            }
            foreach (Branslar item in br.List().ProcessResult)
            {
                bransList.Add(new SelectListItem { Value = item.BransId.ToString(), Text = item.BransAdi });
            }
            pwm.sehirList = sehList;
            pwm.bransList = bransList;
            pwm.Userlar = null;
            return View(pwm);
        }

        [HttpPost]
        public ActionResult UserUyeOlogrenci(UserViewModel model, HttpPostedFileBase photo)
        {
            string PhotoName = "";
            if (photo.ContentLength > 0 & photo != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + PhotoName);
                photo.SaveAs(path);
            }
            model.Userlar.UserPhoto = PhotoName;
            model.Userlar.RoleId = 3;
            result.resultint = ur.Insert(model.Userlar);
            if (result.resultint.ProcessResult > 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

    }
}