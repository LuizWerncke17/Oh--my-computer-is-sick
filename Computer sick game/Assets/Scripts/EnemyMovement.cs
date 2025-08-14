using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Refences")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    private void Start()
    {
        target = LevelManager.main.path[pathIndex]; //Esse cara aqui diz que o alvo do inimigo vai ser do outro programa, então indo ali eu consigo explicar.
    }

   
    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                Destroy(gameObject);
                return;
            } else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }
}
