using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Rotation Settings")]
    [SerializeField]
    private float rotationSpeed = 180f; //velocidade que o inimigo gira

    private ForwardAxis forwardAxis = ForwardAxis.Up; //aqui define a direção que é a frente do inimigo

    private bool initialFaceOnStart = true; //aponta para o primeiro ponto de path caso for verdadeiro

    private enum ForwardAxis { Up }

    private Transform target;
    private int pathIndex = 0;

    // Controle interno de rotação
    private float targetAngle;
    private bool rotatingToNewTarget = false;

    private bool shouldRotate = false;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];

        string enemyName = gameObject.name.ToLower(); 

        if (enemyName.Contains("malware") || enemyName.Contains("maniacware"))
        {
            shouldRotate = true;
        }

        if (initialFaceOnStart && target != null && shouldRotate)
            UpdateTargetRotation(target.position, instant: true);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                Health_LB1 health = GetComponent<Health_LB1>();
                LevelManager.main.life -= health != null ? health.Damage : 1;
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];

                if (shouldRotate && target != null)
                    UpdateTargetRotation(target.position, instant: false);
            }

            if (LevelManager.main.life <= 0)
            {
                LevelManager.main.life = 0;
                LevelManager.main.money = 0;
                Debug.Log("Game Over!");
                Destroy(gameObject);
            }
        }

        if (shouldRotate && rotatingToNewTarget)
            SmoothRotateTowardsTarget();
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    private void UpdateTargetRotation(Vector3 worldPos, bool instant)
    {
        Vector2 dir = (worldPos - transform.position);
        if (dir.sqrMagnitude <= Mathf.Epsilon) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (forwardAxis == ForwardAxis.Up)
            angle -= 90f;

        targetAngle = angle;
        rotatingToNewTarget = true;

        if (instant)
        {
            SetRotation(angle);
            rotatingToNewTarget = false;
        }
    }

    private void SmoothRotateTowardsTarget()
    {
        float currentAngle = rb != null ? rb.rotation : transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        SetRotation(newAngle);

        if (Mathf.Abs(Mathf.DeltaAngle(newAngle, targetAngle)) < 0.5f)
            rotatingToNewTarget = false;
    }

    private void SetRotation(float angle)
    {
        if (rb != null)
            rb.rotation = angle;
        else
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
