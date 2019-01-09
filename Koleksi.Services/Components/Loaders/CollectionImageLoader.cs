using Koleksi.Domain;
using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Implimentations;
using Koleksi.Repository.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Koleksi.Services.Components.Loaders
{
    public class CollectionImageLoader : IImageLoader
    {
        private readonly ICollectionImageRepo _collectionImageRepo;

        public CollectionImageLoader() : this(new CollectionImageRepo())
        {

        }

        public CollectionImageLoader(ICollectionImageRepo repo)
        {
            _collectionImageRepo = repo;
        }

        public List<Image> LoadImages(int sourceID)
        {
            List<Image> items = new List<Image>();
            List<CollectionImageDTO> imageDTOs = _collectionImageRepo.GetCollectionImages(sourceID);
            items = imageDTOs.Select(x => new Image()
            {
                Caption = x.Caption,
                DisplayOrder = x.DisplayOrder,
                ImageID = x.CollectionImageID,
                ImagePath = x.ImagePath
            }).ToList();

            return items;
        }
    }
}
