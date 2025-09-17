using UnityEngine;

public class Health_LB1 : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 3;

    private bool isDestroyed = false;

    public void takeDamage(int dmg) {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
