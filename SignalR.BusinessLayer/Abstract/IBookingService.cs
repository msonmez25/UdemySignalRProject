using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IBookingService:IGenericService<Booking>
    {
        void TBookingStatusApproved(int id);
        void TBookingStatusCanceled(int id);

        public int TTotalBookingCount();
        public int TOnaylanmisBookingCount();
        public int TIptalEdilmisBookingCount();
        public int TOnaylanmamisBookingCount();
    }
}
