using Intive_Patronage.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Intive_Patronage.Tests
{

   public class AuthorTestsMethods
   {

      public bool checkFirstName(Author author)
      {
         var firstName = author.FirstName;
         if(firstName.Length > 50 || firstName.Length <= 1 
            || !Char.IsUpper(firstName[0]) || firstName.Any(char.IsDigit))
            return false;

         return true;
      }

      public bool checkLastName(Author author)
      {
         var lastName = author.LastName;
         if (lastName.Length > 50 || lastName.Length <= 1
            || !Char.IsUpper(lastName[0]) || lastName.Any(char.IsDigit))
            return false;

         return true;
      }
   }
}
