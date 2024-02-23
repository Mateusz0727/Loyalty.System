
using AutoMapper;
using Loyalty.System.API.Models.Register;

namespace Loyalty.System.API.Models
{
    public class AutoMapperInitializator : Profile
    {
        public AutoMapperInitializator()
        {
            UserModels();
        }

        protected void UserModels()
        {
            CreateMap<User, RegisterFormModel>().ReverseMap().ForMember(x => x.UserName, m => m.MapFrom(s => s.SurName));
        }

    }
}
