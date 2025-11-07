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
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        public EfProductDal(SignalRContext context) : base(context)
        {
        }

        public List<Product> GetProductsWithCategoryName()
        {
            var context = new SignalRContext();
           var values = context.Products
    .Include(x => x.Category)
    .Where(p => p.ProductStatus == true)
    .ToList();
            return values;
        }

        public List<Product> GetTrueAndFalseProductsWithCategoryName()
        {
            var context = new SignalRContext();
            var values = context.Products
     .Include(x => x.Category)
     .ToList();
            return values;
        }

        public List<Product> GetLast12ProductsCategoryGroup()
        {
            var context = new SignalRContext();
            var values = context.Products
                .Where(p => p.ProductStatus == true) // 🔥 sadece aktif olanlar
                .AsEnumerable() // bundan sonrası bellekte çalışır
                .GroupBy(p => p.CategoryID)
                .SelectMany(g => g
                .OrderByDescending(p => p.Price)
                .Take(2))
                .ToList();
            return values;
        }

        public int ProductCount()
        {
            using var context = new SignalRContext();
            return context.Products.Count();
        }

        public int ProductCountByCategoryNameDrink()
        {
            using var context = new SignalRContext();
            return context.Products.Where(x => x.CategoryID == (context.Categories.Where(y => y.CategoryName == "İçecek").Select(z => z.CategoryID).FirstOrDefault())).Count();
        }

        public int ProductCountByCategoryNameHamburger()
        {
            using var context = new SignalRContext();
            return context.Products.Where(x => x.CategoryID == (context.Categories.Where(y => y.CategoryName == "Hamburger").Select(z => z.CategoryID).FirstOrDefault())).Count();
        }

        public string ProductNameByPriceMax()
        {
            using var context = new SignalRContext();
            return context.Products.Where(x => x.Price == (context.Products.Max(y => y.Price))).Select(z => z.ProductName).FirstOrDefault();
        }

        public string ProductNameByPriceMin()
        {
            using var context = new SignalRContext();
            return context.Products.Where(x => x.Price == (context.Products.Min(y => y.Price))).Select(z => z.ProductName).FirstOrDefault();
        }

        public decimal ProductPriceAvg()
        {
            using var context = new SignalRContext();
            var avg = context.Products.Average(x => x.Price);
            return Math.Round(avg, 2);
        }

        public decimal ProductAvgPriceByHamburger()
        {
            using var context = new SignalRContext();
            return context.Products.Where(x => x.CategoryID == (context.Categories.Where(y => y.CategoryName == "Hamburger").Select(z => z.CategoryID).FirstOrDefault())).Average(w => w.Price);
        }

        public decimal ProductPriceMax()
        {
            using var context = new SignalRContext();
            return context.Products.Max(x => x.Price);

        }

        public decimal ProductPriceMin()
        {
            using var context = new SignalRContext();
            return context.Products.Min(x => x.Price);
        }

        public void ProductStatusChangeTrue(int id)
        {
            var context = new SignalRContext();
            var value = context.Products.Find(id);
            value.ProductStatus = true;
            context.SaveChanges();
        }

        public void ProductStatusChangeFalse(int id)
        {
            var context = new SignalRContext();
            var value = context.Products.Find(id);
            value.ProductStatus = false;
            context.SaveChanges();
        }

       
    }
}
