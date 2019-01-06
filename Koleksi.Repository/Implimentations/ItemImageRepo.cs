using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Koleksi.Repository.Implimentations
{
    public class ItemImageRepo : IItemImageRepo
    {
        public List<ItemImageDTO> GetItemImages(int itemID)
        {
            List<ItemImageDTO> items = new List<ItemImageDTO>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT [ItemImageID], [ItemID], [ImagePath], [Caption], [DisplayOrder] FROM [dbo].[ItemImage] WITH (NOLOCK) WHERE [ItemID] = @ItemID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("ItemID", itemID);

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

        public ItemImageDTO InsertItemImage(ItemImageDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[ItemImage] ([ItemID], [ImagePath], [Caption], [DisplayOrder]) VALUES (@ItemID, @ImagePath, @Caption, @DisplayOrder); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ItemID", item.ItemID);
                    command.Parameters.AddWithValue("@ImagePath", item.ImagePath);
                    command.Parameters.AddWithValue("@Caption", item.Caption);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    item.ItemImageID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public ItemImageDTO UpdateCollectionImage(ItemImageDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UPDATE [dbo].[ItemImage] SET [Caption] = @Caption, [DisplayOrder] = @DisplayOrder WHERE [ItemImageID] = @ItemImageID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Caption", item.Caption);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    command.Parameters.AddWithValue("@ItemImageID", item.ItemImageID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public void DeleteItemImage(int itemImageID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[ItemImage] WHERE [ItemImageID] = @ItemImageID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ItemImageID", itemImageID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        public void DeleteItemImages(int itemID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[ItemImage] WHERE [ItemID] = @ItemID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ItemID", itemID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        private static ItemImageDTO Populate(SqlDataReader reader)
        {
            ItemImageDTO obj = new ItemImageDTO()
            {
                ItemID = reader.GetInt32(reader.GetOrdinal("ItemID")),
                ItemImageID = reader.GetInt32(reader.GetOrdinal("ItemImageID")),
                DisplayOrder = reader.GetInt32(reader.GetOrdinal("DisplayOrder")),
                Caption = reader.GetString(reader.GetOrdinal("Caption")),
                ImagePath = reader.GetString(reader.GetOrdinal("ImagePath"))
            };

            return obj;
        }
    }
}
