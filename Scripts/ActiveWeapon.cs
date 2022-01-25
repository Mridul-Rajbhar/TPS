using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;


public class ActiveWeapon : MonoBehaviour
{

    public enum WeaponSlot //There are two slots for weapon
    {
        Primary_weapon = 0,
        Secondary_weapon = 1
    }


    public Transform crossHairTarget, leftHandIK, rightHandIK;
    public RaycastWeapon[] equiped_weapon = new RaycastWeapon[2]; //RayCast Component of weapons
    public int active_weapon_index; //the active weapon.
    public bool isHolster, isReload;
    public Transform[] weapon_positions; // Weapon parent objects. Basically weapon instantiated will become child of one of the weapon_pos and its position will
    //be set on the basis of weapon_slot in weapon_body_aim.
    public Rig handIK;

    public Animator rigController;

    [SerializeField]
    Image crossHair;



    private Ray checkInRange;

    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon existWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existWeapon)
        {
            Equip(existWeapon);
        }
    }

    public RaycastWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= equiped_weapon.Length)
            return null;
        return equiped_weapon[index];
    }

    // Update is called once per frame
    void Update()
    {
        var weapon = GetWeapon(active_weapon_index);


        if (weapon)
        {
            checkInRange.origin = weapon.rayCastOrigin.position;
            checkInRange.direction = crossHairTarget.position - weapon.rayCastOrigin.position;

            rigController.SetBool("isSprinting", CharacterMovement.isSprinting);

            Debug.DrawRay(checkInRange.origin, checkInRange.direction * weapon.range, Color.red, 0.05f);
            if (Physics.Raycast(checkInRange, weapon.range))
            {
                crossHair.color = Color.red;
            }
            else
            {
                crossHair.color = Color.yellow;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                isReload = true;
                rigController.SetTrigger("reload");
            }

            if (Input.GetKey(KeyCode.Mouse0) && !isHolster && !CharacterMovement.isSprinting && !isReload) // Shooting
            {
                weapon.StartFiring(crossHairTarget.position);
            }
            else
            {
                weapon.StopFiring();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                //Debug.Log("hoster_" + weapon.weaponName + " : " + !rigController.GetBool("hoster_" + weapon.weaponName));
                Toggle(weapon.weaponName);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                SetActiveWeapon(WeaponSlot.Primary_weapon);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                SetActiveWeapon(WeaponSlot.Secondary_weapon);

        }
        else
            crossHair.color = Color.yellow;

    }


    void Toggle(string weapon_name)
    {
        bool isHolster = rigController.GetBool("hoster_" + weapon_name);
        if (isHolster)
        {
            StartCoroutine(Activate_Weapon(active_weapon_index));
        }
        else
        {
            StartCoroutine(Hoster_Weapon(active_weapon_index));
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot; //Each weapon will have slot index.
        var weapon = GetWeapon(weaponSlotIndex); //Get raycast component of weapon with that weaponSlotIndex.
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.rayCastDestination = crossHairTarget;
        weapon.transform.SetParent(weapon_positions[weaponSlotIndex], false); //weapon will become child of weapon_position[] on the basis of weaponSlotIndex(primary, secondary or small gun)
        weapon.transform.localPosition = Vector3.zero;

        rigController.SetInteger("weapon_index", weaponSlotIndex);
        //Debug.Log(weaponSlotIndex);
        equiped_weapon[weaponSlotIndex] = weapon; //current equiped weapon on that weapon_slot_index
        SetActiveWeapon(weapon.weaponSlot); //Finally setting the active weapon.
    }

    void SetActiveWeapon(WeaponSlot weaponSlotIndex)
    {

        int active_weapon = (int)weaponSlotIndex;//Now, weapon_slot_index will become active_weapon
        if ((active_weapon_index == active_weapon) || (equiped_weapon[active_weapon] == null))
            return;
        int hosterIndex = active_weapon_index; //current active_weapon_index becomes the hoster_index because we have to equip new weapon.

        StartCoroutine(SwitchWeapon(hosterIndex, active_weapon));
    }

    IEnumerator SwitchWeapon(int hosterIndex, int activateIndex)
    {
        yield return StartCoroutine(Hoster_Weapon(hosterIndex));
        yield return StartCoroutine(Activate_Weapon(activateIndex));
        active_weapon_index = activateIndex;
    }

    IEnumerator Hoster_Weapon(int index)
    {
        var weapon = GetWeapon(index);
        //Debug.Log("hoster_weapon: " + index);
        if (weapon)
        {
            isHolster = true;
            rigController.SetBool("hoster_" + weapon.weaponName, true);

            yield return new WaitForSeconds(rigController.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    IEnumerator Activate_Weapon(int index)
    {
        var weapon = GetWeapon(index);
        //var hoster_weapon = GetWeapon(hoster);
        if (weapon)
        {
            isHolster = false;
            rigController.SetBool("hoster_" + weapon.weaponName, false);
            rigController.Play("equip_" + weapon.weaponName);
            rigController.SetInteger("weapon_index", index);
            yield return new WaitForSeconds(rigController.GetCurrentAnimatorStateInfo(0).length);

        }
    }

    //[ContextMenu("Save Weapon Pose")]
    //void SaveWeaponPose()
    //{
    //    GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
    //    recorder.BindComponentsOfType<Transform>(WeaponPivot.gameObject, false);
    //    recorder.BindComponentsOfType<Transform>(leftHandIK.gameObject, false);
    //    recorder.BindComponentsOfType<Transform>(rightHandIK.gameObject, false);
    //    recorder.TakeSnapshot(0.0f);
    //    recorder.SaveToClip(weapon.weaponAnimation);
    //    UnityEditor.AssetDatabase.SaveAssets();

    //}

}
