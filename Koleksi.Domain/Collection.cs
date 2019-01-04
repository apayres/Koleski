using System.Collections.Generic;

namespace Koleksi.Domain
{
    public class Collection
    {
        public int? CollectionID { set; get; }

        public string Name { set; get; }

        public string LongDescription { set; get; }

        public List<Collection> Collections { set; get; }

        public List<Item> Items { set; get; }

        public List<Attribute> Attributes { set; get; }
        
        public List<Image> Images { set; get; }
    }
}
