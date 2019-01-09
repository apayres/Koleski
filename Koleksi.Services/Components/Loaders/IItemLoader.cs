using System.Collections.Generic;
using Koleksi.Domain;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Services.Components.Loaders
{
    public interface IItemLoader
    {
        Item LoadItem(ItemDTO itemDTO, bool loadExtras);
        Item LoadItem(int itemID, bool loadExtras);
        List<Item> LoadItems(List<ItemDTO> itemDTOs, bool loadExtras);
        List<Item> LoadItems(int collectionID, bool loadExtras);
    }
}