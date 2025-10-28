using Microsoft.EntityFrameworkCore;
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
    public class EfOrderDal : GenericRepository<Order>, IOrderDal
    {
        public EfOrderDal(SignalRContext context) : base(context)
        {
        }

        public int ActiveOrderCount()
        {
            var context = new SignalRContext();
            var value = context.Orders.Where(x => x.Description == "Açık").Count();
            return value;
        }


        public decimal LastOrderPrice()
        {
            var context = new SignalRContext();
            var value = context.Orders.OrderByDescending(x => x.OrderID).Take(1).Select(y => y.TotalPrice).FirstOrDefault();
            return value;
        }

        public decimal TodayTotalPrice()
        {
            var context = new SignalRContext();
            var value = context.Orders.Where(x => x.Date.Month == (DateTime.Now.Month)).Where(y => y.Date.Day == (DateTime.Now.Day)).Sum(z => z.TotalPrice);
            return value;
        }

        public int TotalOrderCount()
        {
            var context = new SignalRContext();
            var value = context.Orders.Count();
            return value;
        }
    }
}
