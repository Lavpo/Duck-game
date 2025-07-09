using System.Collections;
using System.Collections.Generic;
using UnityEngine;   

public class Gun : MonoBehaviour
{
    //Gun stats 
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    [SerializeField]
    private TrailRenderer bullettrail;

    [SerializeField] private GameObject bulletPrefab;

    //bools
    bool shooting, readyToShoot, reloading, buttonpressed;

    //Reference
    [SerializeField] private Transform gunTip;
    private GameObject Player;
    [SerializeField] private LayerMask whatIsEnemy;
    private void Start()
    {
        Player = GameObject.Find(nameof(Player));
    }
    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
        //Drawing raycast
        if (Input.GetKeyDown(KeyCode.K) && !buttonpressed) buttonpressed = true;
        if (buttonpressed) Debug.DrawRay(gunTip.transform.position, gunTip.transform.right * 100, Color.blue);
        if (Input.GetKeyDown(KeyCode.I) && buttonpressed)
        {
            buttonpressed = false;
        }
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

        //Slows down player while reloading
        //if (reloading) Player.GetComponent<PlayerMovement>().SpeedReducer();
        //else if (!reloading) Player.GetComponent<PlayerMovement>().SpeedNormaliser();

        //Slows down player while holding right click
        if (Input.GetKey(KeyCode.Mouse1)) Player.GetComponent<PlayerMovement>().SpeedReducer();
        else if (!Input.GetKey(KeyCode.Mouse1)) Player.GetComponent<PlayerMovement>().SpeedNormaliser();
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

        // [ WILL BE USED LATER FOR LASER GUN]

        //if (buttonpressed) Debug.DrawRay(gunTip.transform.position, direction * 100, Color.cyan, 2);
        ////Spawns bullet to shoot
        //RaycastHit2D rayHit = Physics2D.Raycast(gunTip.position, direction, range, whatIsEnemy);

        //TrailRenderer trailInstance = Instantiate(bullettrail, gunTip.position, Quaternion.identity);

        //GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
        //bullet.GetComponent<Rigidbody2D>().velocity = direction * 25;
        //Destroy(bullet, 2f);

        Vector2 hitPoint = gunTip.position + (Vector3)direction * range;

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

        Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);

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
    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    Enemy_Moving enemy = collider.GetComponent<Enemy_Moving>();
    //    if (collider.CompareTag("Enemy"))
    //    {
    //        enemy.TakeDamage(damage);
    //    }
    //    Destroy(bulletpref);
    //}

    //(this part of a script will be used for laser gun mechanics)
    private IEnumerator DelayedHit(Collider2D target, Vector3 hitPoint)
    {
        TrailRenderer trail = Instantiate(bullettrail, gunTip.position, Quaternion.identity);

        float duration = 0.05f; // Time it takes to reach the target
        float elapsed = 0f;

        Vector3 startPos = gunTip.position;

        while (elapsed < duration)
        {
            trail.transform.position = Vector3.Lerp(startPos, hitPoint, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        trail.transform.position = hitPoint;

        //if (target != null && target.CompareTag("Enemy"))
        //{
        //    Enemy_Moving enemy = target.GetComponent<Enemy_Moving>();
        //    if (enemy != null)
        //    {
        //        enemy.Damage(damage);
        //    }
        //}
        if (target != null && target.CompareTag("Enemy"))
        {
            Enemy_Moving enemy = target.GetComponent<Enemy_Moving>();
            if (enemy != null)
            {
                Destroy(trail.gameObject, trail.time); // Cleanup
            }
        }
        Destroy(trail.gameObject, trail.time);
        yield return null;
    }
}
