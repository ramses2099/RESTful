using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;


namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {

        private readonly IProductServices _productServices;

        public ProductController()
        {
            _productServices = new ProductServices();
        }


        public HttpResponseMessage Get() {
            var products = _productServices.GetAllProducts();

            if (products != null)
            {
                var productEntities = products as List<ProductEntity> ?? products.ToList();
                if (products.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, products);

            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");

        }
                
        public HttpResponseMessage Get(int ProductID)
        {
            var product = _productServices.GetProductById(ProductID);
            if (product != null)
                return Request.CreateResponse(HttpStatusCode.OK, product);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found for this ProductID");
        }

        
        public int Post([FromBody] ProductEntity productEntity)
        {
            return _productServices.CreateProduct(productEntity);
        }

        
        public bool Put(int ProductID, [FromBody]ProductEntity productEntity)
        {
            if (ProductID > 0)
            {
                return _productServices.UpdateProduct(ProductID, productEntity);
            }
            return false;
        }

        
        public bool Delete(int ProductID)
        {
            if (ProductID > 0)
                return _productServices.DeleteProduct(ProductID);
            return false;
        }

    }
}
