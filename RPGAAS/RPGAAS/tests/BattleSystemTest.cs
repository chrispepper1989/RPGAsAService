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
            BattleSystem battleSystem = new BattleSystem(characterAttackPower, characterHealth, new ModifierRepository());
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
        void When_CharacterAttacksOtherUntilHealthIsGone_CharacterIsDead()
        {
            // arrange
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
            BattleSystem battleSystem = new BattleSystem(characterAttackPower, characterHealth, new ModifierRepository());
            
            
            battleSystem.Attack("Hero", "Ogre");
            battleSystem.GetHealth("Ogre").Should().Be(20);
            
            battleSystem.Attack("Hero", "Ogre");
            battleSystem.GetHealth("Ogre").Should().Be(10);
            
            battleSystem.Attack("Hero", "Ogre");
            // assert
            // I Expect
            battleSystem.GetHealth("Ogre").Should().Be(0);
            battleSystem.GetHealth("Hero").Should().Be(50);
            battleSystem.IsDead("Ogre").Should().Be(true);
            battleSystem.IsDead("Hero").Should().Be(false);
        }
        
        [Fact]
        void When_CharacterHasBuffAttack_BecomesStronger()
        {
            // arrange
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
            ModifierRepository modifierRepository = new ModifierRepository();
            BattleSystem battleSystem = new BattleSystem(characterAttackPower, characterHealth, modifierRepository);
            
            modifierRepository.AddModifier("Hero", new IncreaseAttackPowerBy(5));
            battleSystem.Attack("Hero", "Ogre");
            
       
            // assert
            // I Expect
            battleSystem.GetHealth("Ogre").Should().Be(15);
            battleSystem.GetHealth("Hero").Should().Be(50);
            battleSystem.IsDead("Ogre").Should().Be(false);
            battleSystem.IsDead("Hero").Should().Be(false);
        }
        
        [Fact]
        void When_CharacterHasBuffAttackMultiplier_BecomesStronger()
        {
            // arrange
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
            ModifierRepository modifierRepository = new ModifierRepository();
            BattleSystem battleSystem = new BattleSystem(characterAttackPower, characterHealth, modifierRepository);
            
            modifierRepository.AddModifier("Hero", new IncreaseAttackMultiplier(2));
            battleSystem.Attack("Hero", "Ogre");
            
       
            // assert
            battleSystem.GetHealth("Ogre").Should().Be(10);
            battleSystem.GetHealth("Hero").Should().Be(50);
            battleSystem.IsDead("Ogre").Should().Be(false);
            battleSystem.IsDead("Hero").Should().Be(false);
        }
        
        [Fact]
        void When_CharacterIsAtLowHealth_TriggersSpecialModifier()
        {
            // arrange
            Dictionary<string, int> characterAttackPower = new Dictionary<string, int>
            {
                {"Hero", 10},
                {"Ogre", 6}
            };
            Dictionary<string, int> characterHealth = new Dictionary<string, int>
            {
                {"Hero", 1},
                {"Ogre", 50}
            };
            
            // act
            ModifierRepository modifierRepository = new ModifierRepository();

            BattleSystem battleSystem = new BattleSystem(characterAttackPower, characterHealth, modifierRepository);
        
            modifierRepository.AddModifier("Hero", new IncreaseAttackMultiplier(2).WhenHealth( health => health < 5 ));
            battleSystem.Attack("Hero", "Ogre");
            
       
            // assert
            // I Expect special multiplier to apply
            battleSystem.GetHealth("Ogre").Should().Be(30);
            battleSystem.GetHealth("Hero").Should().Be(1);
            battleSystem.IsDead("Ogre").Should().Be(false);
            battleSystem.IsDead("Hero").Should().Be(false);
        }
        
        
        
        [Fact]
        //todo this is now a modifier repo test really
        void When_CharacterHasBuffMultipliersAndIncrease_IncreaseHappensBeforeMultiplier()
        {
            // arrange
            Dictionary<string, int> characterAttackPower = new Dictionary<string, int>
            {
                {"Hero", 10},
                {"Ogre", 6}
            };
            Dictionary<string, int> characterHealth = new Dictionary<string, int>
            {
                {"Hero", 50},
                {"Ogre", 50}
            };
            
            // act
            ModifierRepository modifierRepository = new ModifierRepository();

            BattleSystem battleSystem = new BattleSystem(characterAttackPower, characterHealth, modifierRepository);
            
            modifierRepository.AddModifier("Hero", new IncreaseAttackPowerBy(5));
            modifierRepository.AddModifier("Hero", new IncreaseAttackMultiplier(2));
            modifierRepository.AddModifier("Hero", new IncreaseAttackPowerBy(5));
            battleSystem.Attack("Hero", "Ogre");
            
       
            // assert
            // I Expect multipliers to applied last
            battleSystem.GetHealth("Ogre").Should().Be(10);
            battleSystem.GetHealth("Hero").Should().Be(50);
            battleSystem.IsDead("Ogre").Should().Be(false);
            battleSystem.IsDead("Hero").Should().Be(false);
        }
    }
}