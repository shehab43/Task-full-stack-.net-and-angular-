using Application.Dtos;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;


namespace Application.CQRS.Recipes.Queries
{
   public  record GetEmpolyeeByIdCommand(int id) :IRequest<ResponseViewModel<GetByIdEmployeeDto>>;

    public class GetEmployeeByIdHandler : IRequestHandler<GetEmpolyeeByIdCommand, ResponseViewModel<GetByIdEmployeeDto>>
    {
        private readonly IGeneralRepository<Empolyee> _generalRepo;

        //constructor 
        public GetEmployeeByIdHandler(IGeneralRepository<Empolyee> generalRepo) {
            _generalRepo = generalRepo;
        }

        public async Task<ResponseViewModel<GetByIdEmployeeDto>> Handle(GetEmpolyeeByIdCommand request, CancellationToken cancellationToken)
        {
            var Employee = await _generalRepo.GetByIdAsync(request.id);
            if (Employee is null) {

                return ResponseViewModel<GetByIdEmployeeDto>.Failure(
               null,
               "Employee not found.",
               ErrorCodeEnum.NotFound);
            }

            var mappToUser = Employee.Map<GetByIdEmployeeDto>();

            return ResponseViewModel<GetByIdEmployeeDto>.Success(
                mappToUser,
                "Employee retrieved successfully."
            );


        }
    }
}
