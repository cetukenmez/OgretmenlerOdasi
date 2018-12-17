using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOdasi.Common;
using OOdasi.Entity;

namespace OOdasi.Repository
{
    public class IlanlarRepository : DataRepository<Ilanlar, int>
    {
        UserRepository ur = new UserRepository();
        private static OgretmenlerOdasiDBEntities db = Tools.GetConnection();
        ResultProcess<Ilanlar> result = new ResultProcess<Ilanlar>();
        public override Result<int> Delete(int id)
        {
            Ilanlar sil = db.Ilanlars.SingleOrDefault(t => t.İlanId == id);
            sil.UserInfo.ilanSayisi--;
            ur.Update(sil.UserInfo);
            db.Ilanlars.Remove(sil);
            return result.GetResult(db);
        }

        public override Result<Ilanlar> GetObjById(int id)
        {
            
            return result.GetT(db.Ilanlars.SingleOrDefault(t => t.İlanId == id));
        }

        public override Result<int> Insert(Ilanlar item)
        {
            item.IlanTarih = DateTime.Now;            
            db.Ilanlars.Add(item);

            return result.GetResult(db);
        }

        public override Result<List<Ilanlar>> List()
        {
            List<Ilanlar> IlanList = db.Ilanlars.ToList();
            return result.GetListResult(IlanList);
        }

        public override Result<int> Update(Ilanlar item)
        {
            Ilanlar uptd = db.Ilanlars.SingleOrDefault(t => t.İlanId == item.İlanId);
            uptd.UserId = item.UserId;
            uptd.SehirId = item.SehirId;
            uptd.BransId = item.BransId;
            uptd.SinifId = item.SinifId;
            uptd.minUcret = item.minUcret;
            uptd.maxUcret = item.maxUcret;
            uptd.IlanRole = item.IlanRole;
            uptd.IlanTarih = item.IlanTarih;
            return result.GetResult(db);
        }
    }
}
