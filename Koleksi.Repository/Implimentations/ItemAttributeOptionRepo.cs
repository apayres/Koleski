using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Koleksi.Repository.Implimentations
{
    public class ItemAttributeOptionRepo : IItemAttributeOptionRepo
    {
        public List<ItemAttributeOptionDTO> GetItemAttributeOptions(int ItemAttributeID)
        {
            List<ItemAttributeOptionDTO> items = new List<ItemAttributeOptionDTO>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT [ItemAttributeOptionID], [DisplayLabel], [Value], [DisplayOrder], [ItemAttributeID] FROM [dbo].[ItemAttributeOption] WITH (NOLOCK) WHERE [ItemAttributeID] = @ItemAttributeID";
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

        public ItemAttributeOptionDTO InsertItemAttributeOption(ItemAttributeOptionDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[ItemAttributeOption] ([DisplayLabel], [Value], [DisplayOrder], [ItemAttributeID]) VALUES (@DisplayLabel, @Value, @DisplayOrder, @ItemAttributeID); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@DisplayLabel", item.DisplayLabel);
                    command.Parameters.AddWithValue("@Value", item.Value);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    command.Parameters.AddWithValue("@ItemAttributeID", item.ItemAttributeID);
                    item.ItemAttributeOptionID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public ItemAttributeOptionDTO UpdateItemAttributeOption(ItemAttributeOptionDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UPDATE [dbo].[ItemAttributeOption] SET [DisplayLabel] = @DisplayLabel, [Value] = @Value, [DisplayOrder] = @DisplayOrder WHERE [ItemAttributeOptionID] = @ItemAttributeOptionID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@DisplayLabel", item.DisplayLabel);
                    command.Parameters.AddWithValue("@Value", item.Value);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    command.Parameters.AddWithValue("@ItemAttributeOptionID", item.ItemAttributeOptionID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public void DeleteItemAttributeOption(int ItemAttributeOptionID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[ItemAttributeOption] WHERE [ItemAttributeOptionID] = @ItemAttributeOptionID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ItemAttributeOptionID", ItemAttributeOptionID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        private static ItemAttributeOptionDTO Populate(SqlDataReader reader)
        {
            ItemAttributeOptionDTO obj = new ItemAttributeOptionDTO()
            {
                ItemAttributeID = reader.GetInt32(reader.GetOrdinal("ItemAttributeID")),
                ItemAttributeOptionID = reader.GetInt32(reader.GetOrdinal("ItemAttributeOptionID")),
                DisplayLabel = reader.GetString(reader.GetOrdinal("DisplayLabel")),
                DisplayOrder = reader.GetInt32(reader.GetOrdinal("DisplayOrder")),
                Value = reader.GetString(reader.GetOrdinal("Value"))
            };

            return obj;
        }
    }
}
