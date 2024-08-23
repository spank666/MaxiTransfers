using WebApp.Dtos;

namespace WebApp.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> AllEmployees();
        Task<bool> Add(EmployeeDto employee);
        Task<bool> Delete(int Id);
        Task<bool> Update(EmployeeDto employee);
    }
}
