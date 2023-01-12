using FluentValidation;
using Intive_Patronage.Entities;

namespace Intive_Patronage.Validators
{
   public class AuthorValidator : AbstractValidator<Author>
   {
      /// <summary>
      /// Setting up rules which validates Author
      /// </summary>
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

      /// <summary>
      /// Method to check if name has only letters
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      protected bool MustContainOnlyLetters(string name)
      {
         return name.All(Char.IsLetter);
      }
   }
}
