﻿namespace Koleksi.Repository.DataTranserObjects
{
    public class ItemAttributeOptionDTO
    {
        public int? itemAttributeOptionID { set; get; }

        public string DisplayLabel { set; get; }

        public string Value { set; get; }

        public int DisplayOrder { set; get; }

        public int ItemAttributeID { set; get; }
    }
}
