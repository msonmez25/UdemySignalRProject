using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void TAdd(Product entity)
        {
           _productDal.Add(entity);
        }

        public void TDelete(Product entity)
        {
           _productDal.Delete(entity);
        }

        public Product TGetByID(int id)
        {
           return _productDal.GetByID(id);
        }

        public List<Product> TGetListAll()
        {
           return _productDal.GetListAll();
        }

        public List<Product> TGetProductsWithCategoryName()
        {
            return _productDal.GetProductsWithCategoryName();
        }

        public List<Product> TGetLast12ProductsCategoryGroup()
        {
            return _productDal.GetLast12ProductsCategoryGroup();
        }

        public int TProductCount()
        {
            return _productDal.ProductCount();
        }

        public int TProductCountByCategoryNameDrink()
        {
            return _productDal.ProductCountByCategoryNameDrink();
        }

        public int TProductCountByCategoryNameHamburger()
        {
            return _productDal.ProductCountByCategoryNameHamburger();
        }

        public string TProductNameByPriceMax()
        {
            return _productDal.ProductNameByPriceMax();
        }

        public string TProductNameByPriceMin()
        {
            return _productDal.ProductNameByPriceMin();
        }

        public decimal TProductPriceAvg()
        {
            return _productDal.ProductPriceAvg();
        }

        public decimal TProductAvgPriceByHamburger()
        {
            return _productDal.ProductAvgPriceByHamburger();
        }

        public decimal TProductPriceMax()
        {
            return _productDal.ProductPriceMax();
        }

        public decimal TProductPriceMin()
        {
            return _productDal.ProductPriceMin();
        }

        public void TUpdate(Product entity)
        {
           _productDal.Update(entity);
        }

        
    }
}
