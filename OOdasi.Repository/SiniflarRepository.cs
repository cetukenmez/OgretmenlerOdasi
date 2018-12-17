using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOdasi.Entity;
using OOdasi.Common;

namespace OOdasi.Repository
{
    public class SiniflarRepository : DataRepository<Siniflar, int>
    {
        public static OgretmenlerOdasiDBEntities db = Tools.GetConnection();
        ResultProcess<Siniflar> result = new ResultProcess<Siniflar>();
        public override Result<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<Siniflar> GetObjById(int id)
        {
            return result.GetT(db.Siniflars.SingleOrDefault(t => t.SinifId == id));
        }

        public override Result<int> Insert(Siniflar item)
        {
            throw new NotImplementedException();
        }

        public override Result<List<Siniflar>> List()
        {
            List<Siniflar> sinifList = db.Siniflars.ToList();
            return result.GetListResult(sinifList);
        }

        public override Result<int> Update(Siniflar item)
        {
            throw new NotImplementedException();
        }
    }
}
