namespace RPGAAS
{
    internal class IncreaseAttackPowerBy : ICharacterModifier
    {
        private int _attackBuff;
        public IncreaseAttackPowerBy(int attackBuff)
        {
            _attackBuff = attackBuff;
        }

        public int ModifyAttackPower(int normalAttack)
        {
            return normalAttack + _attackBuff;
        }
    }
}