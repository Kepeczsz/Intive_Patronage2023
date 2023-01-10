using FluentValidation;
using Intive_Patronage.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using System.Globalization;

namespace Intive_Patronage.Validators
{
   public class AuthorValidator : AbstractValidator<Author>
   {
      public AuthorValidator() 
      {
         RuleFor(a => a.FirstName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(2, 50).WithMessage("{Length ({TotalLength} of {PropertyName} is Invalid)}")
            .Must(MustContainOnlyLetters).WithMessage("{PropertyName} must contain only letters.");
            ;

         RuleFor(a => a.LastName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(2, 50).WithMessage("{Length ({TotalLength} of {PropertyName} is Invalid)}")
            .Must(MustContainOnlyLetters).WithMessage("{PropertyName} must contain only letters.");
         ;

         RuleFor(a => a.BirthDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} is Empty");
      }

      protected bool MustContainOnlyLetters(string name)
      {
         return name.All(Char.IsLetter);
      }
   }
}
