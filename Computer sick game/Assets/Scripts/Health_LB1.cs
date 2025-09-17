using UnityEngine;

public class Health_LB1 : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 3;
    [SerializeField] private int moneyWorth = 50;

    private bool isDestroyed = false;

    public void takeDamage(int dmg) {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseMoney(moneyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
