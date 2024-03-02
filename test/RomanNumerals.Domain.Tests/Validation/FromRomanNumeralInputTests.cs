using RomanNumerals.Domain.Validation;
using Xunit;

namespace RomanNumerals.Domain.Tests.Validation
{
    public class FromRomanNumeralInputTests
    {
        [Theory]
        [InlineData("a", false)]
        [InlineData("0", false)]
        [InlineData("1", false)]
        [InlineData("2999", false)]
        [InlineData("I", true)]
        [InlineData("III", true)]
        [InlineData("i", false)]
        [InlineData("IVXLCDM", true)]
        [InlineData("IVXLCDm", false)]
        public void StringInputIsValidDumb(string input, bool expected)
        {
            // act

            bool actual = FromRomanNumeralInput.StringInputIsValidDumb(input);

            // assert

            Assert.Equal(expected, actual);
        }

        public static HashSet<object[]> CharInputIsValidDumbTestData => new()
        {
            new object[] { new HashSet<char>(){ 'I', 'V', 'X' }, 'I', true },
            new object[] { new HashSet<char>(){ 'I', 'V', 'X' }, 'i', false },
            new object[] { Enumerable.Empty<char>().ToHashSet(), 'i', false },
        };

        [Theory]
        [InlineData('a', false)]
        [InlineData('I', true)]
        [InlineData('V', false)]
        [InlineData('X', true)]
        [InlineData('L', false)]
        [InlineData('C', true)]
        [InlineData('D', false)]
        [InlineData('M', true)]
        public void CanRepeat(char c, bool expected)
        {
            // act

            bool actual = FromRomanNumeralInput.CanRepeat(c);

            // assert

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(CharInputIsValidDumbTestData))]
        public void CharInputIsValidDumb(IReadOnlyCollection<char> chars, char c, bool expected)
        {
            // act

            bool actual = FromRomanNumeralInput.CharInputIsValidDumb(chars, c);

            // assert

            Assert.Equal(expected, actual);
        }
    }
}
