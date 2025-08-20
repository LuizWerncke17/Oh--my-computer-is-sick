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

    private Vector2 moveDirection; // direção fixa que a bala vai seguir


    // Chamado pelo canhão na hora do disparo
    public void SetDirection(Vector2 targetPosition)
    {
        // calcula direção a partir da posição do alvo na hora do disparo
        moveDirection = (targetPosition - (Vector2)transform.position).normalized;
    }

    private void FixedUpdate()
    {
        // sempre anda em linha reta nessa direção
        rb.linearVelocity = moveDirection * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Ainda tem que tirar a vida do inimigo
        Destroy(gameObject);
        
    }
}
