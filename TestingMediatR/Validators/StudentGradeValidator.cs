using FluentValidation;
using TestingMediatR.Models;

namespace TestingMediatR.Validators
{
    public class StudentGradeValidator : AbstractValidator<StudentGrade>
    {
        public StudentGradeValidator()
        {
            RuleFor(x => x.Grade).InclusiveBetween(0, 100).WithMessage("Notat e lejuara jane midis 0 dhe 100 vjeç");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Pershkrimi eshte i detyruar")
                                       .MinimumLength(5).WithMessage("Pershkrimi duhet te jete te pakten 5 karaktere")
                                       .MaximumLength(50).WithMessage("Pershkrimi nuk mund te jete me i gjate se 50 karaktere"); 
        }
    }
}
