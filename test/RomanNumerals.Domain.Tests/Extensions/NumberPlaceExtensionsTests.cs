using RomanNumerals.Domain.Extensions;
using RomanNumerals.Domain.Models;
using Xunit;

namespace RomanNumerals.Domain.Tests.Extensions
{
    public class NumberPlaceExtensionsTests
    {
        [Theory]
        [InlineData(NumberPlace.Unit, 1)]
        [InlineData(NumberPlace.Ten, 10)]
        [InlineData(NumberPlace.Hundred, 100)]
        [InlineData(NumberPlace.Thousand, 1000)]
        public void ToFactor(NumberPlace numberPlace, int expected)
        {
            // act

            int actual = NumberPlaceExtensions.ToFactor(numberPlace);

            // assert

            Assert.Equal(expected, actual);
        }
    }
}
