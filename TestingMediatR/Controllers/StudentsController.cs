
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TestingMediatR.JWT;
using TestingMediatR.Login;
using TestingMediatR.Models;
using TestingMediatR.Queries.GetStudentByIdQuery;
using TestingMediatR.Queries.GetStudentsPaginated;

namespace TestingMediatR.Controllers
{
    [Route("[controller]")]

    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IEnumerable<IValidator<Commands.CreateStudent.Command1>> _validators;
        private readonly Commands.UpdateStudent.CommandValidator _updateStudentCommandValidator;

        public StudentsController(IMediator mediator,
            IEnumerable<IValidator<Commands.CreateStudent.Command1>> validators,
            Commands.UpdateStudent.CommandValidator updateStudentCommandValidator)
        {
            this.mediator = mediator;
            _validators = validators;
            _updateStudentCommandValidator = updateStudentCommandValidator;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<IQueryable<StudentDetails>> GetStudentListAsync()
        {
            var studentDetails = await mediator.Send(new Queries.GetStudentListQuery.Query());
            return studentDetails;
        }
        [HttpGet("page/{pageNumber}")]
        public async Task<QueryResponse> GetStudentsPaginatedAsync(int pageNumber)
        {
            var studentDetails = await mediator.Send(new Queries.GetStudentsPaginated.Query(pageNumber));
            return studentDetails;
        }
        [Authorize(Policy = "CanViewStudentDetails")]
        [HttpGet("{studentId}")]
        public async Task<StudentQueryResponse> GetStudentByIdAsync(int studentId)
        {
            var studentDetails = await mediator.Send(new Queries.GetStudentByIdQuery.Query(studentId));
            return studentDetails;
        }
        [HttpPost("login")]
        public async Task<ActionResult> LoginStudent([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var loginCommand = new LoginCommand(request.Email);
            var result = await mediator.Send(loginCommand, cancellationToken);
            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<ActionResult> AddStudentAsync(Commands.CreateStudent.Command1 command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command);
            return Ok(result);
            /* return await ValidateAndSendCommandAsync(command, _validators.FirstOrDefault());*/
        }
        // MENYRA E PARE 
        /*var validationResult = await _commandValidator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }*//*
        var result = await mediator.Send(command);
        return Ok(result);
    }*/
        [HttpPut]
        public async Task<ActionResult> UpdateStudentAsync(Commands.UpdateStudent.Command command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command);
            return Ok(result);
            /* return await ValidateAndSendCommandAsync(command, _updateStudentCommandValidator);*/
        }
        [HttpDelete("{studentId}")]
        public async Task<ActionResult> DeleteStudentAsync(int studentId)
        {
            var isDeleted = await mediator.Send(new Commands.DeleteCommand.Command(studentId));
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
        private async Task<ActionResult> ValidateAndSendCommandAsync<T>(T command, IValidator<T> validator)
        {
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { Errors = errors });
                }
            }

            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
