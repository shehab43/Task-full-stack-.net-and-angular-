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
    public record AddEmpolyeeCommand(AddEmployeeDto UserDto):IRequest<ResponseViewModel<bool>>;
    public class AddEmpolyeeCommandHandler : IRequestHandler<AddEmpolyeeCommand, ResponseViewModel<bool>>
    {
        private readonly IGeneralRepository<Empolyee> _generalRepo;

        public AddEmpolyeeCommandHandler(IGeneralRepository<Empolyee> generalRepo)
        {
            _generalRepo = generalRepo;
        }
        public async Task<ResponseViewModel<bool>> Handle(AddEmpolyeeCommand request, CancellationToken cancellationToken)
        {
           
            var recipe = request.UserDto.Map<Empolyee>();
               
               await _generalRepo.AddAsync(recipe);
               var result =  await _generalRepo.SaveChangesAsync();
                if (result > 0)


                return ResponseViewModel<bool>.Success(true, "Empolyee added successfully.");

            
                return ResponseViewModel<bool>.Failure(
                   false,
                   $"An error occurred while adding the Empolyee: ",
                   ErrorCodeEnum.ServerError
               );

          
        }
    }
    }



