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
    public class InventoryService : IService<InventoryVM>
    {
        private readonly InventoryRepository Repository;
        private readonly IMapper Mapper;

        public InventoryService(InventoryRepository repository, IMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Return a list of inventory
        /// </summary>
        /// <param name="include">If you need child of object</param>
        /// <returns>IEnumerable<InventoryVM></returns>
        public async Task<IEnumerable<InventoryVM>> Gets(string include = null)
        {
            try
            {
                IEnumerable<InventoryDM> Inventories = await Repository.Gets(include);
                return Inventories.Select(i => Mapper.Map<InventoryVM>(i));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Return a object inventory
        /// </summary>
        /// <param name="ID">Guid Id to found inventory</param>
        /// <param name="include">If you need child of object</param>
        /// <returns>InventoryVM</returns>
        public async Task<InventoryVM> Get(Guid ID,string include = null)
        {
            try
            {
                InventoryDM Inventory = await Repository.Get(ID, include);
                return Mapper.Map<InventoryVM>(Inventory);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Add inventory and child
        /// </summary>
        /// <param name="InventoryVM">Inventory to add</param>
        /// <returns>Saved InventoryVM</returns>
        public async Task<InventoryVM> Add(InventoryVM InventoryVM)
        {
            try
            {
                if (InventoryVM == null)
                    throw new Exception($"{InventoryVM} is null");

                // Set the "ProductId"
                if(InventoryVM.Items != null && InventoryVM.Items.Count() > 0)
                {
                    InventoryVM.Items.ForEach(i => { i.ProductId = i.Product.Id;});
                    InventoryVM.Items.ForEach(i => i.Product = null);
                }

                InventoryDM InventoryDM = Mapper.Map<InventoryDM>(InventoryVM);
                InventoryDM = await this.Repository.Add(InventoryDM);

                // Return the InventoryVM
                InventoryVM = Mapper.Map(InventoryDM, InventoryVM);

                return InventoryVM;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Update inventory and child
        /// </summary>
        /// <param name="InventoryVM">Inventory to update</param>
        /// <returns>InventoryVM</returns>
        public async Task<InventoryVM> Put(InventoryVM InventoryVM)
        {
            try
            {
                if (InventoryVM == null)
                    throw new Exception($"{InventoryVM} is null");

                // Set the "ProductId"
                if (InventoryVM.Items != null && InventoryVM.Items.Count() > 0)
                {
                    InventoryVM.Items.ForEach(i => i.ProductId = i.Product.Id);
                    InventoryVM.Items.ForEach(i => i.Product = null);
                }

                InventoryDM InventoryDM = Mapper.Map<InventoryDM>(InventoryVM);
                InventoryDM = await this.Repository.Put(InventoryDM);

                // Return the InventoryVM
                InventoryVM = Mapper.Map<InventoryVM>(InventoryDM);

                return InventoryVM;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete inventory
        /// </summary>
        /// <param name="ID">Guid Id to found inventory to delete</param>
        /// <returns>true is inventory is deleted</returns>
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
