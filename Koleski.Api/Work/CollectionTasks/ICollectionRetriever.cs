using System.Collections.Generic;
using Koleksi.Domain;

namespace Koleski.Api.Work.CollectionTasks
{
    public interface ICollectionRetriever
    {
        Collection GetCollection(int id);
        List<Collection> GetCollections();
    }
}