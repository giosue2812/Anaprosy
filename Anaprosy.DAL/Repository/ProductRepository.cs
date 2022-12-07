using Anaprosy.DAL.Repository.Interfaces;
using Anaprosy.Data.Context;
using Anaprosy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anaprosy.DAL.Repository
{
    public class ProductRepository : IRepository<ProductDM>
    {
        private readonly DataContext Context;
        public ProductRepository(DataContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Return a list of Product
        /// </summary>
        /// <param name="include">If you need child</param>
        /// <returns>IEnumerable<ProductDM></returns>
        public async Task<IEnumerable<ProductDM>> Gets(string include = null)
        {
            try
            {
                if (include != null)
                    return await Context.Products.Include(include).ToListAsync();
                else
                    return await Context.Products.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Return a list of Product
        /// </summary>
        /// <param name="value">Value to return a list of product by value</param>
        /// <returns>IEnumerable<ProductDM></returns>
        public async Task<IEnumerable<ProductDM>> GetsByValue(string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("value is null");

                // Search value to get a list of products from the value
                return await Context.Products.Where(p => p.Name.ToUpper().Contains(value.ToUpper())).OrderBy(p => p.Name).ToListAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
          
        }

        /// <summary>
        /// Return product
        /// </summary>
        /// <param name="ID">Id to found the prodcut</param>
        /// <param name="include">If you need child</param>
        /// <returns>ProductDM</returns>
        public async Task<ProductDM> Get(Guid ID, string include = null)
        {
            try
            {
                if (include != null)
                    return await Context.Products.Include(include).FirstOrDefaultAsync(i => i.Id == ID);
                else
                    return await Context.Products.Include(include).FirstOrDefaultAsync(i => i.Id == ID);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="Product">Product to add</param>
        /// <returns>Saved ProductDM</returns>
        public async Task<ProductDM> Add(ProductDM Product)
        {
            try
            {
                if (Product == null)
                    throw new Exception($"{Product} is null");

                await Context.Products.AddAsync(Product);
                await Context.SaveChangesAsync();

                return Product;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="Product">Product to update</param>
        /// <returns>Update ProductDM</returns>
        public async Task<ProductDM> Put(ProductDM Product)
        {
            try
            {
                if (Product == null)
                    throw new Exception($"{Product} is null");
                
                await Context.SaveChangesAsync();

                return Product;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="ID">Id of product to be deleted</param>
        /// <returns>true if product is deleted</returns>
        public async Task<bool> Delete(Guid ID)
        {
            try
            {
                ProductDM Product = await Get(ID);
                if (Product == null)
                    throw new Exception($"{ Product } not found");

                Context.Remove(Product);
                await Context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
