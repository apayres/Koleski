namespace Koleksi.Repository.DataTranserObjects
{
    public class ItemDTO
    {
        public int? ItemID { set; get; }

        public string Name { set; get; }

        public string LongName { set; get; }

        public string Comment { set; get; }

        public int DisplayOrder { set; get; }
    }
}
