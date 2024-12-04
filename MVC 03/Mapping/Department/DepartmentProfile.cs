using AutoMapper;
using Company.G05.DAL.Models;
using MVC_03.ViewModels;

namespace MVC_03.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile() 
        {
            CreateMap<DepartmentViewModel,Department>().ReverseMap();
        }
    }
}
