namespace Koleksi.Repository.DataTranserObjects
{
    public class CollectionRelationshipDTO
    {
        public int? CollectionRelationshipID { set; get; }

        public int ParentCollectionID { set; get; }

        public int CollectionID { set; get; }
    }
}
