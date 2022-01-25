using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        GameObject temp = other.transform.root.gameObject;
        Debug.Log(temp.tag);
        if (gameObject.transform.root.CompareTag("health") && temp.CompareTag("Player"))
        {
            CharacterMovement characterMovement = temp.GetComponent<CharacterMovement>();
            if (characterMovement.Health - characterMovement.currentHealth <= 20)
            {
                characterMovement.currentHealth = characterMovement.Health;
            }
            else
            {
                characterMovement.currentHealth += 20;
            }
            Destroy(gameObject);
        }
        if (gameObject.transform.root.CompareTag("ammo") && temp.CompareTag("Player"))
        {
            ActiveWeapon activeWeapon = temp.GetComponent<ActiveWeapon>();
            if (activeWeapon.active_weapon_index != -1)
            {
                RaycastWeapon raycastWeapon = activeWeapon.equiped_weapon[activeWeapon.active_weapon_index];
                if (raycastWeapon.magazine - raycastWeapon.bulletsLeft <= 50)
                {
                    raycastWeapon.bulletsLeft = raycastWeapon.magazine;
                }
                else
                {
                    raycastWeapon.bulletsLeft += 50;
                }
                Destroy(gameObject);
            }
        }
    }
}
