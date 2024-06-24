using Microsoft.AspNetCore.Mvc;

namespace AgeCalculateApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AgeCalculateController : ControllerBase
{

    private readonly ILogger<AgeCalculateController> _logger;

    public AgeCalculateController(ILogger<AgeCalculateController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "AgeCalculate")]
    public string Get()
    {
        return "Storage Birthday is not setup yet";
    }

    [HttpPost(Name = "AgeCalculate")]
    public string AgeCalculation(string birthday)
    {
        //Checks for an empty string, but swagger enforces the parameter
        if (String.IsNullOrEmpty(birthday))
        {
            //Throws an error saying the birthday field was empty
            throw new Exception("Birthday is empty!  Fix it!");
        }

        DateTime birthdayDate;
        bool isDate = DateTime.TryParse(birthday, out birthdayDate); //tries to parse a date
        if (isDate)
        {
            TimeSpan birthdayDiff = DateTime.Now.Subtract(birthdayDate);
            int daysOld = birthdayDiff.Days;
            int yearsOld = daysOld / 365; //divides by 365 for how many days
            return yearsOld.ToString();
        }
        else{
            //Logs that the birthday was not formatted correctly and returns the string
            _logger.LogError("Birthday was not formatted correctly: " + birthday);
            return "birthday was not formatted correctly";
        }
    }
}
