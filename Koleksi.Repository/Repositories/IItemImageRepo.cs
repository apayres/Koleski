using System.Collections.Generic;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface IItemImageRepo
    {
        void DeleteItemImage(int itemImageID);
        void DeleteItemImages(int itemID);
        List<ItemImageDTO> GetItemImages(int itemID);
        ItemImageDTO InsertItemImage(ItemImageDTO item);
        ItemImageDTO UpdateCollectionImage(ItemImageDTO item);
    }
}