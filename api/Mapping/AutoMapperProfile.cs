using AutoMapper;
using api.Models;
using api.Dtos;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LeaveTypes, LeaveTypesDto>().ReverseMap();
        CreateMap<Departments, DepartmentsDto>().ReverseMap();
        CreateMap<TaskStatuses, TaskStatusesDto>().ReverseMap();
        CreateMap<UserRoles, UserRolesDto>().ReverseMap();
    }
}
