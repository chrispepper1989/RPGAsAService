﻿using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace RPGAAS
{
    public class BattleSystemTest
    {
        [Fact]
        void When_CharacterAttacksOther_HealthIsTaken()
        {
            // arrange
            /*
             * |Char | health | attack power |
             * |Hero | 50     | 5           |
             * |Ogre | 30     | 6           |
             */
            Dictionary<string, int> characterAttackPower = new Dictionary<string, int>
            {
                {"Hero", 5},
                {"Ogre", 6}
            };
            Dictionary<string, int> characterHealth = new Dictionary<string, int>
            {
                {"Hero", 50},
                {"Ogre", 30}
            };
            
            // act
            // john attack ogre once
            BattleSystem battleSystem = new BattleSystem(characterAttackPower, characterHealth);
            battleSystem.Attack("Hero", "Ogre");
            // assert
            // I Expect
            /*
             * |Char | health | attack power |
             * |Hero | 50     | 5           |
             * |Ogre | 25     | 6           |
             */
            battleSystem.GetHealth("Ogre").Should().Be(25);
            battleSystem.GetHealth("Hero").Should().Be(50);
            battleSystem.IsDead("Ogre").Should().Be(false);
            battleSystem.IsDead("Hero").Should().Be(false);
        }
        
        [Fact]
        void When_CharacterAttacksOtherUntilHealthIsGone_CharacerIsDead()
        {
            // arrange
            /*
             * |Char | health | attack power |
             * |Hero | 50     | 5           |
             * |Ogre | 30     | 6           |
             */
            Dictionary<string, int> characterAttackPower = new Dictionary<string, int>
            {
                {"Hero", 10},
                {"Ogre", 6}
            };
            Dictionary<string, int> characterHealth = new Dictionary<string, int>
            {
                {"Hero", 50},
                {"Ogre", 30}
            };
            
            // act
            // john attack ogre once
            BattleSystem battleSystem = new BattleSystem(characterAttackPower, characterHealth);
            
            
            battleSystem.Attack("Hero", "Ogre");
            battleSystem.GetHealth("Ogre").Should().Be(20);
            
            battleSystem.Attack("Hero", "Ogre");
            battleSystem.GetHealth("Ogre").Should().Be(10);
            
            battleSystem.Attack("Hero", "Ogre");
            // assert
            // I Expect
            /*
             * |Char | health | attack power |
             * |Hero | 50     | 5           |
             * |Ogre | 25     | 6           |
             */
            battleSystem.GetHealth("Ogre").Should().Be(0);
            battleSystem.GetHealth("Hero").Should().Be(50);
            battleSystem.IsDead("Ogre").Should().Be(true);
            battleSystem.IsDead("Hero").Should().Be(false);
        }
        
        
    }
}