using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Koleksi.Domain;
using Koleski.Api.Work.CollectionTasks;

namespace Koleski.Api.Controllers
{
    public class CollectionController : ApiController
    {
        public IEnumerable<Collection> Get()
        {
            ICollectionRetriever retriver = new CollectionRetriever();
            return retriver.GetCollections();
        }
        
        public Collection Get(int id)
        {
            ICollectionRetriever retriver = new CollectionRetriever();
            return retriver.GetCollection(id);
        }
        
        public void Post(Collection item)
        {
            ICollectionSaver saver = new CollectionSaver();
            saver.SaveCollection(item);
        }

        public void Put(Collection item)
        {
            ICollectionSaver saver = new CollectionSaver();
            saver.SaveCollection(item);
        }

        public void Delete(int id)
        {
            ICollectionSaver saver = new CollectionSaver();
            saver.DeleteCollection(id);
        }
    }
}
