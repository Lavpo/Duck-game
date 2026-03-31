using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;   

public class Gun : MonoBehaviour
{
    //Gun stats 
    [Header("Gun stats")]
    public float damage, speed, mass;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    [Header("Gun stats (with gravity involved)")]
    public float xdistance, yacceleration;
    public float angle;

    // LineRenderer trajectoryRender;
    [Header("Trajectory")]
    Vector2 DragStartPosition;
    Vector2 DragEndPosition;
    Vector2 velocity;


    public float stepDistance;
    public float force;
    public int maxTrajectoryInerations;
    [SerializeField] private GameObject bulletPrefab;
    private Rigidbody2D bulletRB;


    //bools
    bool shooting, readyToShoot, reloading, buttonpressed;

    //Reference
    [SerializeField] private Transform gunTip;

    [SerializeField] private LayerMask whatIsEnemy;
    private void Start()
    {
        // trajectoryRender = GetComponent<LineRenderer>();
        bulletRB = bulletPrefab.GetComponent<Rigidbody2D>();
    }
    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
        GunRaycast();
    }

    private void GunRaycast()
    {
        //Draws raycast where a gun points to.
        if (Input.GetKeyDown(KeyCode.K) && !buttonpressed) buttonpressed = true;
        if (buttonpressed) Debug.DrawRay(gunTip.transform.position, gunTip.transform.right * 100, Color.blue);
        if (Input.GetKeyDown(KeyCode.I) && buttonpressed) buttonpressed = false;
    }
    private void MyInput()
    {
        //Shooting
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot 
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0f) 
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }

        //Slows down a player while reloading
        //if (reloading) Player.GetComponent<PlayerMovement>().SpeedReducer();
        //else if (!reloading) Player.GetComponent<PlayerMovement>().SpeedNormaliser();

        //Slows down player while holding right click
        // if (Input.GetKey(KeyCode.Mouse1)) gameObject.GetComponent<PlayerMovement>().SpeedReducer();
        // else if (!Input.GetKey(KeyCode.Mouse1)) gameObject.GetComponent<PlayerMovement>().SpeedNormaliser();
    }
    private void Reload()
    {
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
        Debug.Log("Reloading...");
    }
    private void Shoot()
    {
        float y = Random.Range(-spread, spread);
        Vector2 direction = (gunTip.transform.right + (Vector3)gunTip.transform.up * y).normalized;
        readyToShoot = false;

        Vector2 hitPoint = gunTip.position + (Vector3)direction * range;

        // [ WILL BE USED LATER FOR A LASER GUN]

        //if (buttonpressed) Debug.DrawRay(gunTip.transform.position, direction * 100, Color.cyan, 2);
        ////Spawns bullet to shoot
        //RaycastHit2D rayHit = Physics2D.Raycast(gunTip.position, direction, range, whatIsEnemy);

        //TrailRenderer trailInstance = Instantiate(bullettrail, gunTip.position, Quaternion.identity);

        //GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
        //bullet.GetComponent<Rigidbody2D>().velocity = direction * 25;
        //Destroy(bullet, 2f);

        //if (rayHit.collider != null)
        //{
        //    hitPoint = rayHit.point;

        //    // Delay damage: pass collider to coroutine
        //    StartCoroutine(DelayedHit(null, hitPoint));
        //}
        //else
        //{
        //    // Trail ends at full range
        //    StartCoroutine(DelayedHit(null, hitPoint));
        //}

        //if (rayHit.collider != null)
        //{
        //    Debug.Log("Raycast hit the target" + rayHit.collider.name);
        //    if (rayHit.collider.CompareTag("Enemy"))
        //    {
        //        rayHit.collider.GetComponent<Enemy_Moving>().Damage(damage);
        //    }  
        //}


        // Instantiates an object on a scene and than adjusts current values  
        GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);

        // initializes bullet's speed and damage using IProjectileInitializer interface 
        IProjectileInitializer bs = bullet.GetComponent<IProjectileInitializer>();

        bs.Initialise(speed, damage);

        // Adjusts projectile properties 
        Invoke(nameof(ResetShot), timeBetweenShooting);

        bulletsLeft--;
        bulletsShot--;

        if (bulletsLeft > 0 && bulletsShot > 0)
            Invoke(nameof(Shoot), timeBetweenShots);
        }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        Debug.Log("Reloaded!!!");
    }

    // public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    // {
    //     //if your camera is set to Perspective use Vector3
    //     //if your camera is set to Orthographic use Vector2
 
    //     Vector2[] results = new Vector2[steps];
 
    //     float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations * stepDistance;
    //     Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
 
    //     float drag = 1f - timestep * rigidbody.drag;
    //     Vector2 moveStep = velocity * timestep;
 
    //     for (int i = 0; i < steps; i++)
    //     {
    //         moveStep += gravityAccel;
    //         moveStep *= drag;
    //         pos += moveStep;
    //         results[i] = pos;
    //     }
    //     return results;
    // }


    //later can be used to adjust damage and bullet speed from this script

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    Enemy_Moving enemy = collider.GetComponent<Enemy_Moving>();
    //    if (collider.CompareTag("Enemy"))
    //    {
    //        enemy.Damage(damage);
    //    }
    //    Destroy(bulletPrefab);
    //}

    //(this part of a script will be used for laser gun mechanics)
    //private IEnumerator DelayedHit(Collider2D target, Vector3 hitPoint)
    //{
    //    TrailRenderer trail = Instantiate(bullettrail, gunTip.position, Quaternion.identity);

    //    float duration = 0.05f; // Time it takes to reach the target
    //    float elapsed = 0f;

    //    Vector3 startPos = gunTip.position;

    //    while (elapsed < duration)
    //    {
    //        trail.transform.position = Vector3.Lerp(startPos, hitPoint, elapsed / duration);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    trail.transform.position = hitPoint;

    //if (target != null && target.CompareTag("Enemy"))
    //{
    //    Enemy_Moving enemy = target.GetComponent<Enemy_Moving>();
    //    if (enemy != null)
    //    {
    //        enemy.Damage(damage);
    //    }
    //}
    //    if (target != null && target.CompareTag("Enemy"))
    //    {
    //        Enemy_Moving enemy = target.GetComponent<Enemy_Moving>();
    //        if (enemy != null)
    //        {
    //            Destroy(trail.gameObject, trail.time); // Cleanup
    //        }
    //    }
    //    Destroy(trail.gameObject, trail.time);
    //    yield return null;
    //}
}
