using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Koleksi.Repository.Implimentations
{
    public class CollectionAttributeOptionRepo : ICollectionAttributeOptionRepo
    {
        public List<CollectionAttributeOptionDTO> GetCollectionAttributeOptions(int collectionAttributeID)
        {
            List<CollectionAttributeOptionDTO> items = new List<CollectionAttributeOptionDTO>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT [CollectionAttributeOptionID], [DisplayLabel], [Value], [DisplayOrder], [CollectionAttributeID] FROM [dbo].[CollectionAttributeOption] WITH (NOLOCK) WHERE [CollectionAttributeID] = @CollectionAttributeID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("CollectionAttributeID", collectionAttributeID);

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

        public CollectionAttributeOptionDTO InsertCollectionAttribute(CollectionAttributeOptionDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[CollectionAttributeOption] ([DisplayLabel], [Value], [DisplayOrder], [CollectionAttributeID]) VALUES (@DisplayLabel, @Value, @DisplayOrder, @CollectionAttributeID); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@DisplayLabel", item.DisplayLabel);
                    command.Parameters.AddWithValue("@Value", item.Value);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    command.Parameters.AddWithValue("@CollectionAttributeID", item.CollectionAttributeID);
                    item.CollectionAttributeOptionID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public CollectionAttributeOptionDTO UpdateCollectionAttribute(CollectionAttributeOptionDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UPDATE [dbo].[CollectionAttributeOption] SET [DisplayLabel] = @DisplayLabel, [Value] = @Value, [DisplayOrder] = @DisplayOrder WHERE [CollectionAttributeOptionID] = @CollectionAttributeOptionID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@DisplayLabel", item.DisplayLabel);
                    command.Parameters.AddWithValue("@Value", item.Value);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    command.Parameters.AddWithValue("@CollectionAttributeOptionID", item.CollectionAttributeOptionID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public void DeleteCollectionAttributeOption(int collectionAttributeOptionID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[CollectionAttributeOption] WHERE [CollectionAttributeOptionID] = @CollectionAttributeOptionID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionAttributeOptionID", collectionAttributeOptionID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        private static CollectionAttributeOptionDTO Populate(SqlDataReader reader)
        {
            CollectionAttributeOptionDTO obj = new CollectionAttributeOptionDTO()
            {
                CollectionAttributeID = reader.GetInt32(reader.GetOrdinal("CollectionAttributeID")),
                CollectionAttributeOptionID = reader.GetInt32(reader.GetOrdinal("CollectionAttributeOptionID")),
                DisplayLabel = reader.GetString(reader.GetOrdinal("DisplayLabel")),
                DisplayOrder = reader.GetInt32(reader.GetOrdinal("DisplayOrder")),
                Value = reader.GetString(reader.GetOrdinal("Value"))
            };

            return obj;
        }
    }
}
