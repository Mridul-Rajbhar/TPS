
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public RaycastWeapon weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if(activeWeapon)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefab);
            activeWeapon.Equip(newWeapon);
        }    
    }
}

/*
 * -11.3 -20.3 -56.3
 * -78.746 -226.569  -61.064
 * 
 * 6 -29 -52
 * -77.277 252.683 -338.158
 * 
 * 40 -16 242
 * -6.397 5.265 3.248
 */
