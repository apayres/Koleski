using System.Collections.Generic;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface ICollectionAttributeRepo
    {
        void DeleteCollectionAttribute(int collectionAttributeID);
        List<CollectionAttributeDTO> GetCollectionAttributes();
        CollectionAttributeDTO InsertCollectionAttribute(CollectionAttributeDTO item);
        CollectionAttributeDTO UpdateCollectionAttribute(CollectionAttributeDTO item);
    }
}