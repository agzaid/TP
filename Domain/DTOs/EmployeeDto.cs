using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Web.Mvc;

namespace Domain.DTOs
{
    public class EmployeeDto
    {

        public EmployeeDto()
        {
            ListOfGrad = Enum.GetNames(typeof(Grad))
             .Select(v => new SelectListItem
             {
                 Text = v,
                 Value = Enum.Parse<Grad>(v).ToString()
             }).ToList();
        }

        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Grad? Grad { get; set; }
        public IFormFile? Image { get; set; }
        public string? Image64 { get; set; }

        public int? GradId { get; set; }

        public List<SelectListItem>? ListOfGrad { get; set; }

    }
}
