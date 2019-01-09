using Koleksi.Domain;
using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Implimentations;
using Koleksi.Repository.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Koleksi.Services.Components.Loaders
{
    public class ItemLoader : IItemLoader
    {
        private readonly IItemRepo _itemRepo;
        private readonly IAttributeLoader _attributeLoader;
        private readonly IImageLoader _imageLoader;

        public ItemLoader(): this(new ItemRepo(), new ItemAttributeLoader(), new ItemImageLoader())
        {

        }

        public ItemLoader(IItemRepo repo, IAttributeLoader attributeLoader, IImageLoader imageLoader)
        {
            _itemRepo = repo;
            _attributeLoader = attributeLoader;
            _imageLoader = imageLoader;
        }

        public List<Item> LoadItems(int collectionID, bool loadExtras)
        {
            List<ItemDTO> itemDTOs = _itemRepo.GetItems(collectionID);
            return LoadItems(itemDTOs, loadExtras);
        }

        public List<Item> LoadItems(List<ItemDTO> itemDTOs, bool loadExtras)
        {
            return itemDTOs.Select(x => LoadItem(x, loadExtras)).ToList();
        }

        public Item LoadItem(int itemID, bool loadExtras)
        {
            ItemDTO itemDTO = _itemRepo.GetItem(itemID);
            if(itemDTO == null)
            {
                return null;
            }

            return LoadItem(itemDTO, loadExtras);
        }

        public Item LoadItem(ItemDTO itemDTO, bool loadExtras)
        {
            Item item = new Item();
            item.Comments = itemDTO.Comment;
            item.DisplayOrder = itemDTO.DisplayOrder;
            item.ItemID = itemDTO.ItemID;
            item.LongDescription = itemDTO.LongName;
            item.Name = itemDTO.Name;

            if (loadExtras)
            {
                item.Attributes = _attributeLoader.LoadAttributes(item.ItemID.Value);
                item.Images = _imageLoader.LoadImages(item.ItemID.Value);
            }

            return item;
        }
    }
}
