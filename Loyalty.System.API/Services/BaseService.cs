using AutoMapper;
using Loyalty.System.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.System.API.Services
{
    public class BaseService
    {
        protected BaseContext Context { get; }
        protected IMapper Mapper { get; }

        public BaseService(IMapper mapper, BaseContext context)
        {
            Mapper = mapper;
            Context = context;

        }
    }
}
