using System.Collections.Generic;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface IItemAttributeOptionRepo
    {
        void DeleteItemAttributeOption(int ItemAttributeOptionID);
        List<ItemAttributeOptionDTO> GetItemAttributeOptions(int ItemAttributeID);
        ItemAttributeOptionDTO InsertItemAttributeOption(ItemAttributeOptionDTO item);
        ItemAttributeOptionDTO UpdateItemAttributeOption(ItemAttributeOptionDTO item);
    }
}