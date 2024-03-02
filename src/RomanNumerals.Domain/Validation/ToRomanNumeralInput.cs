namespace RomanNumerals.Domain.Validation
{
    internal static class ToRomanNumeralInput
    {
        internal static bool NumberInputIsValid(string s)
        {
            bool parsed = short.TryParse(s, out short number);

            if (!parsed)
                return false;

            bool minimumValueValidation = number > 0;

            if (!minimumValueValidation)
                return false;

            bool maximumValueValidation = number < 3000;

            if (!maximumValueValidation)
                return false;

            return true;
        }
    }
}
