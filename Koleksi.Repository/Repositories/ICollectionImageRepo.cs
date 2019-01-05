using System.Collections.Generic;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface ICollectionImageRepo
    {
        void DeleteCollectionImage(int collectionImageID);
        void DeleteCollectionImages(int collectionID);
        List<CollectionImageDTO> GetCollectionImages(int collectionID);
        CollectionImageDTO InsertCollectionImage(CollectionImageDTO item);
        CollectionImageDTO UpdateCollectionImage(CollectionImageDTO item);
    }
}