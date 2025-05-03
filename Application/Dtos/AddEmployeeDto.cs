using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public record AddEmployeeDto(string FirstName, string LastName, string Email,string position, DateTime CreatedAt);
   
}
