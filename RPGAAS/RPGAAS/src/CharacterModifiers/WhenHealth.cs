using System;

namespace RPGAAS
{
    public class WhenHealth : ICharacterModifier
    {
        private ICharacterModifier toApply;
        private Func<int, bool> trigger;
        public WhenHealth( Func<int, bool> trigger, ICharacterModifier toApply)
        {
            this.trigger = trigger;
            this.toApply = toApply;
        }

        public int Priority => 0;
        public int ModifyAttackPower(int currentAttackPower, int characterHealth)
        {
            if (trigger(characterHealth))
            {
                return toApply.ModifyAttackPower(currentAttackPower, characterHealth);
            }

            return currentAttackPower;
        }
    }
}