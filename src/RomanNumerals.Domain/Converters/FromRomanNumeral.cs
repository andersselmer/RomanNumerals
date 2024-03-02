using RomanNumerals.Domain.Extensions;
using RomanNumerals.Domain.Models;
using RomanNumerals.Domain.Repositories;
using RomanNumerals.Domain.Validation;

namespace RomanNumerals.Domain.Converters
{
    public static class FromRomanNumeral
    {
        public static int Convert(string input)
        {
            bool inputIsValid = FromRomanNumeralInput.StringInputIsValidDumb(input);

            if (!inputIsValid)
                throw new ArgumentException(string.Format("The provided input '{0}' is invalid", input));

            static (bool Matched, string? MatchedRomanNumeral, int Value) applyDefinition(IRomanNumeralDefinitionRepository repository, NumberPlace numberPlace, ReadOnlySpan<char> chars)
            {
                IRomanNumeralDefinition definition = repository.Get(numberPlace);

                return TryMatchWithDefinition(definition, chars);
            }

            static ReadOnlySpan<char> getNextSlice(bool match, string? matchRomanNumeral, ReadOnlySpan<char> currentSlice)
            {
                return match && matchRomanNumeral is string romanNumeral ? currentSlice.Slice(romanNumeral.Length) : currentSlice;
            }

            RomanNumeralDefinitionRepository repository = new();

            ReadOnlySpan<char> inputChars = input.AsSpan();

            // try to match for a thousands number

            var thousandsMatchResult = applyDefinition(repository, NumberPlace.Thousand, inputChars);

            var afterThousandsSlice = getNextSlice(thousandsMatchResult.Matched, thousandsMatchResult.MatchedRomanNumeral, inputChars);

            // try to match for a hundreds number

            var hundredsMatchResult = applyDefinition(repository, NumberPlace.Hundred, afterThousandsSlice);

            var afterHundredSlice = getNextSlice(hundredsMatchResult.Matched, hundredsMatchResult.MatchedRomanNumeral, afterThousandsSlice);

            // try to match for a tens number

            var tensMatchResult = applyDefinition(repository, NumberPlace.Ten, afterHundredSlice);

            var afterTensSlice = getNextSlice(tensMatchResult.Matched, tensMatchResult.MatchedRomanNumeral, afterHundredSlice);

            // try to match for a units number

            var unitsMatchResult = applyDefinition(repository, NumberPlace.Unit, afterTensSlice);

            return thousandsMatchResult.Value + hundredsMatchResult.Value + tensMatchResult.Value + unitsMatchResult.Value;
        }

        // TODO: refactor + unit tests
        internal static (bool Matched, string? MatchedRomanNumeral, int Value) TryMatchWithDefinition(IRomanNumeralDefinition definition, ReadOnlySpan<char> slice)
        {
            int factor = definition.DefinitionFor.ToFactor();

            ReadOnlySpan<char> one = new string(definition.FactorCalculation, 1).AsSpan();
            ReadOnlySpan<char> two = new string(definition.FactorCalculation, 2).AsSpan();
            ReadOnlySpan<char> three = new string(definition.FactorCalculation, 3).AsSpan();

            if (definition.Factor5Char is char base5 && definition.Factor10Char is char base10)
            {
                ReadOnlySpan<char> five = new string(base5, 1).AsSpan();
                ReadOnlySpan<char> ten = new string(base10, 1).AsSpan();
                ReadOnlySpan<char> four = string.Concat(one, five).AsSpan();
                ReadOnlySpan<char> six = string.Concat(five, one).AsSpan();
                ReadOnlySpan<char> seven = string.Concat(five, two).AsSpan();
                ReadOnlySpan<char> eight = string.Concat(five, three).AsSpan();
                ReadOnlySpan<char> nine = string.Concat(one, ten).AsSpan();

                if (slice.StartsWith(nine))
                {
                    return (true, nine.ToString(), 9 * factor);
                }
                else if (slice.StartsWith(eight))
                {
                    return (true, eight.ToString(), 8 * factor);
                }
                else if (slice.StartsWith(seven))
                {
                    return (true, seven.ToString(), 7 * factor);
                }
                else if (slice.StartsWith(six))
                {
                    return (true, six.ToString(), 6 * factor);
                }
                else if (slice.StartsWith(five))
                {
                    return (true, five.ToString(), 5 * factor);
                }
                else if (slice.StartsWith(four))
                {
                    return (true, four.ToString(), 4 * factor);
                }
                else
                {
                    return (false, default, 0);
                }
            }

            if (slice.StartsWith(three))
            {
                return (true, three.ToString(), 3 * factor);
            }
            else if (slice.StartsWith(two))
            {
                return (true, two.ToString(), 2 * factor);
            }
            else if (slice.StartsWith(one))
            {
                return (true, one.ToString(), 1 * factor);
            }
            else
            {
                return (false, default, 0);
            }
        }
    }
}
