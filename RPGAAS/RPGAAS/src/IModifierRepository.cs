using System.Collections.Generic;

namespace RPGAAS
{
    public interface IModifierRepository
    {
        public IEnumerable<ICharacterModifier> GetModifiers(string attackingCharacter);
        public void AddModifier(string character, ICharacterModifier modifier);
    }
}