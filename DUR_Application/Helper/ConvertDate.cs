using DUR_Application.Exceptions;
using System.Reflection.Metadata.Ecma335;

namespace DUR_Application.Helper
{
    public class ConvertDate
    {
        public static DateTime ChangeToDateTime (string date)
        {
            var result = DateTime.TryParse(date, out DateTime dateTime);

            if (!result)
            {
                throw new BadRequestException($"Cannot convert date: {date}");
            }

            return dateTime;
        }
    }
}
