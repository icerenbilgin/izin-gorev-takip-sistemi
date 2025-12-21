using api.Business.Abstract;
using api.Business.Concrete;
using api.DataAccess.Abstract;
using api.DataAccess.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace api.DependencyResolvers
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDal, EfUserDal>();

            services.AddScoped<ITaskService, TaskManager>();
            services.AddScoped<ITaskDal, EfTaskDal>();

            services.AddScoped<IDepartmentService, DepartmentManager>();
            services.AddScoped<IDepartmentDal, EfDepartmentDal>();

            services.AddScoped<ILeaveRequestService, LeaveRequestsManager>();
            services.AddScoped<ILeaveRequestsDal, EfLeaveRequestsDal>();

            services.AddScoped<ILeaveTypeService, LeaveTypeManager>();
            services.AddScoped<ILeaveTypeDal, EfLeaveTypeDal>();

            services.AddScoped<ITaskStatusService, TaskStatusManager>();
            services.AddScoped<ITaskStatusDal, EfTaskStatusDal>();

            services.AddScoped<IUserRoleService, UserRoleManager>();
            services.AddScoped<IUserRoleDal, EfUserRoleDal>();

            return services;
        }
    }
}