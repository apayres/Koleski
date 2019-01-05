using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Koleksi.Repository.Implimentations
{
    public class CollectionRepo : ICollectionRepo
    {
        public List<CollectionDTO> GetCollections()
        {
            List<CollectionDTO> items = new List<CollectionDTO>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT [CollectionID], [Name], [Description], [DisplayOrder] FROM [dbo].[Collection] WITH (NOLOCK)";
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

        public List<CollectionDTO> GetCollections(int? parentCollectionID)
        {
            List<CollectionDTO> items = new List<CollectionDTO>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" SELECT 
                                                c.[CollectionID], 
                                                c.[Name], 
                                                c.[Description], 
                                                c.[DisplayOrder] 
                                             FROM [dbo].[Collection] c WITH (NOLOCK)
                                             LEFT JOIN [dbo].[CollectionRelationship] rel ON rel.CollectionID = c.CollectionID";

                    if (parentCollectionID.HasValue)
                    {
                        command.CommandText += "WHERE rel.ParentCollectionID = @ParentCollectionID";
                        command.Parameters.AddWithValue("@ParentCollectionID", parentCollectionID);
                    }
                    else
                    {
                        command.CommandText += "WHERE rel.ParentCollectionID IS NULL";
                    }

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

        public CollectionDTO InsertCollection(CollectionDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[Collection] ([Name], [Description], [DisplayOrder]) VALUES (@Name, @Description, @DisplayOrder); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Description", item.Description);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    item.CollectionID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public CollectionDTO UpdateCollection(CollectionDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UPDATE [dbo].[Collection] SET [Name] = @Name, [Description] = @Description, [DisplayOrder] = @DisplayOrder WHERE [CollectionID] = @CollectionID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Description", item.Description);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    command.Parameters.AddWithValue("@CollectionID", item.CollectionID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public void DeleteCollection(int collectionID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" 
                        DELETE FROM [CollectionAttributeValue] WHERE CollectionID = @CollectionID
                        DELETE FROM [CollectionImage] WHERE CollectionID = @CollectionID
                        DELETE FROM [CollectionRelationship] WHERE CollectionID = @CollectionID OR [ParentCollectionID] = @CollectionID
                        DELETE FROM [ItemToCollection] WHERE CollectionID = @CollectionID
                    ";

                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionID", collectionID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        private static CollectionDTO Populate(SqlDataReader reader)
        {
            CollectionDTO obj = new CollectionDTO()
            {
                DisplayOrder = reader.GetInt32(reader.GetOrdinal("DisplayOrder")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                CollectionID = reader.GetInt32(reader.GetOrdinal("CollectionID")),
                Description = reader.GetString(reader.GetOrdinal("Description"))
            };

            return obj;
        }
    }
}
