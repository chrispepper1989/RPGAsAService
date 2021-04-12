using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace RPGAAS
{
    public class BattleSystemTest
    {
        private BattleSystem battleSystem;
        private ModifierRepository modifierRepository;
        public BattleSystemTest()
        {
           
        }

        private void LoadStandardTestData()
        {
            //standard test data
            /*
            * |Char | health | attack power |
            * |Hero | 50     | 5           |
            * |Ogre | 30     | 6           |
            */
            Dictionary<string, int> characterAttackPower = new Dictionary<string, int>
            {
                {"Hero", 10},
                {"Ogre", 10}
            };
            Dictionary<string, int> characterHealth = new Dictionary<string, int>
            {
                {"Hero", 50},
                {"Ogre", 100}
            };
            Dictionary<string, string> characterType =
                new Dictionary<string, string> //todo character feels like it should possibly be in object now?
                {
                    {"Hero", "Fire"},
                    {"Ogre", "Fire"}
                };

            modifierRepository = new ModifierRepository();
            battleSystem = new BattleSystem(characterAttackPower, characterHealth, characterType, modifierRepository);
        }

        [Fact]
        void When_CharacterAttacksOther_HealthIsTaken()
        {
            LoadStandardTestData();
            battleSystem.Attack("Hero", "Ogre");
            
            // assert
            // I Expect
            /*
             * |Char | health | attack power |
             * |Hero | 50     | 5           |
             * |Ogre | 25     | 6           |
             */
            battleSystem.GetHealth("Ogre").Should().Be(90);
            battleSystem.GetHealth("Hero").Should().Be(50);
            battleSystem.IsDead("Ogre").Should().Be(false);
            battleSystem.IsDead("Hero").Should().Be(false);
        }
        
        [Fact]
        void When_CharacterAttacksOtherUntilHealthIsGone_CharacterIsDead()
        {
            LoadStandardTestData();
            for (var i = 0; i < 10; ++i)
            {
                battleSystem.Attack("Hero", "Ogre");
            }
          
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
            LoadStandardTestData();
            modifierRepository.AddModifier("Hero", new IncreaseAttackPowerBy(5));
            battleSystem.Attack("Hero", "Ogre");
            
       
            // assert
            // I Expect
            battleSystem.GetHealth("Ogre").Should().Be(85);
            battleSystem.GetHealth("Hero").Should().Be(50);
            battleSystem.IsDead("Ogre").Should().Be(false);
            battleSystem.IsDead("Hero").Should().Be(false);
        }
        
        [Fact]
        void When_CharacterHasBuffAttackMultiplier_BecomesStronger()
        {
            LoadStandardTestData();
            modifierRepository.AddModifier("Hero", new IncreaseAttackMultiplier(2));
            battleSystem.Attack("Hero", "Ogre");
            
       
            // assert
            battleSystem.GetHealth("Ogre").Should().Be(80);
            battleSystem.GetHealth("Hero").Should().Be(50);
            battleSystem.IsDead("Ogre").Should().Be(false);
            battleSystem.IsDead("Hero").Should().Be(false);
        }
        
        [Fact]
        void When_CharacterIsAtLowHealth_TriggersSpecialModifier()
        {
            
            Dictionary<string, int> characterAttackPower = new Dictionary<string, int>
            {
                {"Hero", 10},
                {"Ogre", 10}
            };
            Dictionary<string, int> characterHealth = new Dictionary<string, int>
            {
                {"Hero", 1},
                {"Ogre", 100}
            };
            Dictionary<string, string> characterType =
                new Dictionary<string, string> //todo character feels like it should possibly be in object now?
                {
                    {"Hero", "Fire"},
                    {"Ogre", "Fire"}
                };

            modifierRepository = new ModifierRepository();
            battleSystem = new BattleSystem(characterAttackPower, characterHealth, characterType, modifierRepository);
            
            modifierRepository.AddModifier("Hero", new IncreaseAttackMultiplier(2).WhenHealth( health => health < 5 ));
            battleSystem.Attack("Hero", "Ogre");
            
            // assert
            // I Expect special multiplier to apply
            battleSystem.GetHealth("Ogre").Should().Be(80);
            battleSystem.GetHealth("Hero").Should().Be(1);
            battleSystem.IsDead("Ogre").Should().Be(false);
            battleSystem.IsDead("Hero").Should().Be(false);
        }


        [Fact]
        void When_CharacterTypeIsAdvantage_DealsDoubleDamage()
        {
            Dictionary<string, int> characterAttackPower = new Dictionary<string, int>
            {
                {"Hero", 10},
                {"Ogre", 10}
            };
            Dictionary<string, int> characterHealth = new Dictionary<string, int>
            {
                {"Hero", 10},
                {"Ogre", 100}
            };
            Dictionary<string, string> characterType =
                new Dictionary<string, string> //todo character feels like it should possibly be in object now?
                {
                    {"Hero", "Fire"},
                    {"Ogre", "Ice"}
                };

            modifierRepository = new ModifierRepository();
            battleSystem = new BattleSystem(characterAttackPower, characterHealth, characterType, modifierRepository);
            
 
            battleSystem.Attack("Hero", "Ogre");
            
       
            // assert
            // I Expect special multiplier to apply
            battleSystem.GetHealth("Ogre").Should().Be(80);
            battleSystem.GetHealth("Hero").Should().Be(10);
            battleSystem.IsDead("Ogre").Should().Be(false);
            battleSystem.IsDead("Hero").Should().Be(false);
        }


        [Fact]
        //todo this is now a modifier repo test really
        void When_CharacterHasBuffMultipliersAndIncrease_IncreaseHappensBeforeMultiplier()
        {
            LoadStandardTestData();
      
            modifierRepository.AddModifier("Hero", new IncreaseAttackPowerBy(5));
            modifierRepository.AddModifier("Hero", new IncreaseAttackMultiplier(2));
            modifierRepository.AddModifier("Hero", new IncreaseAttackPowerBy(5));
            battleSystem.Attack("Hero", "Ogre");
            
       
            // assert
            // I Expect multipliers to applied last
            battleSystem.GetHealth("Ogre").Should().Be(60);
            battleSystem.GetHealth("Hero").Should().Be(50);
            battleSystem.IsDead("Ogre").Should().Be(false);
            battleSystem.IsDead("Hero").Should().Be(false);
        }
    }
}