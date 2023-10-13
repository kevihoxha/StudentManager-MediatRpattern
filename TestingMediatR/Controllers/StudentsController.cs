
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestingMediatR.Commands;
using TestingMediatR.Commands.CreateStudent;
using TestingMediatR.Commands.UpdateStudent;
using TestingMediatR.Models;

namespace TestingMediatR.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IQueryable<StudentDetails>> GetStudentListAsync()
        {
            var studentDetails = await mediator.Send(new Queries.GetStudentListQuery.Query());
            return studentDetails;
        }
        [HttpGet("studentId")]
        public async Task<StudentDetails> GetStudentByIdAsync(int studentId)
        {
            var studentDetails = await mediator.Send(new Queries.GetStudentByIdQuery.Query(studentId));
            return studentDetails;
        }
        [HttpPost]
        public async Task<ActionResult> AddStudentAsync(Commands.CreateStudent.Command1 command)
        {
            return await ValidateAndSendCommandAsync(command, _validators.FirstOrDefault());
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
        public async Task<ActionResult> UpdateStudentAsync(Commands.UpdateStudent.Command command)
        {
            return await ValidateAndSendCommandAsync(command, _updateStudentCommandValidator);
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
