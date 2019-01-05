using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Koleksi.Repository.Implimentations
{
    public class CollectionRelationshipRepo : ICollectionRelationshipRepo
    {
        public List<CollectionRelationshipDTO> GetCollectionRelationships(int parentCollectionID)
        {
            List<CollectionRelationshipDTO> items = new List<CollectionRelationshipDTO>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT [CollectionRelationshipID], [ParentCollectionID], [CollectionID] FROM [dbo].[CollectionRelationship] WITH (NOLOCK) WHERE [ParentCollectionID] = @ParentCollectionID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("ParentCollectionID", parentCollectionID);

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

        public CollectionRelationshipDTO InsertCollectionRelationship(CollectionRelationshipDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[CollectionRelationship] ([ParentCollectionID], [CollectionID]) VALUES (@ParentCollectionID, @CollectionID); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionID", item.CollectionID);
                    command.Parameters.AddWithValue("@ParentCollectionID", item.ParentCollectionID);
                    item.CollectionRelationshipID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }
       
        public void DeleteCollectionRelationships(int parentCollectionID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[CollectionRelationship] WHERE [ParentCollectionID] = @ParentCollectionID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ParentCollectionID", parentCollectionID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }
    
        public void DeleteCollectionRelationshipsByChild(int collectionID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[CollectionRelationship] WHERE [CollectionID] = @CollectionID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionID", collectionID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        private static CollectionRelationshipDTO Populate(SqlDataReader reader)
        {
            CollectionRelationshipDTO obj = new CollectionRelationshipDTO()
            {
                CollectionRelationshipID = reader.GetInt32(reader.GetOrdinal("CollectionRelationshipID")),
                ParentCollectionID = reader.GetInt32(reader.GetOrdinal("ParentCollectionID")),
                CollectionID = reader.GetInt32(reader.GetOrdinal("CollectionID"))
            };

            return obj;
        }
    }
}
