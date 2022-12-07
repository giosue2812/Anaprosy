using Anaprosy.DAL.Repository.Interfaces;
using Anaprosy.Data.Context;
using Anaprosy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anaprosy.DAL.Repository
{
    public class InventoryRepository : IRepository<InventoryDM>
    {
        private readonly DataContext Context;
        public InventoryRepository(DataContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Return a list of inventory
        /// </summary>
        /// <param name="include">If you need to incelude child</param>
        /// <returns>IEnumerable<InventoryDM></returns>
        public async Task<IEnumerable<InventoryDM>> Gets(string include = null)
        {
            try
            {
                // Check if there is a include to be
                if (include != null)
                    return await Context.Inventories.Include(include).ToListAsync();
                else
                    return await Context.Inventories.OrderBy(i => i.Date).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Return a inventory
        /// </summary>
        /// <param name="ID">Id to found the inventory</param>
        /// <param name="include">If you need to incelude child</param>
        /// <returns>InventoryDM</returns>
        public async Task<InventoryDM> Get(Guid ID, string include = null)
        {
            try
            {
                // Check if there is a include to be
                if (include != null)
                    return await Context.Inventories.Include(include).FirstOrDefaultAsync(i => i.Id == ID);
                else
                    return await Context.Inventories.FirstOrDefaultAsync(i => i.Id == ID);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Add the inventory
        /// </summary>
        /// <param name="Inventory">Inventory to add</param>
        /// <returns>InventoryDM</returns>
        public async Task<InventoryDM> Add(InventoryDM Inventory)
        {
            try
            {
                if (Inventory == null)
                    throw new Exception($"{Inventory} is null");

                await Context.Inventories.AddAsync(Inventory);
                await Context.SaveChangesAsync();

                return Inventory;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Update to update
        /// </summary>
        /// <param name="Inventory">Inventory to update</param>
        /// <returns>InventoryDM</returns>
        public async Task<InventoryDM> Put(InventoryDM Inventory)
        {
            try
            {
                if (Inventory == null)
                    throw new Exception();
                
                // Get the existing inventory will be updated
                InventoryDM SourceInventory = await Get(Inventory.Id, "Items");

                if (SourceInventory == null)
                    throw new Exception($"{Inventory} is null");

                // Check if existing inventory contains child if yes => delete child and set the id
                if (SourceInventory.Items != null)
                {
                    Inventory.Items.ForEach(i => i.Id = new Guid());
                    SourceInventory.Items.RemoveAll(i => i.InventoryId == Inventory.Id);
                    SourceInventory.Items = Inventory.Items;
                    
                }
                // Remove existing inventory
                Context.Inventories.Remove(SourceInventory);

                // Add Again inventory with informations update
                await Add(Inventory);
               
                await Context.SaveChangesAsync();
                
                return Inventory;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete inventory
        /// </summary>
        /// <param name="ID">Id of inventory to deleted</param>
        /// <returns>bool</returns>
        public async Task<bool> Delete(Guid ID)
        {
            try
            {
                // Get existing inventory
                InventoryDM Inventory = await Get(ID,"Items");

                if (Inventory == null)
                    throw new Exception($"{ Inventory } not found");

                //Check if existing inventory contains child if yes => delete child
                if (Inventory.Items != null && Inventory.Items.Count() > 0)
                {
                    Inventory.Items.RemoveAll(i => i.InventoryId == ID);
                }

                var DeletedInventory =  Context.Remove(Inventory);
                await Context.SaveChangesAsync();
                
                if (DeletedInventory.State == EntityState.Deleted || DeletedInventory.State == EntityState.Detached)
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
