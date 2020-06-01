using System;
using System.Threading.Tasks;

namespace movies.Interfaces
{
    public interface ISimpleRequestService
    {
        Task<T> GetSimpleAsync<T>(string requestParam);
    }
}