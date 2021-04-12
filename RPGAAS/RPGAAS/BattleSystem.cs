using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace RPGAAS
{
    public class BattleSystem : IBattleSystem
    {
        private Dictionary<string, int> characterAttackPower;
        private Dictionary<string, int> characterHealth;
        private Dictionary<string, List<ICharacterModifier>> mods = new Dictionary<string, List<ICharacterModifier>>();
        public BattleSystem(Dictionary<string, int> characterAttackPower, Dictionary<string, int> characterHealth)
        {
            this.characterAttackPower = characterAttackPower;
            this.characterHealth = characterHealth;
        }
        
        public void Attack(string attackingCharacter, string defendingCharacter)
        {
            var buffs = GetModifiers(attackingCharacter);
            var attackPower = characterAttackPower[attackingCharacter];
            var enemyHealth = characterHealth[defendingCharacter];
          
            foreach (var buff in buffs)
            {
                attackPower = buff.ModifyAttackPower(attackPower);
            }
            
            characterHealth[defendingCharacter] -= attackPower;
        }

        private List<ICharacterModifier> GetModifiers(string attackingCharacter)
        {
            return mods.ContainsKey(attackingCharacter) ? mods[attackingCharacter] : new List<ICharacterModifier>();
        }

        public int GetHealth(string character)
        {
            return characterHealth[character];
        }

        public bool IsDead(string character)
        {
            return GetHealth(character) <= 0;
        }

        public void AddModifier(string character, ICharacterModifier modifier)
        {
            if (!mods.ContainsKey(character))
            {
                mods[character] = new List<ICharacterModifier>();
            }
            mods[character].Add(modifier);
        }
    }
}