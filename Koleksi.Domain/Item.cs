using System.Collections.Generic;

namespace Koleksi.Domain
{
    public class Item
    {
        public int? ItemID { set; get; }

        public string Name { set; get; }

        public string LongDescription { set; get; }

        public string Comments { set; get; }

        public int DisplayOrder { set; get; }

        public List<Image> Images { set; get; }

        public List<Attribute> Attributes { set; get; }
    }
}
