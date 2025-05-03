using Application.Helpers;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Recipes.Commands
{
    public record RemoveEmpolyeeCommand(int id):IRequest<ResponseViewModel<bool>>;
    public class RemoveEmployeeCommandHandler : IRequestHandler<RemoveEmpolyeeCommand,ResponseViewModel<bool>>
    {
        private readonly IGeneralRepository<Empolyee> _generalRepo;

        public RemoveEmployeeCommandHandler(IGeneralRepository<Empolyee> generalRepo) {
            _generalRepo = generalRepo;
        }
        public async Task<ResponseViewModel<bool>> Handle(RemoveEmpolyeeCommand request, CancellationToken cancellationToken)
        {
                var Employee = await _generalRepo.GetByIdAsync(request.id);
                if (Employee is null)
                {
                    return ResponseViewModel<bool>.Failure(false, "Employee not found", ErrorCodeEnum.NotFound);
                }

              await _generalRepo.Delete(request.id);
             var result =  await _generalRepo.SaveChangesAsync();
                if (result < 0)
                  return ResponseViewModel<bool>.Failure(false, "An error occurred while deleting the Employee", ErrorCodeEnum.ServerError);

            return ResponseViewModel<bool>.Success(true, "Employee deleted successfully.");
 




        }
    }
}
