using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOdasi.Entity;
using OOdasi.Common;

namespace OOdasi.Repository
{
    
    public class UserRepository : DataRepository<UserInfo, int>
    {
        public static OgretmenlerOdasiDBEntities db = Tools.GetConnection();
        ResultProcess<UserInfo> result = new ResultProcess<UserInfo>();
        public override Result<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<UserInfo> GetObjById(int id)
        {
            return result.GetT(db.UserInfoes.SingleOrDefault(t => t.UserId == id));
        }

        public override Result<int> Insert(UserInfo item)
        {
            item.ilanSayisi = 0;
            item.UserUyeDate = DateTime.Now;
            db.UserInfoes.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<UserInfo>> List()
        {
            List<UserInfo> uyeList = db.UserInfoes.ToList();
            return result.GetListResult(uyeList);
        }

        public override Result<int> Update(UserInfo item)
        {
            UserInfo uptd = db.UserInfoes.SingleOrDefault(t => t.UserId == item.UserId);
            uptd.UserName = item.UserName;
            uptd.UserLastname = item.UserLastname;
            uptd.UserMail = item.UserMail;
            uptd.RoleId = item.RoleId;
            uptd.UserSehirId = item.UserSehirId;
            uptd.UserUyeDate = item.UserUyeDate;
            uptd.ilanSayisi = item.ilanSayisi;
            uptd.UserBio = item.UserBio;
            uptd.UserPass = item.UserPass;
            uptd.UserBolum = item.UserBolum;
            uptd.UserLise = item.UserLise;
            uptd.UserUniversite = item.UserUniversite;
            uptd.UserAge = item.UserAge;
            uptd.UserPhoto = item.UserPhoto;
            uptd.UserPhone = item.UserPhone;
            return result.GetResult(db);
        }
    }
}
