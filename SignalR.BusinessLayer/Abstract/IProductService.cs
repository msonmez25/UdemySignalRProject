using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IProductService:IGenericService<Product>
    {
        List<Product> TGetProductsWithCategoryName();
        List<Product> TGetTrueAndFalseProductsWithCategoryName();
        List<Product> TGetLast12ProductsCategoryGroup();
        void TProductStatusChangeTrue(int id);
        void TProductStatusChangeFalse(int id);
        public int TProductCount();
        public int TProductCountByCategoryNameHamburger();
        public int TProductCountByCategoryNameDrink();
        public decimal TProductPriceAvg();
        public decimal TProductPriceMax();
        public string TProductNameByPriceMax();
        public decimal TProductPriceMin();
        public string TProductNameByPriceMin();
        public decimal TProductAvgPriceByHamburger();

    }
}
