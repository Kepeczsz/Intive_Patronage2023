using FluentValidation;
using Intive_Patronage.Entities;

namespace Intive_Patronage.Validators
{
   public class AuthorValidator : AbstractValidator<Author>
   {
      public AuthorValidator() 
      {
         RuleFor(a => a.FirstName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .Length(2, 50).WithMessage("{Length ({TotalLength} of {PropertyName} is Invalid)}")
            .Must(MustContainOnlyLetters).WithMessage("{PropertyName} must contain only letters.");

         RuleFor(a => a.LastName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .Length(2, 50).WithMessage("{Length ({TotalLength} of {PropertyName} is Invalid)}")
            .Must(MustContainOnlyLetters).WithMessage("{PropertyName} must contain only letters.");

         RuleFor(a => a.BirthDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} must not be empty");

         RuleFor(a => a.Gender)
            .NotEmpty().WithMessage("{PropertyName} must not be empty");
      }

      protected bool MustContainOnlyLetters(string name)
      {
         return name.All(Char.IsLetter);
      }
   }
}
