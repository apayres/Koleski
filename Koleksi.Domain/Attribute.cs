using System.Collections.Generic;

namespace Koleksi.Domain
{
    public class Attribute
    {
        public int? AttributeID { set; get; }

        public string Name { set; get; }

        public AttributeDataType DataType { set; get; }

        public List<AttributeValue> Options { set; get; }

        public string Value { set; get; }
    }
}
