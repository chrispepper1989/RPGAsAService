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
        public void Attack(string attackingCharacter, string defendingCharacter)
        {
            throw new System.NotImplementedException();
        }

        public int GetHealth(string character)
        {
            throw new System.NotImplementedException();
        }

        public bool IsDead(string character)
        {
            throw new System.NotImplementedException();
        }
    }
}