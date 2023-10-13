using FluentValidation;
using TestingMediatR.Services;
using TestingMediatR.Validators;

namespace TestingMediatR.Commands.UpdateStudent
{
    public class CommandValidator : AbstractValidator<Command>
    {
        private readonly IStudentService _studentService;

        public CommandValidator(IStudentService studentService)
        {
            _studentService = studentService;

            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("Invalid student ID");

            RuleFor(command => command.StudentName)
                .NotEmpty().WithMessage("Emri nuk mund të lihet bosh")
                .Matches("^[a-zA-Z]+[a-zA-Z\\s]*$").WithMessage("Emri mund të përmbajë vetëm shkronja dhe hapsira dhe duhet të fillojë me një shkronjë")
                .MinimumLength(3).WithMessage("Emri duhet të jetë të paktën 3 karaktere")
                .MaximumLength(50).WithMessage("Emri nuk mund të jetë më i gjatë se 50 karaktere");

            RuleFor(command => command.StudentEmail)
                .NotEmpty().WithMessage("Email nuk mund të lihet bosh")
                .EmailAddress().WithMessage("Email i pa përshtatshëm")
                .CustomAsync(async (email, context, cancellationToken) =>
                {
                    var studentId = context.InstanceToValidate.Id;
                    var exists = await _studentService.ExistsWithEmail(email, studentId);
                    if (exists)
                    {
                        context.AddFailure("Ky email është në përdorim");
                    }
                })
                .MaximumLength(50).WithMessage("Email nuk mund të jetë më i gjatë se 50 karaktere");

            RuleFor(command => command.StudentAge)
                .NotEmpty().WithMessage("Mosha nuk mund të lihet bosh")
                .InclusiveBetween(18, 99).WithMessage("Mosha e lejuar është midis 18 dhe 99 vjeç");

            RuleFor(command => command.StudentAddress)
                .NotEmpty().WithMessage("Adresa nuk mund të lihet bosh")
                .MinimumLength(3).WithMessage("Adresa duhet të jetë të paktën 3 karaktere")
                .MaximumLength(50).WithMessage("Adresa nuk mund të jetë më e gjatë se 50 karaktere");

            RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("Phone number is required.")
           .Matches(@"^\+[1-9]\d{1,14}$").WithMessage("Invalid phone number format.")
           .CustomAsync(async (phoneNumber, context, cancellationToken) =>
           {
               var studentId = context.InstanceToValidate.Id;
               var exists = await _studentService.ExistsWithEmail(phoneNumber, studentId);
               if (exists)
               {
                   context.AddFailure("Ky numer telefoni është në përdorim");
               }
           });

            RuleFor(x => x.studentGrade).SetValidator(new StudentGradeValidator());

        }
    }
}
