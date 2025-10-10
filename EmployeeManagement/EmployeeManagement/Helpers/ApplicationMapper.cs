using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Model;

namespace EmployeeManagement.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Employee, EmployeeModel>().ReverseMap().
                ForMember(dest => dest.Id, opt => opt.Ignore()).
                ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}
