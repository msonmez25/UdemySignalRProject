using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface ITestimonialDal : IGenericDal<Testimonial>
    {
        void TestimonialStatusChangeTrue(int id);
        void TestimonialStatusChangeFalse(int id);
        List<Testimonial> GetTestimonialListByStatusTrue();
    }
}
