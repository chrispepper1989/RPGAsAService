using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace RPGAAS
{

   
    public interface IBattleSystem
    {
        public void Attack(string attackingCharacter, string defendingCharacter);
        public int GetHealth(string character);
        public bool IsDead(string character);
    }
    
    public class BattleSystem : IBattleSystem
    {
        private Dictionary<string, int> characterAttackPower;
        private Dictionary<string, int> characterHealth;

        public BattleSystem(Dictionary<string, int> characterAttackPower, Dictionary<string, int> characterHealth)
        {
            this.characterAttackPower = characterAttackPower;
            this.characterHealth = characterHealth;
        }
        
        public void Attack(string attackingCharacter, string defendingCharacter)
        {
            var attackPower = characterAttackPower[attackingCharacter];
            characterHealth[defendingCharacter] -= attackPower;
        }

        public int GetHealth(string character)
        {
            return characterHealth[character];
        }

        public bool IsDead(string character)
        {
            return GetHealth(character) <= 0;
        }
    }
}