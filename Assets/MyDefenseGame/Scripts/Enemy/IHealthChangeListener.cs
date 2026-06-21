namespace MyDefenseGame
{
    public interface IHealthChangeListener
    {
        void OnHealthChanged(float currentHealth, float maxHealth);
    }
}