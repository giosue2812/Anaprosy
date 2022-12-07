using Anaprosy.API.Services.Interfaces;
using Anaprosy.Client.Models;
using Anaprosy.DAL.Repository;
using Anaprosy.Data.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Anaprosy.API.Services
{
    public class ProductService : IService<ProductVM>
    {
        private readonly ProductRepository Repository;
        private readonly IMapper Mapper;

        public ProductService(ProductRepository repository, IMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Return a list od product
        /// </summary>
        /// <param name="include">If you need child</param>
        /// <returns>IEnumerable<ProductVM></returns>
        public async Task<IEnumerable<ProductVM>> Gets(string include = null)
        {
            try
            {
                IEnumerable<ProductDM> Products = await Repository.Gets(include);
                return Products.Select(i => Mapper.Map<ProductVM>(i));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Return a list of product
        /// </summary>
        /// <param name="value">Value to return a list of product by value</param>
        /// <returns>IEnumerable<ProductVM></returns>
        public async Task<IEnumerable<ProductVM>> GetsByValue(string value)
        {
            try
            {
                IEnumerable<ProductDM> Products = await Repository.GetsByValue(value);
                return Products.Select(i => Mapper.Map <ProductVM>(i));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Return a prdocut
        /// </summary>
        /// <param name="ID">Id of product to found</param>
        /// <param name="include">If you need child</param>
        /// <returns>ProductVM</returns>
        public async Task<ProductVM> Get(Guid ID,string include = null)
        {
            try
            {
                ProductDM Product = await Repository.Get(ID, include);
                return Mapper.Map<ProductVM>(Product);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Add a product
        /// </summary>
        /// <param name="ProductVM">Product to add</param>
        /// <returns>Save ProductVM</returns>
        public async Task<ProductVM> Add(ProductVM ProductVM)
        {
            try
            {
                if (ProductVM == null)
                    throw new Exception($"{ProductVM} is null");

                ProductDM ProductDM = Mapper.Map<ProductDM>(ProductVM);
                ProductDM = await this.Repository.Add(ProductDM);

                // Return the InventoryVM
                ProductVM = Mapper.Map(ProductDM, ProductVM);

                return ProductVM;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="ProductVM">Product to update</param>
        /// <returns>Updated ProductVM</returns>
        public async Task<ProductVM> Put(ProductVM ProductVM)
        {
            try
            {
                ProductDM ProductDM = await this.Repository.Get(ProductVM.Id);

                if (ProductDM == null)
                    throw new Exception($"{ProductDM} is null");

                ProductDM = Mapper.Map<ProductDM>(ProductVM);
                ProductDM = await this.Repository.Put(ProductDM);

                // Return the InventoryVM
                ProductVM = Mapper.Map<ProductVM>(ProductDM);

                return ProductVM;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="ID">Id of product to be delete</param>
        /// <returns>true id product is deleted</returns>
        public async Task<bool> Delete(Guid ID)
        {
            try
            {
                return await Repository.Delete(ID);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
