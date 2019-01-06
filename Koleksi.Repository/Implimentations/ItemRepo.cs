using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Koleksi.Repository.Implimentations
{
    public class ItemRepo : IItemRepo
    {
        public List<ItemDTO> GetItems(int collectionID)
        {
            List<ItemDTO> items = new List<ItemDTO>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT i.[ItemID], i.[Name], i.[LongName], i.[Comment], i.[DisplayOrder] FROM [dbo].[Item] i WITH (NOLOCK) INNER JOIN [dbo].[ItemToCollection] rel ON rel.ItemID = i.ItemID WHERE rel.CollectionID = @CollectionID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionID", collectionID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(Populate(reader));
                        }
                    }
                }

                connection.Close();
                connection.Dispose();
            }

            return items;
        }

        public ItemDTO GetItem(int itemID)
        {
            ItemDTO item = null;
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT i.[ItemID], i.[Name], i.[LongName], i.[Comment], i.[DisplayOrder] FROM [dbo].[Item] i WITH (NOLOCK) WHERE i.ItemID = @ItemID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ItemID", itemID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item = Populate(reader);
                        }
                    }
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }
        
        public ItemDTO InsertItem(ItemDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[Item] ([Name], [LongName], [Comment], [DisplayOrder]) VALUES (@Name, @LongName, @Comment, @DisplayOrder); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@LongName", item.LongName);
                    command.Parameters.AddWithValue("@Comment", item.Comment);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    item.ItemID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public ItemDTO UpdateItem(ItemDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UPDATE [dbo].[Item] SET [Name] = @Name, [LongName] = @LongName, [Comment] = @Comment, [DisplayOrder] = @DisplayOrder WHERE [ItemID] = @ItemID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@LongName", item.LongName);
                    command.Parameters.AddWithValue("@Comment", item.Comment);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    command.Parameters.AddWithValue("@ItemID", item.ItemID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public void DeleteItem(int itemID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" 
                        DELETE FROM [ItemAttributeValue] WHERE ItemID = @ItemID
                        DELETE FROM [ItemImage] WHERE ItemID = @ItemID
                        DELETE FROM [ItemToCollection] WHERE ItemID = @ItemID
                        DELETE FROM [Item] WHERE ItemID = @ItemID
                    ";

                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ItemID", itemID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        private static ItemDTO Populate(SqlDataReader reader)
        {
            ItemDTO obj = new ItemDTO()
            {
                DisplayOrder = reader.GetInt32(reader.GetOrdinal("DisplayOrder")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                LongName = reader.GetString(reader.GetOrdinal("LongName")),
                Comment = reader.GetString(reader.GetOrdinal("Comment")),
                ItemID = reader.GetInt32(reader.GetOrdinal("ItemID")),
            };

            return obj;
        }
    }
}
