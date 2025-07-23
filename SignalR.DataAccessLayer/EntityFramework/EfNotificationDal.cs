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
    public class EfNotificationDal : GenericRepository<Notification>, INotificationDal
    {
        public EfNotificationDal(SignalRContext context) : base(context)
        {
        }

        public List<Notification> GetAllNotificationByFalseList()
        {
            var context = new SignalRContext();
            var values = context.Notifications.Where(x => x.Status == false).ToList();
            return values;
        }

        public int NotificationCountByStatusFalse()
        {
            var context = new SignalRContext();
            var value = context.Notifications.Where(x => x.Status == false).Count();
            return value;
        }

        public void NotificationStatusChangeToFalse(int id)
        {
            var context = new SignalRContext();
            var value = context.Notifications.Find(id);
            value.Status= false;
            context.SaveChanges();
        }

        public void NotificationStatusChangeToTrue(int id)
        {
            var context = new SignalRContext();
            var value = context.Notifications.Find(id);
            value.Status = true;
            context.SaveChanges();
        }
    }
}
