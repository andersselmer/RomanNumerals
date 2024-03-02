using RomanNumerals.Domain.Validation;
using Xunit;

namespace RomanNumerals.Domain.Tests.Validation
{
    public class ToRomanNumeralInputTests
    {
        [Theory]
        [InlineData("a", false)]
        [InlineData("0", false)]
        [InlineData("3000", false)]
        [InlineData("1", true)]
        [InlineData("2999", true)]
        public void NumberInputIsValid(string input, bool expected)
        {
            // act

            bool actual = ToRomanNumeralInput.NumberInputIsValid(input);

            // assert

            Assert.Equal(expected, actual);
        }
    }
}
