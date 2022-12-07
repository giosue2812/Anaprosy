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
    public class ProductController : ControllerBase
    {
        private readonly ProductService Service;

        public ProductController(ProductService service)
        {
            this.Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Gets(string Include = null)
        {
            try
            {
                IEnumerable<ProductVM> Products = await this.Service.Gets(Include);
                return Ok(Products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetsByValue/{value}")]
        public async Task<IActionResult> GetsByValue(string value)
        {
            try
            {
                IEnumerable<ProductVM> Products = new List<ProductVM>();
                if (string.IsNullOrWhiteSpace(value))
                    return await Gets();

                else
                {
                    Products = await this.Service.GetsByValue(value);
                    return Ok(Products);
                }
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
                ProductVM Product = await this.Service.Get(ID, Include);
                return Ok(Product);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductVM Product)
        {
            try
            {
                ProductVM SavedInventory = await this.Service.Add(Product);
                return Ok(SavedInventory);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("ID")]
        public async Task<IActionResult> Put(Guid ID,ProductVM Product)
        {
            try
            {
                Product.Id = ID;
                ProductVM UpdateProduct = await this.Service.Put(Product);
                return Ok(UpdateProduct);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("ID")]
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
