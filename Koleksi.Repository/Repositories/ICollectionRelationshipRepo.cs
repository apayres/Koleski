using System.Collections.Generic;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface ICollectionRelationshipRepo
    {
        void DeleteCollectionRelationships(int parentCollectionID);
        void DeleteCollectionRelationshipsByChild(int collectionID);
        List<CollectionRelationshipDTO> GetCollectionRelationships(int parentCollectionID);
        CollectionRelationshipDTO InsertCollectionRelationship(CollectionRelationshipDTO item);
    }
}