using System.Threading.Tasks;

namespace EmailCache
{
    public interface IEmailList : Orleans.IGrainWithStringKey
    {
        Task Add(string email);

        Task Remove(string email);

        Task<bool> Contains(string email);

        Task<int> Count();
    }
}
