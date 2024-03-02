using RomanNumerals.Domain.Models;

namespace RomanNumerals.Domain.Extensions
{
    public static class NumberPlaceExtensions
    {
        public static int ToFactor(this NumberPlace numberPlace)
        {
            return numberPlace switch
            {
                NumberPlace.Unit => 1,
                NumberPlace.Ten => 10,
                NumberPlace.Hundred => 100,
                NumberPlace.Thousand => 1000,
                _ => throw new ArgumentException(string.Format("Unknown/unexpected {0} enum variant: {1}", nameof(NumberPlace), numberPlace))
            };
        }
    }
}
