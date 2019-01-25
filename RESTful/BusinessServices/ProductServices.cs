using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BusinessEntities;
using DataModel.UnitOfWork;
using AutoMapper;
using DataModel;

namespace BusinessServices
{
    public class ProductServices : IProductServices
    {

        private readonly UnitOfWork _unitOfWork;

        public ProductServices()
        {
            _unitOfWork = new UnitOfWork();
        }

        public int CreateProduct(ProductEntity productEntity)
        {

            using (var scope = new TransactionScope())
            {
                var product = new Product
                {
                    
                    ProductName = productEntity.ProductName,
                    ProductCategoryID = productEntity.ProductCategoryID,
                    UnitPrice = productEntity.UnitPrice,
                    Description = productEntity.Description,
                    Discontinued = productEntity.Discontinued,
                    Stocks = productEntity.Stocks,
                    CreationUser = productEntity.CreationUser,
                    CreationDateTime = productEntity.CreationDateTime,
                    LastUpdateUser = productEntity.LastUpdateUser,
                    LastUpdateDateTime = productEntity.LastUpdateDateTime,
                    Timestamp = productEntity.Timestamp

                };

                _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.Save();
                scope.Complete();
                return product.ProductID;

            }
            
        }

        public bool DeleteProduct(int ProductID)
        {
            var success = false;
            if (ProductID > 0) {
                using (var scope = new TransactionScope()) {

                    var product = _unitOfWork.ProductRepository.GetByID(ProductID);
                    if (product != null) {
                        _unitOfWork.ProductRepository.Delete(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }

                }
            }
            return success;
        }

        public IEnumerable<ProductEntity> GetAllProducts()
        {
            var product = _unitOfWork.ProductRepository.GetAll().ToList();
            if (product.Any())
            {
                var productModel = Mapper.Map<List<Product>, List<ProductEntity>>(product);
                return productModel;
            }
            return null;
        }

        public ProductEntity GetProductById(int ProductID)
        {
            var product = _unitOfWork.ProductRepository.GetByID(ProductID);
            if (product != null)
            {
                var productModel = Mapper.Map<Product, ProductEntity>(product);
                return productModel;
            }
            return null;
        }

        public bool UpdateProduct(int ProductID, ProductEntity productEntity)
        {
            var success = false;
            if (productEntity != null) {

                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetByID(ProductID);
                    if (product != null)
                    {
                        product.ProductName = productEntity.ProductName;
                        product.ProductCategoryID = productEntity.ProductCategoryID;
                        product.UnitPrice = productEntity.UnitPrice;
                        product.Description = productEntity.Description;
                        product.Discontinued = productEntity.Discontinued;
                        product.Stocks = productEntity.Stocks;
                        product.CreationUser = productEntity.CreationUser;
                        product.CreationDateTime = productEntity.CreationDateTime;
                        product.LastUpdateUser = productEntity.LastUpdateUser;
                        product.LastUpdateDateTime = productEntity.LastUpdateDateTime;
                        product.Timestamp = productEntity.Timestamp;
                        
                        _unitOfWork.ProductRepository.Update(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

    }
}
