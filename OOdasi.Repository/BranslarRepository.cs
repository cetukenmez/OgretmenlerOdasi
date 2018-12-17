using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOdasi.Entity;
using OOdasi.Common;

namespace OOdasi.Repository
{
     public class BranslarRepository : DataRepository<Branslar, int>
    {
        public static OgretmenlerOdasiDBEntities db = Tools.GetConnection();
        ResultProcess<Branslar> result = new ResultProcess<Branslar>();
        public override Result<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<Branslar> GetObjById(int id)
        {
            return result.GetT(db.Branslars.SingleOrDefault(t => t.BransId == id));
        }

        public override Result<int> Insert(Branslar item)
        {
            db.Branslars.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Branslar>> List()
        {
            List<Branslar> bransList = db.Branslars.ToList();
            return result.GetListResult(bransList);
        }

        public override Result<int> Update(Branslar item)
        {
            throw new NotImplementedException();
        }
    }
}
