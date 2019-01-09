using Koleksi.Domain;
using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Implimentations;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Koleksi.Services.Components.Loaders
{
    public class CollectionAttributeLoader : IAttributeLoader
    {
        private readonly ICollectionAttributeOptionRepo _collectionAttributeOptionRepo;
        private readonly ICollectionAttributeRepo _collectionAttributeRepo;
        private readonly ICollectionAttributeValueRepo _collectionAttributeValueRepo;
        
        public CollectionAttributeLoader() : this(new CollectionAttributeOptionRepo(), new CollectionAttributeRepo(), new CollectionAttributeValueRepo())
        {

        }

        public CollectionAttributeLoader(ICollectionAttributeOptionRepo collectionAttributeOptionRepo, ICollectionAttributeRepo collectionAttributeRepo, ICollectionAttributeValueRepo collectionAttributeValueRepo)
        {
            _collectionAttributeOptionRepo = collectionAttributeOptionRepo;
            _collectionAttributeRepo = collectionAttributeRepo;
            _collectionAttributeValueRepo = collectionAttributeValueRepo;
        }

        public List<Domain.Attribute> LoadAttributes(int sourceID)
        {
            List<Domain.Attribute> items = new List<Domain.Attribute>();
            List<CollectionAttributeDTO> attributeDTOs = _collectionAttributeRepo.GetCollectionAttributes();
            foreach (CollectionAttributeDTO dto in attributeDTOs)
            {
                List<CollectionAttributeOptionDTO> optionDTOs = _collectionAttributeOptionRepo.GetCollectionAttributeOptions(dto.CollectionAttributeID.Value);
                List<CollectionAttributeValueDTO> attributeValueDTOs = _collectionAttributeValueRepo.GetCollectionAttributeValues(dto.CollectionAttributeID.Value);

                Domain.Attribute attribute = new Domain.Attribute();
                attribute.AttributeID = dto.CollectionAttributeID;
                attribute.DataType = (AttributeDataType)Enum.Parse(typeof(AttributeDataType), dto.DataType);
                attribute.Name = dto.Name;

                attribute.Options = optionDTOs.Select(x => new AttributeValue()
                {
                    AttributeOptionID = x.CollectionAttributeOptionID,
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
