using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    #region Public Variables

    public AudioSource audioSource;
    public ActiveWeapon activeWeapon;

    public ActiveWeapon.WeaponSlot weaponSlot;
    public Transform rayCastDestination;

    public ParticleSystem[] muzzleFlash;
    public Transform rayCastOrigin;
    public TrailRenderer bulletTracer;
    public float fireRate, timeBetweenBullets, range;
    public int bulletsPerTap;
    public int bulletsFired, bulletsLeft, magazine, total_bullets, reload_capacity, gun_capacity;
    public Vector2 spreadValues;
    public GameObject magazine_gameobject;
    public string weaponName;
    public bool canFire = false;
    public int damage;


    #endregion

    #region Private Variables

    bool inRange;

    [SerializeField]
    public Ray ray;

    [SerializeField]
    public RaycastHit hitInfo;
    float lastShot = 0f;

    public ParticleSystem hitEffect, bloodEffect;
    #endregion

    private void Start()
    {
        bulletsFired = 0;
        bulletsLeft = magazine;
        audioSource = GetComponent<AudioSource>();
        if (transform.root.gameObject.CompareTag("enemy"))
        {
            gameObject.AddComponent<AiShoot>();

        }

    }

    public void StartFiring(Vector3 target)
    {
        canFire = true;
        if (bulletsLeft == 0 && transform.root.CompareTag("enemy"))
        {
            bulletsLeft = 10000;
        }
        if (Time.time > fireRate + lastShot && bulletsLeft > 0)
        {
            FireBullet(target);
            lastShot = Time.time;
            if (transform.root.CompareTag("Player"))
            {
                if (weaponName == "handgun")
                    activeWeapon.sound.play_riffle_sound(audioSource);
                else
                    activeWeapon.sound.play_pistol_shoot(audioSource);
            }
        }
        else if (gameObject.transform.root.CompareTag("Player") && bulletsLeft == 0 && total_bullets > 0)
        {
            Reload();
        }
    }

    public void FireBullet(Vector3 target)
    {
        foreach (var ps in muzzleFlash)
        {
            ps.Emit(1);
        }

        ray.origin = rayCastOrigin.position;
        ray.direction = target - rayCastOrigin.position + new Vector3(Random.Range(-spreadValues.x, spreadValues.x), Random.Range(-spreadValues.y, spreadValues.y), 0f);


        var currentBulletTracerEffect = Instantiate(bulletTracer, ray.origin, Quaternion.identity);
        currentBulletTracerEffect.AddPosition(ray.origin);

        LayerMask avoidBorder = (1 << 9);
        avoidBorder = ~avoidBorder;

        if (Physics.Raycast(ray, out hitInfo, range, avoidBorder))
        {
            // Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1f);

            currentBulletTracerEffect.transform.position = hitInfo.point;
            var hitBox = hitInfo.collider.GetComponent<HitBox>();
            var Idamageble = hitInfo.collider.transform.root.GetComponent<IDamageable>();
            if (hitBox)
            {

                hitBox.OnRaycastHit(this, ray.direction);
                Debug.Log("hit info");
            }
            else if (Idamageble != null)
            {
                Idamageble.TakeDamage(damage, ray.direction);
                Debug.Log("idamagable");
            }
            if (hitBox)
            {
                bloodEffect.transform.position = hitInfo.point;
                bloodEffect.transform.forward = hitInfo.normal;
                bloodEffect.Emit(1);
            }
            else
            {
                hitEffect.transform.position = hitInfo.point;
                hitEffect.transform.forward = hitInfo.normal;
                hitEffect.Emit(1);
            }


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
        if (total_bullets > magazine)
        {
            total_bullets -= (magazine - bulletsLeft);
            bulletsLeft = magazine;

        }
        else
        {
            bulletsLeft = total_bullets;
            total_bullets = 0;

        }
        activeWeapon.rigController.SetTrigger("reload");
    }

    public void StopFiring()
    {
        canFire = false;
    }
}
