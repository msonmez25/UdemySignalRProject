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
    public class EfRestaurantTableDal : GenericRepository<RestaurantTable>, IRestaurantTableDal
    {
        public EfRestaurantTableDal(SignalRContext context) : base(context)
        {
        }

        public void ChangeRestaurantTableStatusToFalse(int id)
        {
            var context = new SignalRContext();
            var value = context.RestaurantTables.Where(x => x.RestaurantTableID == id).FirstOrDefault();
            value.Status = false;
            context.SaveChanges();
        }

        public void ChangeRestaurantTableStatusToTrue(int id)
        {
            var context = new SignalRContext();
            var value = context.RestaurantTables.Where(x => x.RestaurantTableID == id).FirstOrDefault();
            value.Status = true;
            context.SaveChanges();
        }

        public int CountTable()
        {
            var context = new SignalRContext();
            var value = context.RestaurantTables.Count();
            return value;
        }
    }
}
