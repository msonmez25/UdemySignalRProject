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
    public class EfTestimonialDal : GenericRepository<Testimonial>, ITestimonialDal
    {
        public EfTestimonialDal(SignalRContext context) : base(context)
        {
        }

        public List<Testimonial> GetTestimonialListByStatusTrue()
        {
            var context = new SignalRContext();
            var values = context.Testimonials.Where(x => x.Status == true).ToList();
            return values;
        }

        public void TestimonialStatusChangeFalse(int id)
        {
            var context = new SignalRContext();
            var value = context.Testimonials.Find(id);
            value.Status = false;
            context.SaveChanges();
        }

        public void TestimonialStatusChangeTrue(int id)
        {
            var context = new SignalRContext();
            var value = context.Testimonials.Find(id);
            value.Status = true;
            context.SaveChanges();
        }
    }
}
