using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Koleksi.Repository.Implimentations
{
    public class CollectionAttributeValueRepo : ICollectionAttributeValueRepo
    {
        public List<CollectionAttributeValueDTO> GetCollectionAttributeValues(int collectionAttributeID)
        {
            List<CollectionAttributeValueDTO> items = new List<CollectionAttributeValueDTO>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT [CollectionAttributeID], [CollectionAttributeValueID], [Value], [CollectionID] FROM [dbo].[CollectionAttributeValue] WITH (NOLOCK) WHERE [CollectionAttributeID] = @CollectionAttributeID";
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

        public CollectionAttributeValueDTO InsertCollectionAttributeValue(CollectionAttributeValueDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[CollectionAttributeValue] ([CollectionAttributeID], [CollectionID], [Value]) VALUES (@CollectionAttributeID, @CollectionID, @Value); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionAttributeID", item.CollectionAttributeID);
                    command.Parameters.AddWithValue("@CollectionID", item.CollectionID);
                    command.Parameters.AddWithValue("@Value", item.Value);
                    item.CollectionAttributeValueID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public CollectionAttributeValueDTO UpdateCollectionAttributeValue(CollectionAttributeValueDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UPDATE [dbo].[CollectionAttributeValue] SET [Value] = @Value WHERE [CollectionAttributeValueID] = @CollectionAttributeValueID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Value", item.Value);
                    command.Parameters.AddWithValue("@CollectionAttributeValueID", item.CollectionAttributeValueID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public void DeleteCollectionAttributeValue(int collectionAttributeValueID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[CollectionAttributeValue] WHERE [CollectionAttributeValueID] = @CollectionAttributeValueID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionAttributeValueID", collectionAttributeValueID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        private static CollectionAttributeValueDTO Populate(SqlDataReader reader)
        {
            CollectionAttributeValueDTO obj = new CollectionAttributeValueDTO()
            {
                CollectionAttributeID = reader.GetInt32(reader.GetOrdinal("CollectionAttributeID")),
                CollectionAttributeValueID = reader.GetInt32(reader.GetOrdinal("CollectionAttributeValueID")),
                CollectionID = reader.GetInt32(reader.GetOrdinal("CollectionID")),
                Value = reader.GetString(reader.GetOrdinal("Value"))
            };

            return obj;
        }
    }
}
