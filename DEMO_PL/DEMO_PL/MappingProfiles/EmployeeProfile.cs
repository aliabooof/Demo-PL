using AutoMapper;
using Demo.DAL.Models;
using DEMO_PL.ViewModels;

namespace DEMO_PL.MappingProfiles
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap()
                /*.ForMember(d=>d.Name,o=>o.MapFrom(s=>s.Name)*/; // if u got two propertis with different name 
                                                                //auto mapper by default map properties with the same name
        }
    }
}
