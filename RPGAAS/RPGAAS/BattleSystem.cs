using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace RPGAAS
{
    public class BattleSystem : IBattleSystem
    {
        private Dictionary<string, int> _characterAttackPower;
        private Dictionary<string, int> _characterHealth;
        private IModifierRepository _characterModifiers; 
        public BattleSystem(Dictionary<string, int> characterAttackPower, Dictionary<string, int> characterHealth, IModifierRepository characterModifiers)
        {
            _characterModifiers = characterModifiers;
            _characterAttackPower = characterAttackPower;
            _characterHealth = characterHealth;
        }
        
        public void Attack(string attackingCharacter, string defendingCharacter)
        {
            var buffs = _characterModifiers.GetModifiers(attackingCharacter);
            var attackPower = _characterAttackPower[attackingCharacter];
          
            foreach (var buff in buffs)
            {
                attackPower = buff.ModifyAttackPower(attackPower, GetHealth(attackingCharacter));
            }
            
            _characterHealth[defendingCharacter] -= attackPower;
        }
        
        public int GetHealth(string character)
        {
            return _characterHealth[character];
        }

        public bool IsDead(string character)
        {
            return GetHealth(character) <= 0;
        }

        
    }
}