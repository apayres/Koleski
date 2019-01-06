using Koleksi.Repository.DataTranserObjects;
using Koleksi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Koleksi.Repository.Implimentations
{
    public class ItemToCollectionRepo : IItemToCollectionRepo
    {
        public ItemToCollectionDTO InsertItemToCollection(ItemToCollectionDTO item)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[ItemToCollection] ([CollectionID], [ItemID]) VALUES (@CollectionID, @ItemID); SELECT @@IDENTITY AS [ID]";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionID", item.CollectionID);
                    command.Parameters.AddWithValue("@ItemID", item.ItemID);
                    item.ItemToCollectionID = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
                connection.Dispose();
            }

            return item;
        }

        public void DeleteItemToCollection(int collectionID, int itemID)
        {

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DatabaseConnectoin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "DELETE FROM [dbo].[ItemToCollection] WHERE CollectionID = @CollectionID AND ItemID = @ItemID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CollectionID", collectionID);
                    command.Parameters.AddWithValue("@ItemID", itemID);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }
    }
}
