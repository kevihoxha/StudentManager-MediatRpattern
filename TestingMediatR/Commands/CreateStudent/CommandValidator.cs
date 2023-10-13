using FluentValidation;
using FluentValidation.Internal;
using System.ComponentModel.DataAnnotations;
using TestingMediatR.Commands.CreateStudent;
using TestingMediatR.Models;
using TestingMediatR.Repositories;
using TestingMediatR.Services;
using TestingMediatR.Validators;

namespace TestingMediatR.Commands.CreateStudent
{
    public class CommandValidator : AbstractValidator<Command1>
    {
        private readonly IStudentService _studentService;
        public CommandValidator(IStudentService studentService)
        {
            _studentService = studentService;

            RuleFor(student => student.studentName)
                .NotEmpty().WithMessage("Emri nuk mund te lihet bosh ")
                .Matches("^[a-zA-Z]+[a-zA-Z\\s]*$").WithMessage("Emri mund te permbaje vetem shkronja dhe hapesira dhe duhet te filloje me nje shkronje")
                .MinimumLength(3).WithMessage("Emri duhet te jete te pakten 3 karaktere")
                .MaximumLength(50).WithMessage("Emri nuk mund te jete me i gjate se 50 karaktere");

            RuleFor(student => student.studentEmail)
                .NotEmpty().WithMessage("Email nuk mund te lihet bosh ")
                .EmailAddress().WithMessage("Email i pa pershtatshem")
                .CustomAsync(async (email, context, cancellationToken) =>
                {
                    var exists = await _studentService.ExistsWithEmail(email, null);
                    if (exists)
                    {
                        context.AddFailure("Ky email është në përdorim");
                    }
                })
               .MaximumLength(50).WithMessage("Email nuk mund te jete me i gjate se 50 karaktere");

            RuleFor(student => student.studentAge)
                .NotEmpty().WithMessage("Mosha nuk mund te lihet bosh ")
                .InclusiveBetween(18, 99).WithMessage("Mosha e lejuar eshte midis 18 dhe 99 vjec");

            RuleFor(student => student.studentAddress)
                .NotEmpty().WithMessage("Adresa nuk mund te lihet bosh")
                .MinimumLength(5).WithMessage("Adresa duhet te jete te pakten 5 karaktere")
                .MaximumLength(100).WithMessage("Andresa nuk mund te jete me e gjate se 50 karaktere");

            RuleFor(x => x.phoneNumber)
          .NotEmpty().WithMessage("Phone number is required.")
          .Matches(@"^\+[1-9]\d{1,14}$").WithMessage("Invalid phone number format.")
          .CustomAsync(async (phoneNumber, context, cancellationToken) =>
          {
              var exists = await _studentService.ExistsWithPhoneNumber(phoneNumber, null);
              if (exists)
              {
                  context.AddFailure("Ky numer telefoni është në përdorim");
              }
          });

            RuleFor(x => x.StudentGrade).SetValidator(new StudentGradeValidator());
        }
    }
}
