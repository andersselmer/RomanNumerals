using RomanNumerals.Domain.Converters;
using Xunit;

namespace RomanNumerals.Domain.Tests.Converters
{
    public class FromRomanNumeralTests
    {
        [Theory]
        [InlineData("XC", 90)]
        [InlineData("MCMXCIX", 1999)]
        [InlineData("MMCDXLIV", 2444)]
        [InlineData("MMCMXCIX", 2999)]
        public void Convert_ValidInput_ReturnsValue(string input, short expected)
        {
            // act

            int actual = FromRomanNumeral.Convert(input);

            // assert

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("i")]
        public void Convert_InvalidInput_ThrowsException(string input)
        {
            // act

            Action action = () => FromRomanNumeral.Convert(input);

            //assert

            ArgumentException exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal($"The provided input '{input}' is invalid", exception.Message);
        }
    }
}
