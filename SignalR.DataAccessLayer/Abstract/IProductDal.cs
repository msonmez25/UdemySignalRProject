using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        List<Product> GetProductsWithCategoryName();
        List<Product> GetLast12ProductsCategoryGroup();
        public int ProductCount();
        public int ProductCountByCategoryNameHamburger();
        public int ProductCountByCategoryNameDrink();
        public decimal ProductPriceAvg();
        public decimal ProductPriceMax();
        public string ProductNameByPriceMax();
        public decimal ProductPriceMin();
        public string ProductNameByPriceMin();
        public decimal ProductAvgPriceByHamburger();

    }
}
