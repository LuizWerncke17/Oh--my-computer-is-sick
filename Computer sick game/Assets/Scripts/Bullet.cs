using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attribute")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bps = 1f; //Bullets Per Second
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float timeSinceSpawn;


    private Vector2 moveDirection; // dire��o fixa que a bala vai seguir

    private void Update()
    {
        EliminateBullet();
    }


    // Chamado pelo canh�o na hora do disparo
    public void SetDirection(Vector2 targetPosition)
    {
        // calcula dire��o a partir da posi��o do alvo na hora do disparo
        moveDirection = (targetPosition - (Vector2)transform.position).normalized;
    }

    public void EliminateBullet()
    {
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn > 5)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        // sempre anda em linha reta nessa dire��o
        rb.linearVelocity = moveDirection * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health_LB1>().takeDamage(bulletDamage);
        Destroy(gameObject);
        
    }
}
