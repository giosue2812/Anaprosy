using Anaprosy.API.Services;
using Anaprosy.Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anaprosy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService Service;

        public InventoryController(InventoryService service)
        {
            this.Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Gets(string Include = null)
        {
            try
            {
                IEnumerable<InventoryVM> Inventories = await this.Service.Gets(Include);
                return Ok(Inventories);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> Get(Guid ID,string Include = null)
        {
            try
            {
                InventoryVM Inventory = await this.Service.Get(ID, Include);
                return Ok(Inventory);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(InventoryVM Inventory)
        {
            try
            {
                InventoryVM SavedInventory = await this.Service.Add(Inventory);
                return Ok(SavedInventory);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(InventoryVM Inventory)
        {
            try
            {
                InventoryVM UpdateInventory = await this.Service.Put(Inventory);
                return Ok(UpdateInventory);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> Delete(Guid ID)
        {
            try
            {
                bool IsDeleted = await Service.Delete(ID);
                return Ok(IsDeleted);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
