using System.Collections.Generic;
using Koleksi.Domain;

namespace Koleksi.Services.Components.Loaders
{
    public interface IAttributeLoader
    {
        List<Attribute> LoadAttributes(int sourceID);
    }
}