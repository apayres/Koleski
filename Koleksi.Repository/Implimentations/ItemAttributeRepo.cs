using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Koleksi.Repository.Implimentations
{
    public class ItemAttributeRepo : IItemAttributeRepo
    {
        public List<ItemAttributeDTO> GetItemAttributes()
        {
            List<ItemAttributeDTO> items = new List<ItemAttributeDTO>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT [ItemAttributeID], [Name], [DataType] FROM [dbo].[ItemAttribute] WITH (NOLOCK)";
                    command.CommandType = CommandType.Text;
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

        public ItemAttributeDTO InsertItemAttribute(ItemAttributeDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[ItemAttribute] ([Name], [DataType]) VALUES (@Name, @DataType); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@DataType", item.DataType);
                    item.ItemAttributeID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public ItemAttributeDTO UpdateItemAttribute(ItemAttributeDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UPDATE [dbo].[ItemAttribute] SET [Name] = @Name, [DataType] = @DataType WHERE [ItemAttributeID] = @ItemAttributeID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@DataType", item.DataType);
                    command.Parameters.AddWithValue("@ItemAttributeID", item.ItemAttributeID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public void DeleteItemAttribute(int ItemAttributeID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[ItemAttributeValue] WHERE [ItemAttributeID] = @ItemAttributeID
                                             DELETE FROM [dbo].[ItemAttributeOption] WHERE [ItemAttributeID] = @ItemAttributeID
                                             DELETE FROM [dbo].[ItemAttribute] WHERE [ItemAttributeID] = @ItemAttributeID";

                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ItemAttributeID", ItemAttributeID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        private static ItemAttributeDTO Populate(SqlDataReader reader)
        {
            ItemAttributeDTO obj = new ItemAttributeDTO()
            {
                ItemAttributeID = reader.GetInt32(reader.GetOrdinal("ItemAttributeID")),
                DataType = reader.GetString(reader.GetOrdinal("DataType")),
                Name = reader.GetString(reader.GetOrdinal("Name"))
            };

            return obj;
        }
    }
}
