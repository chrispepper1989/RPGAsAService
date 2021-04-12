namespace RPGAAS
{
    public interface ICharacterModifier
    {  
        public int Priority { get; }
        public int ModifyAttackPower(int normalAttack);
    }
}