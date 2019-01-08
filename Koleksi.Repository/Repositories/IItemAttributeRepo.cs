using System.Collections.Generic;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface IItemAttributeRepo
    {
        void DeleteItemAttribute(int ItemAttributeID);
        List<ItemAttributeDTO> GetItemAttributes();
        ItemAttributeDTO InsertItemAttribute(ItemAttributeDTO item);
        ItemAttributeDTO UpdateItemAttribute(ItemAttributeDTO item);
    }
}