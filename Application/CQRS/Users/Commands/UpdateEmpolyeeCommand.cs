using Application.Dtos;
using Application.Helpers;
using Application.Helpers.MappingProfile;
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
   public record UpdateEmpolyeeCommand(UpdateEmployeeDto EmployeeDto):IRequest<ResponseViewModel<bool>>;

    public class UpdateEmpolyeeCommandHandler : IRequestHandler<UpdateEmpolyeeCommand, ResponseViewModel<bool>>
    {
        private readonly IGeneralRepository<Empolyee> _generalRepository;

        public UpdateEmpolyeeCommandHandler(IGeneralRepository<Empolyee> generalRepository)
        {
            _generalRepository = generalRepository;
        }
        public async Task<ResponseViewModel<bool>> Handle(UpdateEmpolyeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmpolyee = await _generalRepository.GetByIdAsync(request.EmployeeDto.Id);
            if (existingEmpolyee is null)
            {
                return ResponseViewModel<bool>.Failure(false, "Empolyee not found", ErrorCodeEnum.NotFound);
            }
            var updatedRecipe = request.EmployeeDto.Map<Empolyee>();
           

            _generalRepository.UpdateInclude(updatedRecipe,
           nameof(Empolyee.FirstName),
           nameof(Empolyee.LastName),
           nameof(Empolyee.Email)
          
        );
         var result = await _generalRepository.SaveChangesAsync();
            if (result <= 0)
                return ResponseViewModel<bool>.Failure(false, "An error occurred while updating the Empolyee", ErrorCodeEnum.ServerError);
            return ResponseViewModel<bool>.Success(true, "Empolyee updated successfully");


        }
    }
}
