using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface IBookingDal : IGenericDal<Booking>
    {
        void BookingStatusApproved(int id);
        void BookingStatusCanceled(int id);

        public int TotalBookingCount();
        public int OnaylanmisBookingCount();
        public int IptalEdilmisBookingCount();
        public int OnaylanmamisBookingCount();
    }
}
