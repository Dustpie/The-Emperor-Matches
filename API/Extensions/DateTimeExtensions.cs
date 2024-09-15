namespace API.Extensions;

/**
 * This class contains extension methods for the DateTime class
 */
public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var age = today.Year - dob.Year;
        if (today < dob.AddYears(-age)) age--;

        return age;
    }
}