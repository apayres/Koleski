using Koleksi.Domain;
using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Implimentations;
using Koleksi.Repository.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Koleksi.Services.Components.Loaders
{
    public class CollectionLoader : ICollectionLoader
    {
        private readonly ICollectionRepo _collectionRepo;
        private readonly ICollectionRelationshipRepo _collectionRelationshipRepo;
        private readonly IAttributeLoader _attributeLoader;
        private readonly IImageLoader _imageLoader;
        private readonly IItemLoader _itemLoader;

        public CollectionLoader() : this(new CollectionRepo(), new CollectionRelationshipRepo(), new CollectionAttributeLoader(), new CollectionImageLoader(), new ItemLoader())
        {

        }

        public CollectionLoader(ICollectionRepo collectionRepo, ICollectionRelationshipRepo collectionRelationshipRepo, IAttributeLoader attributeLoader, IImageLoader imageLoader, IItemLoader itemLoader)
        {
            _collectionRelationshipRepo = collectionRelationshipRepo;
            _collectionRepo = collectionRepo;
            _attributeLoader = attributeLoader;
            _imageLoader = imageLoader;
            _itemLoader = itemLoader;
        }

        public Collection LoadCollection(int collectionID, bool includeItems)
        {
            CollectionDTO collectionDTO = _collectionRepo.GetCollection(collectionID);
            if(collectionDTO == null)
            {
                return null;
            }

            Collection obj = new Collection();
            obj.CollectionID = collectionDTO.CollectionID;
            obj.Name = collectionDTO.Name;
            obj.LongDescription = collectionDTO.Description;
            obj.Images = _imageLoader.LoadImages(collectionDTO.CollectionID.Value);
            obj.Attributes = _attributeLoader.LoadAttributes(collectionDTO.CollectionID.Value);

            if (includeItems)
            {
                obj.Items = _itemLoader.LoadItems(collectionDTO.CollectionID.Value, true);
            }
            
            List<CollectionRelationshipDTO> children = _collectionRelationshipRepo.GetCollectionRelationships(obj.CollectionID.Value);
            obj.Collections = children.Select(x => LoadCollection(x.CollectionID, includeItems)).ToList();
            return obj;
        }
    }
}
