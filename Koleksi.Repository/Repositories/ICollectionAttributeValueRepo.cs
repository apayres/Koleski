using System.Collections.Generic;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface ICollectionAttributeValueRepo
    {
        void DeleteCollectionAttributeValue(int collectionAttributeValueID);
        List<CollectionAttributeValueDTO> GetCollectionAttributeValues(int collectionAttributeID);
        CollectionAttributeValueDTO InsertCollectionAttributeValue(CollectionAttributeValueDTO item);
        CollectionAttributeValueDTO UpdateCollectionAttributeValue(CollectionAttributeValueDTO item);
    }
}