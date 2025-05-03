using Application.Dtos;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Application.ViewModels;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Recipes.Queries
{
    public record GetAllEmpolyeesCommand(queryParams Query) : IRequest<ResponseViewModel<IEnumerable<GetAllEmployeesDto>>>;
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmpolyeesCommand, ResponseViewModel<IEnumerable<GetAllEmployeesDto>>>
    {
        private readonly IGeneralRepository<Empolyee> _generalRepo;

        public GetAllEmployeesQueryHandler(IGeneralRepository<Empolyee> generalRepo)
        {
            _generalRepo = generalRepo;
        }


        public async Task<ResponseViewModel<IEnumerable<GetAllEmployeesDto>>> Handle(GetAllEmpolyeesCommand request, CancellationToken cancellationToken)
        {
            var Employees =   _generalRepo.GetAll();
            if (request.Query.SearchTerm is not null)
            {
                Employees = Employees.Where(x => x.FirstName.Contains(request.Query.SearchTerm));
            }
            if (request.Query.Skip.HasValue)
            {
                Employees = Employees.Skip((request.Query.Skip.Value));

            }
            if (request.Query.Take.HasValue)
            {
                Employees = Employees.Take(request.Query.Take.Value);
            }


            var mappedEmployees = Employees.Map<IEnumerable<GetAllEmployeesDto>>().ToList();
            return ResponseViewModel<IEnumerable<GetAllEmployeesDto>>.Success(
                mappedEmployees,
                "Employee retrieved successfully."
            );
        }
    }

   

}
