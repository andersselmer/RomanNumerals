namespace RomanNumerals.Domain.Models
{
    public record RomanNumeralDefinition(NumberPlace DefinitionFor, char FactorCalculation, char? Factor5Char, char? Factor10Char) : IRomanNumeralDefinition;
}
