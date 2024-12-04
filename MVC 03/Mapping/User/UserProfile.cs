using AutoMapper;
using Company.G05.DAL.Models;
using MVC_03.ViewModels;

namespace MVC_03.Mapping.User
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();
        }
    }
}
