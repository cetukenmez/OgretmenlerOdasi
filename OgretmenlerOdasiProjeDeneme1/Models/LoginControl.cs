using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using OOdasi.Entity;
using OOdasi.Repository;

namespace OgretmenlerOdasiProjeDeneme1.Models
{
    public class LoginControl : AuthorizeAttribute
    {
        OgretmenlerOdasiDBEntities db = new OgretmenlerOdasiDBEntities();
        private readonly List<UserRole> _allowedroles = new List<UserRole>();
        
        UserRepository ur = new UserRepository();
         
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            int a = int.Parse(HttpContext.Current.User.Identity.Name);
           UserInfo u = db.UserInfoes.FirstOrDefault(t=> t.UserId == a);

                if (int.Parse(HttpContext.Current.User.Identity.Name) == u.UserId && u.RoleId == 1)
                {

                    return true;

                }
                else
                    return false;
            

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}