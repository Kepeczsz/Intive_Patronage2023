using FluentValidation;
using Intive_Patronage.Entities;

namespace Intive_Patronage.Validators
{
   public class BookValidator : AbstractValidator<Book>
   {
      /// <summary>
      /// Setting up rules which validates Book
      /// </summary>
      public BookValidator()
      {
         RuleFor(b => b.Title)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .Length(2, 50).WithMessage("{Length ({TotalLength} of {PropertyName} is Invalid)}");

         RuleFor(b => b.Description)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .Length(1, 255).WithMessage("{Length ({TotalLength} of {PropertyName} is Invalid)}");

         RuleFor(b => b.Rating)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} must not be empty");

         RuleFor(b => b.ISBN)
            .NotEmpty().WithMessage("{PropertyName} must not be empty");

         RuleFor(b => b.PublicationDate)
            .NotEmpty().WithMessage("{PropertyNam} must not be empty");
      }
   }
}

