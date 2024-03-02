using RomanNumerals.Domain.Models;

namespace RomanNumerals.Domain.Repositories
{
    public class RomanNumeralDefinitionRepository : IRomanNumeralDefinitionRepository
    {
        private readonly Dictionary<NumberPlace, IRomanNumeralDefinition> _romanNumeralDefinition = new()
        {
            { NumberPlace.Unit, new RomanNumeralDefinition(NumberPlace.Unit, 'I', 'V', 'X') },
            { NumberPlace.Ten, new RomanNumeralDefinition(NumberPlace.Ten, 'X', 'L', 'C') },
            { NumberPlace.Hundred, new RomanNumeralDefinition(NumberPlace.Hundred, 'C', 'D', 'M') },
            { NumberPlace.Thousand, new RomanNumeralDefinition(NumberPlace.Thousand, 'M', default, default) }
        };

        public IRomanNumeralDefinition Get(NumberPlace numberPlace)
        {
            bool resolved = _romanNumeralDefinition.TryGetValue(numberPlace, out IRomanNumeralDefinition? definition);

            if (!resolved || definition is null)
                throw new Exception(string.Format("No definition resolved for the number place {0}", numberPlace));

            return definition;
        }
    }
}
