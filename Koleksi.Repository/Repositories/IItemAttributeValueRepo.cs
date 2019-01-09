using System.Collections.Generic;
using Koleksi.Repository.DataTranserObjects;

namespace Koleksi.Repository.Repositories
{
    public interface IItemAttributeValueRepo
    {
        void DeleteItemAttributeValue(int ItemAttributeValueID);
        List<ItemAttributeValueDTO> GetItemAttributeValues(int ItemAttributeID);
        ItemAttributeValueDTO InsertItemAttributeValue(ItemAttributeValueDTO item);
        ItemAttributeValueDTO UpdateItemAttributeValue(ItemAttributeValueDTO item);
    }
}