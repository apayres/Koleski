namespace Koleksi.Repository.DataTranserObjects
{
    public class CollectionImageDTO
    {
        public int? CollectionImageID { set; get; }

        public string ImagePath { set; get; }

        public string Caption { set; get; }

        public int DisplayOrder { set; get; }

        public int CollectionID { set; get; }
    }
}
