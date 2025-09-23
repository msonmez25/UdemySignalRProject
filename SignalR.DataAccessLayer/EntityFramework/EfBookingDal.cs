using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfBookingDal : GenericRepository<Booking>, IBookingDal
    {
        public EfBookingDal(SignalRContext context) : base(context)
        {
        }

        public void BookingStatusApproved(int id)
        {
            var context = new SignalRContext();
            var value = context.Bookings.Find(id);
            value.Description = "Rezervason Onaylandı";
            value.Status = true;
            context.SaveChanges();
        }

        public void BookingStatusCanceled(int id)
        {
            var context = new SignalRContext();
            var value = context.Bookings.Find(id);
            value.Description = "Rezervason İptal Edildi";
            value.Status = false;
            context.SaveChanges();
        }

        public int OnaylanmamisBookingCount()
        {
            var context= new SignalRContext();
            var value = context.Bookings.Where(x => x.Description == "Rezervasyon Alındı").Count();
            return value;
        }

        public int OnaylanmisBookingCount()
        {
            var context = new SignalRContext();
            var value = context.Bookings.Where(x => x.Description == "Rezervason Onaylandı").Count();
            return value;
        }

        public int TotalBookingCount()
        {
            var context = new SignalRContext();
            var value = context.Bookings.Count();
            return value;
        }

        public int IptalEdilmisBookingCount()
        {
            var context = new SignalRContext();
            var value = context.Bookings.Where(x => x.Description == "Rezervason İptal Edildi").Count();
            return value;
        }
    }
}
