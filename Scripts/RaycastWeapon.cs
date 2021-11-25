using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    #region Public Variables

    public ActiveWeapon.WeaponSlot weaponSlot;
    public Transform rayCastDestination;
    
    public ParticleSystem[] muzzleFlash; 
    public ParticleSystem hitEffect;
    public Transform rayCastOrigin;
    public TrailRenderer bulletTracer;
    public float fireRate, timeBetweenBullets, range;
    public int bulletsPerTap;
    public int bulletsFired, bulletsLeft, magazine;
    public Vector2 spreadValues;
    public GameObject magazine_gameobject;
    public string weaponName;



    #endregion

    #region Private Variables

    bool inRange;
    public bool canFire = false;
    
    [SerializeField]
    public Ray ray;

    [SerializeField]
    public RaycastHit hitInfo;
    float lastShot = 0f;

    #endregion

    private void Start()
    {
        bulletsFired = 0;
        bulletsLeft = magazine;

    }

    public void StartFiring()
    {
        canFire = true;
        if (Time.time > fireRate + lastShot && bulletsLeft>0)
        {
            FireBullet();
            lastShot = Time.time;
        }

    }
    public void FireBullet()
    {
            foreach (var ps in muzzleFlash)
            {
                ps.Emit(1);
            }

            ray.origin = rayCastOrigin.position;
            ray.direction = rayCastDestination.position - rayCastOrigin.position + new Vector3(Random.Range(-spreadValues.x, spreadValues.x), Random.Range(-spreadValues.y, spreadValues.y), 0f);
            
            
            var currentBulletTracerEffect = Instantiate(bulletTracer, ray.origin, Quaternion.identity);
            currentBulletTracerEffect.AddPosition(ray.origin);

            if (Physics.Raycast(ray, out hitInfo, range))
            {
                // Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1f);
                hitEffect.transform.position = hitInfo.point;
                hitEffect.transform.forward = hitInfo.normal;
                hitEffect.Emit(1);
                currentBulletTracerEffect.transform.position = hitInfo.point;

            }
        bulletsFired++;
        bulletsLeft--;
        if (bulletsLeft > 0)
        {
            if (bulletsFired < bulletsPerTap)
                Invoke("FireBullet", timeBetweenBullets);
            else
                bulletsFired = 0;
        }
        else
            bulletsFired = 0;

        Destroy(currentBulletTracerEffect.gameObject, 0.5f);
        
    }

    public void Reload()
    {
        bulletsLeft = magazine;
    }

     public void StopFiring()
    {
        canFire = false;
    }
}
