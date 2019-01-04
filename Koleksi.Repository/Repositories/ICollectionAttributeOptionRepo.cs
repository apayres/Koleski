using System.Collections.Generic;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface ICollectionAttributeOptionRepo
    {
        void DeleteCollectionAttributeOption(int collectionAttributeOptionID);
        List<CollectionAttributeOptionDTO> GetCollectionAttributeOptions(int collectionAttributeID);
        CollectionAttributeOptionDTO InsertCollectionAttributeOption(CollectionAttributeOptionDTO item);
        CollectionAttributeOptionDTO UpdateCollectionAttributeOption(CollectionAttributeOptionDTO item);
    }
}