using Loyalty.System.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.System.API.Services
{
    public class BaseService
    {
        public BaseContext Context { get; }
        public BaseService(BaseContext baseContext) {
            Context = baseContext;
        }
    }
}
