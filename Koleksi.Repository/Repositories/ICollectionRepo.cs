using System.Collections.Generic;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface ICollectionRepo
    {
        void DeleteCollection(int collectionID);
        List<CollectionDTO> GetCollections();
        CollectionDTO GetCollection(int collectionID);
        List<CollectionDTO> GetCollections(int? parentCollectionID);
        CollectionDTO InsertCollection(CollectionDTO item);
        CollectionDTO UpdateCollection(CollectionDTO item);
    }
}