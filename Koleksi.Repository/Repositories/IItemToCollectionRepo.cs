using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface IItemToCollectionRepo
    {
        void DeleteItemToCollection(int collectionID, int itemID);
        ItemToCollectionDTO InsertItemToCollection(ItemToCollectionDTO item);
    }
}