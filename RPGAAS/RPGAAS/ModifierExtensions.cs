using System;

namespace RPGAAS
{
    static class ModifierExtensions
    {
        public static ICharacterModifier WhenHealth(this ICharacterModifier mod, Func<int, bool> evaluate)
        {
            return new WhenHealth(evaluate, mod);
        }
    }
}