using RomanNumerals.Domain.Models;

namespace RomanNumerals.Domain.Repositories
{
    public interface IRomanNumeralDefinitionRepository
    {
        IRomanNumeralDefinition Get(NumberPlace numberPlace);
    }
}