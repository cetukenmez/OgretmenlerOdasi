using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOdasi.Entity;
using OOdasi.Repository;
using OgretmenlerOdasiProjeDeneme1.Areas.Admin.Models.ResultModel;
using OgretmenlerOdasiProjeDeneme1.Areas.Admin.Models.ViewModel;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace OgretmenlerOdasiProjeDeneme1.Controllers
{
    public class HomeController : Controller
    {
        IlanlarRepository Ir = new IlanlarRepository();
        UserRepository ur = new UserRepository();
        InstanceResult<Ilanlar> result = new InstanceResult<Ilanlar>();
        InstanceResult<UserInfo> resultUser = new InstanceResult<UserInfo>();
        // GET: Home
        //rolü öğretmen olan ilanları listeliyor giriş yaptıktan sonra
        [Authorize]
        public ActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            result.resultList = Ir.List();
            return View(result.resultList.ProcessResult.Where(t => t.IlanRole == 2).OrderByDescending(t => t.IlanTarih).ToPagedList(pageNumber, pageSize));
        }
        //rolü öğrenci olan ilanları listeliyor giriş yaptıktan sonra
        [Authorize]
        public ActionResult ListOgrenciLogin(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            result.resultList = Ir.List();
            return View(result.resultList.ProcessResult.Where(t => t.IlanRole == 3).OrderByDescending(t => t.IlanTarih).ToPagedList(pageNumber, pageSize));
        }
        //rolü öğretmen olan ilanları listeliyor giriş yapmadan
        public ActionResult ListOgretmen(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            result.resultList = Ir.List();
            return View(result.resultList.ProcessResult.Where(t => t.IlanRole==2).OrderByDescending(t=>t.IlanTarih).ToPagedList(pageNumber,pageSize));
        }

        //rolü öğrenci olan ilanları listeliyor giriş yapmadan
        public ActionResult ListOgrenci(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            result.resultList = Ir.List();
            return View(result.resultList.ProcessResult.Where(t => t.IlanRole == 3).OrderByDescending(t => t.IlanTarih).ToPagedList(pageNumber, pageSize));
        }
        [Authorize]
        public ActionResult ListBySehBrans(int? sehid , int? bransid,int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            if (sehid == null)
            {
                List<Ilanlar> iList = Ir.List().ProcessResult.Where(t => t.BransId == bransid).ToList();
                return View(iList.Where(t => t.IlanRole == 2).OrderByDescending(t => t.IlanTarih).ToPagedList(pageNumber, pageSize));
            }
            else if (bransid == null)
            {
                List<Ilanlar> iList = Ir.List().ProcessResult.Where(t => t.SehirId == sehid).ToList();
                return View(iList.Where(t => t.IlanRole == 2).OrderByDescending(t => t.IlanTarih).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                List<Ilanlar> iList = Ir.List().ProcessResult.Where(t => t.SehirId == sehid)
                                                         .Where(t => t.BransId == bransid).ToList();
                return View(iList.Where(t => t.IlanRole == 2).OrderByDescending(t => t.IlanTarih).ToPagedList(pageNumber, pageSize));
            }           
        }
        [Authorize]
        public ActionResult ListBySehBransOgrc(int? sehid, int? bransid, int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            if (sehid == null)
            {
                List<Ilanlar> iList = Ir.List().ProcessResult.Where(t => t.BransId == bransid).ToList();
                return View(iList.Where(t => t.IlanRole == 3).OrderByDescending(t => t.IlanTarih).ToPagedList(pageNumber, pageSize));
            }
            else if (bransid == null)
            {
                List<Ilanlar> iList = Ir.List().ProcessResult.Where(t => t.SehirId == sehid).ToList();
                return View(iList.Where(t => t.IlanRole == 3).OrderByDescending(t => t.IlanTarih).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                List<Ilanlar> iList = Ir.List().ProcessResult.Where(t => t.SehirId == sehid)
                                                         .Where(t => t.BransId == bransid).ToList();
                return View(iList.Where(t => t.IlanRole == 3).OrderByDescending(t => t.IlanTarih).ToPagedList(pageNumber, pageSize));
            }
        }

        [Authorize]
        public ActionResult ProfiliGoruntule(int id)
        {         
            resultUser.Tresult = ur.GetObjById(id);
            return View(resultUser.Tresult.ProcessResult);
        }

        [Authorize]
        
        public ActionResult Iletisim(int id)
        {
            resultUser.Tresult = ur.GetObjById(id);
            return View(resultUser.Tresult.ProcessResult);
        }

        [Authorize]
        public ActionResult Ogretmenlerodasi(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            resultUser.resultList = ur.List();
            return View(resultUser.resultList.ProcessResult.Where(t => t.RoleId == 2||t.RoleId==1).OrderBy(t => t.UserUyeDate).ToPagedList(pageNumber, pageSize));
        }
    }
}