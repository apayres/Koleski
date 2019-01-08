using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Koleksi.Domain;
using Koleksi.Services;
using Koleski.Api.Work;

namespace Koleski.Api.Work.CollectionTasks
{
    public class CollectionRetriever : ICollectionRetriever
    {
        public List<Collection> GetCollections()
        {
            return new List<Collection>();
        }

        public Collection GetCollection(int id)
        {
            return null;
        }
    }
}