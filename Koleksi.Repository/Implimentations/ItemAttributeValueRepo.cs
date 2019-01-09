using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Koleksi.Repository.Implimentations
{
    public class ItemAttributeValueRepo : IItemAttributeValueRepo
    {
        public List<ItemAttributeValueDTO> GetItemAttributeValues(int ItemAttributeID)
        {
            List<ItemAttributeValueDTO> items = new List<ItemAttributeValueDTO>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT [ItemAttributeID], [ItemAttributeValueID], [Value], [ItemID] FROM [dbo].[ItemAttributeValue] WITH (NOLOCK) WHERE [ItemAttributeID] = @ItemAttributeID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("ItemAttributeID", ItemAttributeID);

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

        public ItemAttributeValueDTO InsertItemAttributeValue(ItemAttributeValueDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[ItemAttributeValue] ([ItemAttributeID], [ItemID], [Value]) VALUES (@ItemAttributeID, @ItemID, @Value); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ItemAttributeID", item.ItemAttributeID);
                    command.Parameters.AddWithValue("@ItemID", item.ItemID);
                    command.Parameters.AddWithValue("@Value", item.Value);
                    item.ItemAttributeValueID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public ItemAttributeValueDTO UpdateItemAttributeValue(ItemAttributeValueDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UPDATE [dbo].[ItemAttributeValue] SET [Value] = @Value WHERE [ItemAttributeValueID] = @ItemAttributeValueID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Value", item.Value);
                    command.Parameters.AddWithValue("@ItemAttributeValueID", item.ItemAttributeValueID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public void DeleteItemAttributeValue(int ItemAttributeValueID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[ItemAttributeValue] WHERE [ItemAttributeValueID] = @ItemAttributeValueID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ItemAttributeValueID", ItemAttributeValueID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        private static ItemAttributeValueDTO Populate(SqlDataReader reader)
        {
            ItemAttributeValueDTO obj = new ItemAttributeValueDTO()
            {
                ItemAttributeID = reader.GetInt32(reader.GetOrdinal("ItemAttributeID")),
                ItemAttributeValueID = reader.GetInt32(reader.GetOrdinal("ItemAttributeValueID")),
                ItemID = reader.GetInt32(reader.GetOrdinal("ItemID")),
                Value = reader.GetString(reader.GetOrdinal("Value"))
            };

            return obj;
        }
    }
}
