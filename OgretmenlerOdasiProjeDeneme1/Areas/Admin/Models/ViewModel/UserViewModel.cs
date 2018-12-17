using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOdasi.Entity;

namespace OgretmenlerOdasiProjeDeneme1.Areas.Admin.Models.ViewModel
{
    public class UserViewModel
    {
        public UserInfo Userlar { get; set; }
        public IEnumerable<SelectListItem> sehirList { get; set; }
        public IEnumerable<SelectListItem> bransList { get; set; }
    }
}