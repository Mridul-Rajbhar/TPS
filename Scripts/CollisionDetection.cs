using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    Sound sound;
    AudioSource audioSource;
    UIHealthScript uIHealthScript;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<Sound>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {

        GameObject temp = other.transform.root.gameObject;
        Debug.Log(temp.tag);
        if (temp.CompareTag("Player"))
        {
            Debug.Log("collider: Player");
            if (gameObject.transform.root.CompareTag("health"))
            {
                CharacterMovement characterMovement = temp.GetComponent<CharacterMovement>();
                if (characterMovement.Health - characterMovement.currentHealth <= 50)
                {
                    characterMovement.currentHealth = characterMovement.Health;
                }
                else
                {
                    characterMovement.currentHealth += 50;


                }
                sound.play_pick_up(audioSource);
                Destroy(gameObject.transform.root.gameObject);

            }
            if (gameObject.transform.root.CompareTag("ak47"))
            {
                ActiveWeapon activeWeapon = temp.GetComponent<ActiveWeapon>();
                if (activeWeapon.active_weapon_index != -1)
                {
                    RaycastWeapon raycastWeapon = activeWeapon.equiped_weapon[0];
                    if (raycastWeapon)
                    {
                        if (raycastWeapon.total_bullets == 0)
                        {
                            raycastWeapon.total_bullets = raycastWeapon.magazine;
                        }
                        else if (raycastWeapon.total_bullets == raycastWeapon.gun_capacity)
                        {
                            raycastWeapon.bulletsLeft = raycastWeapon.magazine;
                        }
                        else
                        {
                            raycastWeapon.total_bullets += raycastWeapon.magazine;
                        }
                        sound.play_pick_up(audioSource);
                        Destroy(gameObject.transform.root.gameObject);

                    }
                }

            }
            if (gameObject.transform.root.CompareTag("pistol"))
            {
                ActiveWeapon activeWeapon = temp.GetComponent<ActiveWeapon>();
                if (activeWeapon.active_weapon_index != -1)
                {
                    RaycastWeapon raycastWeapon = activeWeapon.equiped_weapon[1];
                    if (raycastWeapon)
                    {
                        if (raycastWeapon.magazine - raycastWeapon.bulletsLeft <= 50)
                        {
                            raycastWeapon.bulletsLeft = raycastWeapon.magazine;
                        }
                        else
                        {
                            raycastWeapon.total_bullets += raycastWeapon.reload_capacity;
                        }
                        sound.play_pick_up(audioSource);
                        Destroy(gameObject.transform.root.gameObject);



                    }
                }

            }
        }
    }
}
