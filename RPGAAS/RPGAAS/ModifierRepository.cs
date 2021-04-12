using System.Collections.Generic;
using System.Linq;

namespace RPGAAS
{
    public class ModifierRepository : IModifierRepository
    {
        private Dictionary<string, List<ICharacterModifier>> _modifiers = new Dictionary<string, List<ICharacterModifier>>();
       
        public IEnumerable<ICharacterModifier> GetModifiers(string attackingCharacter)
        {
            var finalMods = _modifiers.ContainsKey(attackingCharacter) ? _modifiers[attackingCharacter] : new List<ICharacterModifier>();
            return finalMods.OrderBy(x => x.Priority);
        }
        
        public void AddModifier(string character, ICharacterModifier modifier)
        {
            if (!_modifiers.ContainsKey(character))
            {
                _modifiers[character] = new List<ICharacterModifier>();
            }
            _modifiers[character].Add(modifier);
        }
    }
}