using RomanNumerals.Domain.Converters;
using Xunit;

namespace RomanNumerals.Domain.Tests.Converters
{
    public class ToRomanNumeralTests
    {
        [Theory]
        [InlineData("1", "I")]
        [InlineData("2", "II")]
        [InlineData("3", "III")]
        [InlineData("4", "IV")]
        [InlineData("5", "V")]
        [InlineData("10", "X")]
        [InlineData("20", "XX")]
        [InlineData("30", "XXX")]
        [InlineData("40", "XL")]
        [InlineData("50", "L")]
        [InlineData("100", "C")]
        [InlineData("200", "CC")]
        [InlineData("300", "CCC")]
        [InlineData("400", "CD")]
        [InlineData("500", "D")]
        [InlineData("1000", "M")]
        [InlineData("2000", "MM")]
        [InlineData("90", "XC")]
        [InlineData("1999", "MCMXCIX")]
        [InlineData("2444", "MMCDXLIV")]
        public void Convert(string input, string expected)
        {
            // act

            string actual = ToRomanNumeral.Convert(input);

            // assert

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 0, 0, 0, 1)]
        [InlineData("12", 0, 0, 1, 2)]
        [InlineData("123", 0, 1, 2, 3)]
        [InlineData("1234", 1, 2, 3, 4)]
        public void ParseAsNumberPlaces(string input, short expectedThousands, short expectedHundreds, short expectedTens, short expectedOnes)
        {
            // act

            (short thousands, short hundreds, short tens, short ones) = ToRomanNumeral.ParseAsNumberPlaces(input);

            //assert

            Assert.Equal(expectedThousands, thousands);

            Assert.Equal(expectedHundreds, hundreds);

            Assert.Equal(expectedTens, tens);

            Assert.Equal(expectedOnes, ones);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("0")]
        [InlineData("3000")]
        public void ParseAsNumberPlaces_ThrowsException(string input)
        {
            // act

            Action action = () => ToRomanNumeral.ParseAsNumberPlaces(input);

            //assert

            ArgumentException exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal($"The provided input '{input}' is invalid", exception.Message);
        }
    }
}
