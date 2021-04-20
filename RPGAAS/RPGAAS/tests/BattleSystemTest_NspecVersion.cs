using System.Collections.Generic;
using FluentAssertions;
using NSpec;
using NSpecInNUnit;

namespace RPGAAS
{

    

    /*
     * describe("when character attacks another character")
	

		
it("other character health goes down")
		it("other character dies when health below 0")
		it("health goes down by double attack power when the attacking character has type advantage")
     */
    
    class BattleSystemTests : nspec_as_nunit<BattleSystemTests>
    {
        IBattleSystem battleSystem;
        Dictionary<string, int> characterAttackPowerRepo = new Dictionary<string, int>
        {
            {"Hero", 10},
            {"Ogre", 10}
        };
        Dictionary<string, int> characterHealthRepo = new Dictionary<string, int>
        {
            {"Hero", 50},
            {"Ogre", 100}
        };
        Dictionary<string, string> characterTypeRepo =
            new Dictionary<string, string> //todo character feels like it should possibly be in object now?
            {
                {"Hero", "Fire"},
                {"Ogre", "Fire"}
            };
        
        //context methods are discovered by NSpec (any method that contains underscores)
        void describe_BattleSystem()
        {
            //contexts can be nested n-deep and contain befores and specifications
            context["characters are in battle"] = () =>
            {
                before = () =>
                {
                    ModifierRepository modifierRepository = new ModifierRepository();
                    battleSystem = new BattleSystem(characterAttackPowerRepo, characterHealthRepo, characterTypeRepo, modifierRepository);
                };


                context["character attacks another character"] = () =>
                {
                    before = () => battleSystem.Attack("Hero", "Ogre");
                    
                    it["defending characters' health goes down by attack character attack power"] = () =>
                    {
                        battleSystem.GetHealth("Ogre").Should().Be(90);
                    };
                    
                    it["other character dies when health below 0"] = () =>
                    {
                        for (int i = 0; i < 100; ++i)
                            battleSystem.Attack("Hero", "Ogre");
                            
                        battleSystem.IsDead("Ogre").Should().Be(true);
                    };
                };
            };
        }

        
    }
}