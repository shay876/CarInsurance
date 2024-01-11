using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarInsurance.Models
{
    public class GetQuote
    {

      // calculate age of insuree
      DateTime insureeBday = insuree.DateOfBirth;
      DateTime currentDate = DateTime.Now;
      var insureeAge = currentDate.Year - insureeBday.Year;
      Console.WriteLine(insureeAge);
            
      //Calculate Quote
      //age restrictions
      if (insureeAge< 18 )
      { insuree.Quote = insuree.Quote + 100; }
      else if (insureeAge > 18 && insureeAge< 26)
      { insuree.Quote = insuree.Quote + 50}
      else
      { insuree.Quote = insuree.Quote + 25}

//car year restrictions
if (insuree.CarYear < 2000 || insuree.CarYear > 2015)
{ insuree.Quote = insuree.Quote + 25}

//car make restrictions
if (insuree.CarMake == "Porsche")
{ insuree.Quote = insuree.Quote + 25}

if (insuree.CarMake == "Porsche" && insuree.CarModel == "911 Carrera")
{ insuree.Quote = insuree.Quote + 50}





    }
}