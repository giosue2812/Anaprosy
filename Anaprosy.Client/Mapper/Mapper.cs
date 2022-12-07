using Anaprosy.Client.Models;
using Anaprosy.Data.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anaprosy.Client.Mapper
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;


            CreateMap<InventoryDM, InventoryVM>().ReverseMap();
            CreateMap<InventoryItemDM, InventoryItemVM>().ReverseMap();
            CreateMap<ProductDM, ProductVM>().ReverseMap();
            
        }
    }
}
