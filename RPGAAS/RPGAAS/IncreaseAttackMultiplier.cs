namespace RPGAAS
{
    internal class IncreaseAttackMultiplier : ICharacterModifier
    {
        private int _multiplier;
        public IncreaseAttackMultiplier(int amount)
        {
            _multiplier = amount;
        }

        public int ModifyAttackPower(int normalAttack)
        {
            return normalAttack * _multiplier;
        }
    }
}