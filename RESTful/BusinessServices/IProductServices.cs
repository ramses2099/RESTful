using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices
{
    public interface IProductServices
    {
        ProductEntity GetProductById(int ProductID);
        IEnumerable<ProductEntity> GetAllProducts();
        int CreateProduct(ProductEntity productEntity);
        bool UpdateProduct(int ProductID, ProductEntity productEntity);
        bool DeleteProduct(int ProductID);
        
    }
}
