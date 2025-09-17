using UnityEngine;

public class Health_LB1 : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 3;

    public void takeDamage(int dmg) {
        hitPoints -= dmg;

        if (hitPoints <= 0) {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
