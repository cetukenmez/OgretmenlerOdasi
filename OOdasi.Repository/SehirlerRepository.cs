using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOdasi.Entity;
using OOdasi.Common;

namespace OOdasi.Repository
{
    public class SehirlerRepository : DataRepository<Sehirler, int>
    {
        public static OgretmenlerOdasiDBEntities db = Tools.GetConnection();
        ResultProcess<Sehirler> result = new ResultProcess<Sehirler>();
        public override Result<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<Sehirler> GetObjById(int id)
        {
            return result.GetT(db.Sehirlers.SingleOrDefault(t => t.SehirId == id));
        }

        public override Result<int> Insert(Sehirler item)
        {
            throw new NotImplementedException();
        }

        public override Result<List<Sehirler>> List()
        {
            List<Sehirler> SehList = db.Sehirlers.ToList();
            return result.GetListResult(SehList);

        }

        public override Result<int> Update(Sehirler item)
        {
            throw new NotImplementedException();
        }
    }
}
