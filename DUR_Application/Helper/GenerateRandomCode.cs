namespace DUR_Application.Helper
{
    public static class GenerateRandomCode
    {
        public static string GenerateCode()
        {
            var Numbers = "0123456789";

            return new string(Enumerable
                .Repeat(Numbers, 5)
                .Select(s => s[Random.Shared.Next(Numbers.Length)])
                .ToArray());
        }
    }
}
