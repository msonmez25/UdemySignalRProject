using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
    public interface ITestimonialService:IGenericService<Testimonial>
    {
        void TTestimonialStatusChangeTrue(int id);
        void TTestimonialStatusChangeFalse(int id);
        List<Testimonial> TGetTestimonialListByStatusTrue();
    }
}
