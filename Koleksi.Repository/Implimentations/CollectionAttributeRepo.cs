using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Koleksi.Repository.Implimentations
{
    public class CollectionAttributeRepo : ICollectionAttributeRepo
    {
        public List<CollectionAttributeDTO> GetCollectionAttributes()
        {
            List<CollectionAttributeDTO> items = new List<CollectionAttributeDTO>();
            using(SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT [CollectionAttributeID], [Name], [DataType] FROM [dbo].[CollectionAttribute] WITH (NOLOCK)";
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

        public CollectionAttributeDTO InsertCollectionAttribute(CollectionAttributeDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[CollectionAttribute] ([Name], [DataType]) VALUES (@Name, @DataType); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@DataType", item.DataType);
                    item.CollectionAttributeID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public CollectionAttributeDTO UpdateCollectionAttribute(CollectionAttributeDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UPDATE [dbo].[CollectionAttribute] SET [Name] = @Name, [DataType] = @DataType WHERE [CollectionAttributeID] = @CollectionAttributeID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@DataType", item.DataType);
                    command.Parameters.AddWithValue("@CollectionAttributeID", item.CollectionAttributeID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public void DeleteCollectionAttribute(int collectionAttributeID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[CollectionAttributeValue] WHERE [CollectionAttributeID] = @CollectionAttributeID
                                             DELETE FROM [dbo].[CollectionAttributeOption] WHERE [CollectionAttributeID] = @CollectionAttributeID
                                             DELETE FROM [dbo].[CollectionAttribute] WHERE [CollectionAttributeID] = @CollectionAttributeID";

                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionAttributeID", collectionAttributeID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        private static CollectionAttributeDTO Populate(SqlDataReader reader)
        {
            CollectionAttributeDTO obj = new CollectionAttributeDTO()
            {
                CollectionAttributeID = reader.GetInt32(reader.GetOrdinal("CollectionAttributeID")),
                DataType = reader.GetString(reader.GetOrdinal("DataType")),
                Name = reader.GetString(reader.GetOrdinal("Name"))
            };

            return obj;
        }
    }
}
