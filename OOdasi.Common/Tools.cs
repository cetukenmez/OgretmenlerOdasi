using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOdasi.Entity;

namespace OOdasi.Common
{
    public class Tools
    {
        public static OgretmenlerOdasiDBEntities db = null;
        public static OgretmenlerOdasiDBEntities GetConnection()
        {
            if (db==null)
            {
                db = new OgretmenlerOdasiDBEntities();
            }
            return db;
        }
    }
}
