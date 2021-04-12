namespace RPGAAS
{
    internal class IncreaseAttackMultiplier : ICharacterModifier
    {
        private int _multiplier;
        public IncreaseAttackMultiplier(int amount)
        {
            _multiplier = amount;
        }

        public int Priority { get; } = 1;

        public int ModifyAttackPower(int currentAttackPower, int characterHealth)
        {
            return currentAttackPower * _multiplier;
        }
    }
}