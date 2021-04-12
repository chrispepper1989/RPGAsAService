namespace RPGAAS
{
    internal class IncreaseAttackPowerBy : ICharacterModifier
    {
        private int _attackBuff;
        public IncreaseAttackPowerBy(int attackBuff)
        {
            _attackBuff = attackBuff;
        }

        public int Priority { get; } = 0;

        public int ModifyAttackPower(int currentAttackPower, int characterHealth)
        {
            return currentAttackPower + _attackBuff;
        }
    }
}