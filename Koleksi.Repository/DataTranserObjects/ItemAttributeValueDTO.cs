namespace Koleksi.Repository.DataTranserObjects
{
    public class ItemAttributeValueDTO
    {
        public int? ItemAttributeValueID { set; get; }

        public int itemAttributeID { set; get; }

        public string Value { set; get; }

        public int ItemID { set; get; }
    }
}
