using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOdasi.Common;
using OOdasi.Entity;
using OOdasi.Repository;
using OgretmenlerOdasiProjeDeneme1.Areas.Admin.Models.ResultModel;
using OgretmenlerOdasiProjeDeneme1.Areas.Admin.Models.ViewModel;
using OgretmenlerOdasiProjeDeneme1.Models;

namespace OgretmenlerOdasiProjeDeneme1.Areas.Admin.Controllers
{
    [LoginControl]
    public class BransController : Controller
    {
        SehirlerRepository sr = new SehirlerRepository();
        BranslarRepository br = new BranslarRepository();
        SiniflarRepository sir = new SiniflarRepository();
        IlanlarRepository Ir = new IlanlarRepository();
        UserRepository ur = new UserRepository();
        InstanceResult<Branslar> result = new InstanceResult<Branslar>();
        // GET: Admin/Brans
        [Authorize]
        [LoginControl]
        public ActionResult List(UserInfo model)
        {

                result.resultList = br.List();
                return View(result.resultList.ProcessResult);
        }

        [Authorize]
        [LoginControl]
        [HttpGet]
        public ActionResult AddBrans()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginControl]
        public ActionResult AddBrans(Branslar model)
        {
            result.resultint = br.Insert(model);
            ViewBag.Mesaj = result.resultint.UserMessage;
            return View();
        }
    }

}