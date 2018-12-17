using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOdasi.Common;
using OOdasi.Entity;
using OOdasi.Repository;
using OgretmenlerOdasiProjeDeneme1.Areas.Admin.Models.ResultModel;
using OgretmenlerOdasiProjeDeneme1.Models;
namespace OgretmenlerOdasiProjeDeneme1.Areas.Admin.Controllers
{
    [LoginControl]
    public class UserController : Controller
    {
        UserRepository ur = new UserRepository();
        //Result<List<UserInfo>> resultList = new Result<List<UserInfo>>();
        //Result<int> resultint = new Result<int>();
        //Result<UserInfo> resultt = new Result<UserInfo>();
        InstanceResult<UserInfo> result = new InstanceResult<UserInfo>();
        // GET: Admin/User
        [Authorize]
        public ActionResult List()
        {
            result.resultList = ur.List();
            return View(result.resultList.ProcessResult);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            result.Tresult = ur.GetObjById(id);
            return View(result.Tresult.ProcessResult);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(UserInfo model)
        {
            result.resultint = ur.Update(model);
            ViewBag.Mesaj = result.resultint.UserMessage;
            return RedirectToAction("List");
        }
    }
}