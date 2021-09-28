using System.Threading.Tasks;
using QTEMP.Models;

namespace QTEMP.Providers
{
    public interface IApiProvider
    {
        Task<Response> PostHealth(Temperature temperature);
    }
}