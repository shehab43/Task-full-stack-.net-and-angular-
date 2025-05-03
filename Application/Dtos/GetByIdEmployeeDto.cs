using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public record GetByIdEmployeeDto(int Id, string FirstName, string LastName, string Email, string position);

}
