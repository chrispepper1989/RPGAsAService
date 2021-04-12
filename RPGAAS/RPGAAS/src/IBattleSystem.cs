namespace RPGAAS
{
    public interface IBattleSystem
    {
        public void Attack(string attackingCharacter, string defendingCharacter);
        public int GetHealth(string character);
        public bool IsDead(string character);

      
    }
}