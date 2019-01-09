using Koleksi.Domain;
using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Implimentations;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Koleksi.Services.Components.Loaders
{
    public class ItemAttributeLoader : IAttributeLoader
    {
        private readonly IItemAttributeRepo _itemAttributeRepo;
        private readonly IItemAttributeValueRepo _itemAttributeValueRepo;
        private readonly IItemAttributeOptionRepo _itemAttributeOptionRepo;

        public ItemAttributeLoader() : this(new ItemAttributeRepo(), new ItemAttributeValueRepo(), new ItemAttributeOptionRepo())
        {
        }

        public ItemAttributeLoader(IItemAttributeRepo itemAttributeRepo, IItemAttributeValueRepo itemAttributeValueRepo, IItemAttributeOptionRepo itemAttributeOptionRepo)
        {
            _itemAttributeRepo = itemAttributeRepo;
            _itemAttributeOptionRepo = itemAttributeOptionRepo;
            _itemAttributeValueRepo = itemAttributeValueRepo;
        }

        public List<Domain.Attribute> LoadAttributes(int itemID)
        {
            List<Domain.Attribute> items = new List<Domain.Attribute>();
            List<ItemAttributeDTO> attributeDTOs = _itemAttributeRepo.GetItemAttributes();
            foreach (ItemAttributeDTO dto in attributeDTOs)
            {
                List<ItemAttributeOptionDTO> optionDTOs = _itemAttributeOptionRepo.GetItemAttributeOptions(dto.ItemAttributeID.Value);
                List<ItemAttributeValueDTO> attributeValueDTOs = _itemAttributeValueRepo.GetItemAttributeValues(dto.ItemAttributeID.Value);

                Domain.Attribute attribute = new Domain.Attribute();
                attribute.AttributeID = dto.ItemAttributeID;
                attribute.DataType = (AttributeDataType)Enum.Parse(typeof(AttributeDataType), dto.DataType);
                attribute.Name = dto.Name;

                attribute.Options = optionDTOs.Select(x => new AttributeValue()
                {
                    AttributeOptionID = x.ItemAttributeOptionID,
                    DefaultValue = x.Value,
                    DisplayOrder = x.DisplayOrder,
                    Label = x.DisplayLabel
                }).ToList();

                attribute.Values = attributeValueDTOs.Select(x => (object)x.Value).ToList();
                items.Add(attribute);
            }

            return items;
        }
    }
}
