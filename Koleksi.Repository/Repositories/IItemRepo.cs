using System.Collections.Generic;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface IItemRepo
    {
        void DeleteItem(int itemID);
        ItemDTO GetItem(int itemID);
        List<ItemDTO> GetItems(int collectionID);
        ItemDTO InsertItem(ItemDTO item);
        ItemDTO UpdateItem(ItemDTO item);
    }
}