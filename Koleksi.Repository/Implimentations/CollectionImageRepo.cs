using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Koleksi.Repository.Implimentations
{
    public class CollectionImageRepo : ICollectionImageRepo
    {
        public List<CollectionImageDTO> GetCollectionImages(int collectionID)
        {
            List<CollectionImageDTO> items = new List<CollectionImageDTO>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT [CollectionImageID], [CollectionID], [ImagePath], [Caption], [DisplayOrder] FROM [dbo].[CollectionImage] WITH (NOLOCK) WHERE [CollectionID] = @CollectionID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("CollectionID", collectionID);

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

        public CollectionImageDTO InsertCollectionImage(CollectionImageDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[CollectionImage] ([CollectionID], [ImagePath], [Caption], [DisplayOrder]) VALUES (@CollectionID, @ImagePath, @Caption, @DisplayOrder); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionID", item.CollectionID);
                    command.Parameters.AddWithValue("@ImagePath", item.ImagePath);
                    command.Parameters.AddWithValue("@Caption", item.Caption);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    item.CollectionImageID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public CollectionImageDTO UpdateCollectionImage(CollectionImageDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "UPDATE [dbo].[CollectionImage] SET [Caption] = @Caption, [DisplayOrder] = @DisplayOrder WHERE [CollectionImageID] = @CollectionImageID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Caption", item.Caption);
                    command.Parameters.AddWithValue("@DisplayOrder", item.DisplayOrder);
                    command.Parameters.AddWithValue("@CollectionImageID", item.CollectionImageID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public void DeleteCollectionImage(int collectionImageID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[CollectionImage] WHERE [CollectionImageID] = @CollectionImageID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionImageID", collectionImageID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        public void DeleteCollectionImages(int collectionID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = @" DELETE FROM [dbo].[CollectionImage] WHERE [CollectionID] = @CollectionID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionID", collectionID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        private static CollectionImageDTO Populate(SqlDataReader reader)
        {
            CollectionImageDTO obj = new CollectionImageDTO()
            {
                CollectionID = reader.GetInt32(reader.GetOrdinal("CollectionID")),
                CollectionImageID = reader.GetInt32(reader.GetOrdinal("CollectionImageID")),
                DisplayOrder = reader.GetInt32(reader.GetOrdinal("DisplayOrder")),
                Caption = reader.GetString(reader.GetOrdinal("Caption")),
                ImagePath = reader.GetString(reader.GetOrdinal("ImagePath"))
            };

            return obj;
        }
    }
}
