using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfSocialMediaDal : GenericRepository<SocialMedia>, ISocialMediaDal
    {
        public EfSocialMediaDal(SignalRContext context) : base(context)
        {
        }

        public List<SocialMedia> GetSocialMediaListByStatusTrue()
        {
            var context = new SignalRContext();
            var values = context.SocialMedias.Where(x => x.Status == true).ToList();
            return values;
        }

        public void SocialMediaStatusChangeFalse(int id)
        {
            var context = new SignalRContext();
            var value = context.SocialMedias.Find(id);
            value.Status = false;
            context.SaveChanges();
        }

        public void SocialMediaStatusChangeTrue(int id)
        {
            var context = new SignalRContext();
            var value = context.SocialMedias.Find(id);
            value.Status = true;
            context.SaveChanges();
        }
    }
}
