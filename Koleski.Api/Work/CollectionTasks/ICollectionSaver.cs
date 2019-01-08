using Koleksi.Domain;

namespace Koleski.Api.Work.CollectionTasks
{
    public interface ICollectionSaver
    {
        void SaveCollection(Collection collection);

        void DeleteCollection(int id);
    }
}