namespace RomanNumerals.Domain.Models
{
    public interface IRomanNumeralDefinition
    {
        NumberPlace DefinitionFor { get; }

        char FactorCalculation { get; }

        char? Factor5Char { get; }

        char? Factor10Char { get; }
    }
}
