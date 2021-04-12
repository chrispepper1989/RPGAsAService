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
            
            
            // act
            // john attack ogre once
            BattleSystem battleSystem = new BattleSystem();
            battleSystem.Attack("Hero", "Ogre");
            // assert
            // I Expect
            /*
             * |Char | health | attack power |
             * |John | 50     | 5           |
             * |Ogre | 25     | 6           |
             */
            battleSystem.GetHealth("Ogre").Should().Be(25);
            battleSystem.GetHealth("John").Should().Be(50);
            battleSystem.IsDead("John").Should().Be(false);
            battleSystem.IsDead("John").Should().Be(false);
        }
        
        
    }
}