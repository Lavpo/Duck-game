public class HealthSystem
{
    private int health;
    private int maxHealth;
    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }
    public int GetHealth()
    {
        return health;
    }
    public void DamageUnit(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }
    public void HealUnit(int heal)
    {
        health += heal;
        if (health < 0)
        {
            health = 0;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
