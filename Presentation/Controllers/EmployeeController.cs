
using Application.CQRS.Recipes.Commands;
using Application.CQRS.Recipes.Queries;
using Application.Dtos;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using Application.ViewModels;
using AutoMapper.Features;
using Azure;
using Domain.Enums;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Add Employee
        [HttpPost]
        public async Task<ResponseViewModel<bool>> CreateEmployee([FromBody] AddEmployeeDto EmployeeDto)
        {
            var result = await _mediator.Send(new AddEmpolyeeCommand(EmployeeDto));

            if (!result.IsSuccess)
                return
                    ResponseViewModel<bool>.Failure(
                        false,
                        result.Message,
                        result.StatusCode);



            return ResponseViewModel<bool>.Success(true, "Employee added successfully.");
        }

        #endregion


        #region Get All Employee

        [HttpGet]
        public async Task<ResponseViewModel<IEnumerable<GetAllEmployeesDto>>> GetAllEmployees([FromQuery] queryParams queryParams)
        {
            var result = await _mediator.Send(new GetAllEmpolyeesCommand(queryParams));

            if (!result.IsSuccess || result.Data is null)
            {

                return ResponseViewModel<IEnumerable<GetAllEmployeesDto>>.Failure(null, result.Message, result.StatusCode);
            }



            var mappedData = result.Data.Map<IEnumerable<GetAllEmployeesDto>>();
            return ResponseViewModel<IEnumerable<GetAllEmployeesDto>>.Success(mappedData, "Success");
        }
        #endregion

        #region Delete Employee
        [HttpDelete]
        public async Task<ResponseViewModel<bool>> DeleteEmployee(int id)
        {
            var result = await _mediator.Send(new RemoveEmpolyeeCommand(id));


            if (result.IsSuccess)
                return ResponseViewModel<bool>.Success(result.Data, result.Message);

            return ResponseViewModel<bool>.Failure(result.Data, result.Message, ErrorCodeEnum.FailerDelete);

        }
        #endregion

      

        #region Update Employee
        [HttpPut]
        public async Task<ResponseViewModel<bool>> UpdateRecipe(UpdateEmployeeDto viewModel)
        {
            var result = await _mediator.Send(new UpdateEmpolyeeCommand(viewModel));

            return ResponseViewModel<bool>.Success(true, "Employee Updated Success");
        }
        #endregion


    }
}
