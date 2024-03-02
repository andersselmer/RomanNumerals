using RomanNumerals.Domain.Models;
using RomanNumerals.Domain.Repositories;
using RomanNumerals.Domain.Validation;

namespace RomanNumerals.Domain.Converters
{
    public static class ToRomanNumeral
    {
        public static string Convert(string input)
        {
            (int thousands, int hundreds, int tens, int units) = ParseAsNumberPlaces(input);

            static string getStringForNumberPlace(
                int value, 
                IRomanNumeralDefinitionRepository repository, 
                NumberPlace numberPlace)
            {
                IRomanNumeralDefinition definition = repository.Get(numberPlace);

                return Convert(value, definition);
            }

            RomanNumeralDefinitionRepository repository = new();

            string thousandsPart = getStringForNumberPlace(thousands, repository, NumberPlace.Thousand);

            string hundredsPart = getStringForNumberPlace(hundreds, repository, NumberPlace.Hundred);

            string tensPart = getStringForNumberPlace(tens, repository, NumberPlace.Ten);

            string unitsPart = getStringForNumberPlace(units, repository, NumberPlace.Unit);

            return string.Concat(thousandsPart, hundredsPart, tensPart, unitsPart);
        }

        internal static (short Thousands, short Hundreds, short Tens, short Ones) ParseAsNumberPlaces(string input)
        {
            bool inputIsValid = ToRomanNumeralInput.NumberInputIsValid(input);

            if (!inputIsValid)
                throw new ArgumentException(string.Format("The provided input '{0}' is invalid", input));

            ReadOnlySpan<char> digits = input.AsSpan();

            static short Convert(ReadOnlySpan<char> chars, int idx) => (short)(chars[idx] - '0');

            return digits.Length switch
            {
                1 => (0, 0, 0, Convert(digits, 0)),
                2 => (0, 0, Convert(digits, 0), Convert(digits, 1)),
                3 => (0, Convert(digits, 0), Convert(digits, 1), Convert(digits, 2)),
                4 => (Convert(digits, 0), Convert(digits, 1), Convert(digits, 2), Convert(digits, 3)),
                _ => throw new InvalidOperationException()
            };
        }

        // TODO: unit tests
        internal static string Convert(int numberPlaceValue, IRomanNumeralDefinition definition)
        {
            if (numberPlaceValue == 0)
                return string.Empty;

            string getRepeatedChars(int repeatCount) => new(definition.FactorCalculation, repeatCount);

            string getSubtractChars(char subtractFrom) => string.Concat(new string(definition.FactorCalculation, 1), new string(subtractFrom, 1));

            string getAddChars(char addTo, int addCount) => string.Concat(new string(addTo, 1), getRepeatedChars(addCount));

            if (definition.Factor5Char is char factor5Char && definition.Factor10Char is char factor10Char)
            {
                return numberPlaceValue switch
                {
                    int value when value == 1 => getRepeatedChars(1),
                    int value when value == 2 => getRepeatedChars(2),
                    int value when value == 3 => getRepeatedChars(3),
                    int value when value == 4 => getSubtractChars(factor5Char),
                    int value when value == 5 => new string(factor5Char, 1),
                    int value when value == 6 => getAddChars(factor5Char, 1),
                    int value when value == 7 => getAddChars(factor5Char, 2),
                    int value when value == 8 => getAddChars(factor5Char, 3),
                    int value when value == 9 => getSubtractChars(factor10Char),
                    _ => throw new Exception()
                } ;
            }

            return numberPlaceValue switch
            {
                int value when value == 1 => getRepeatedChars(1),
                int value when value == 2 => getRepeatedChars(2),
                int value when value == 3 => getRepeatedChars(3),
                _ => throw new Exception()
            };
        }
    }
}
