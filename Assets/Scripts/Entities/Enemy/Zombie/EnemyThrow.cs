using UnityEngine;

public class EnemyThrower : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectilePrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwAngle = 45f;
    [SerializeField] private float throwSpeed = 10f;
    [SerializeField] private float throwInterval = 2f;
    [SerializeField] private int damage = 1;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= throwInterval)
        {
            ThrowObject();
            timer = 0f;
        }
    }

    private void ThrowObject()
    {
        if (projectilePrefab == null || throwPoint == null) return;

        Rigidbody2D projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);

        Rock projectileScript = projectile.GetComponent<Rock>();
        if (projectileScript != null)
        {
            projectileScript.SetDamage(damage);
        }

        float angleInRadians = throwAngle * Mathf.Deg2Rad;
        Vector2 throwDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        projectile.linearVelocity = throwDirection.normalized * throwSpeed;
    }
}