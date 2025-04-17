using Application.Common.Interfaces;
using Application.Common.Utility;
using Application.Services.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Application.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IUnitOfWork unitOfWork, ILogger<EmployeeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<string>> AddEmployee(EmployeeDto obj)
        {
            try
            {
                obj.Name = obj.Name?.ToLower();
                var lookForName = await _unitOfWork.Employees.Get(s => s.Name == obj.Name);
                if (lookForName == null)
                {
                    var employee = new Employee()
                    {
                        Name = obj.Name,
                        Modified_Date = DateTime.Now,
                        Email = obj.Email,
                        PhoneNumber = obj.PhoneNumber,
                        Create_Date = DateTime.Now,
                        IsDeleted = false,
                        Grad = obj.Grad,
                    };
                    if (obj.Image.Length > 0)
                    {
                        employee.Image = FileExtensions.ConvertImageToByteArray(obj.Image);
                    }


                    _unitOfWork.Employees.Add(employee);
                    _unitOfWork.Save();

                    return Result<string>.Success("employee added successfully.", "Success");
                }
                else
                {
                    return Result<string>.Failure("new employee Already Exists", "error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating employee with Name: {employeeName}", obj.Name);
                return Result<string>.Failure("Error", "error"); ;  // Rethrow the exception after logging it
            }
        }

        public async Task<Result<string>> DeleteEmployee(int id)
        {
            try
            {
                var oldEmployee = await _unitOfWork.Employees.Get(s => s.Id == id);
                if (oldEmployee != null)
                {
                    oldEmployee.IsDeleted = true;
                    oldEmployee.Modified_Date = DateTime.UtcNow;
                    _unitOfWork.Employees.Update(oldEmployee);
                    _unitOfWork.Save();
                    return Result<string>.Success("Deleted Employee Created Successfully", "Success");
                }
                else
                    return Result<string>.Failure("new employee Already Exists", "error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating employee with Id: {Id}", id);
                return Result<string>.Failure("Error Occured...!!!", "error"); // Rethrow the exception after logging it
            }
        }

        public async Task<Result<EmployeeDto>> GetEmployee(int id)
        {
            try
            {
                var employee = await _unitOfWork.Employees.Get(u => u.Id == id);
                if (employee != null)
                {
                    var employeeDto = new EmployeeDto()
                    {
                        Name = employee.Name,
                        Grad = employee.Grad,
                        GradId = (int?)employee.Grad,
                        Email = employee.Email,
                        PhoneNumber = employee.PhoneNumber,
                        Id = employee.Id,
                        Image64 = FileExtensions.ByteArrayToImageBase64(employee.Image),
                    };
                    return Result<EmployeeDto>.Success(employeeDto, "employee retrieved successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving employee with Id: {Id}", id);
                throw;  // Rethrow the exception after logging it
            }
            return Result<EmployeeDto>.Failure("Employee doesn't Exists", "error");

        }

        public async Task<Result<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            try
            {
                var employees = await _unitOfWork.Employees.GetAll(s => s.IsDeleted == false);
                var showEmployee = employees.Select(s => new EmployeeDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Grad = s.Grad,
                    GradId = (int?)s.Grad,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Image64 = s.Image != null ? FileExtensions.ByteArrayToImageBase64(s.Image) : "",
                }).ToList();

                if (showEmployee != null)
                {
                    _logger.LogInformation("GetAllEmployees method completed. {employeesCount} employees retrieved.", showEmployee.Count);
                }
                else
                {
                    _logger.LogWarning("GetAllEmployees method completed. No employees retrieved because the employee list is null.");
                }
                return Result<IEnumerable<EmployeeDto>>.Success(showEmployee, "employee retrieved successfully."); ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving employees.");
                return Result<IEnumerable<EmployeeDto>>.Failure(ex.InnerException.ToString(), "error");
            }
        }

        public async Task<Result<string>> UpdateEmployee(EmployeeDto obj)
        {
            try
            {
                obj.Name = obj.Name?.ToLower();
                var oldEmployee = await _unitOfWork.Employees.Get(s => s.Id == obj.Id);
                if (oldEmployee != null)
                {
                    oldEmployee.Name = obj.Name;
                    oldEmployee.Email = obj.Email;
                    oldEmployee.PhoneNumber = obj.PhoneNumber;
                    //oldEmployee.Grad = obj.Grad;
                    oldEmployee.Grad = (Grad?)obj.GradId;
                    if (obj.Image.Length > 0)
                    {
                        var image = FileExtensions.ConvertImageToByteArray(obj.Image);
                        oldEmployee.Image = image;
                    }

                    oldEmployee.Modified_Date = DateTime.UtcNow;
                    _unitOfWork.Employees.Update(oldEmployee);
                    _unitOfWork.Save();
                    return Result<string>.Success("employee retrieved successfully.", "Successeded");
                }
                else
                    return Result<string>.Failure("employee retrieved failed.", "error"); ;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating employee with Id: {Id}", obj.Id);
                return Result<string>.Failure("employee retrieved failed.", "error"); ;
                // Rethrow the exception after logging it
            }
        }
    }
}
