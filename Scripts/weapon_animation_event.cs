
using UnityEngine;

public class weapon_animation_event : MonoBehaviour
{
    [SerializeField]
    ActiveWeapon active_Weapon;

    RaycastWeapon active_raycast_weapon;
    GameObject magazine_in_hand;

    #region Public Variables
    public Transform left_hand_IK;
    #endregion


    public void attach_magazine()
    {
        active_raycast_weapon.magazine_gameobject.SetActive(true);
        Destroy(magazine_in_hand);
        Invoke("setReload",1);
    }

    void setReload()
    {
        active_Weapon.isReload = false;
    }

    public void detach_magazine()
        {
            active_raycast_weapon = active_Weapon.GetWeapon(active_Weapon.active_weapon_index);
            magazine_in_hand = Instantiate(active_raycast_weapon.magazine_gameobject, left_hand_IK,true);
            active_raycast_weapon.magazine_gameobject.SetActive(false);
        }

    public void drop_magazine()
        {
            magazine_in_hand.transform.parent = null;
            magazine_in_hand.AddComponent<Rigidbody>();
            magazine_in_hand.AddComponent<BoxCollider>();
            Destroy(magazine_in_hand, 2f);
        }


    public void pick_magazine()
        {
        active_raycast_weapon = active_Weapon.GetWeapon(active_Weapon.active_weapon_index);
        magazine_in_hand = Instantiate(active_raycast_weapon.magazine_gameobject, left_hand_IK, true);
            
        }
}
