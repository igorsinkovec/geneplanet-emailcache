using System.Threading.Tasks;

namespace EmailCache
{
    public interface IList : Orleans.IGrainWithIntegerKey
    {
        Task Add(string email);

        Task Remove(string email);

        Task<bool> Contains(string email);

        Task<int> Count();
    }
}
