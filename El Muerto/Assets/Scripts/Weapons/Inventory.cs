using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Animator anim; 
    private Weapon[] weapons = new Weapon[3];       // Moras da znas unapred koliko ces imati oruzja

    [HideInInspector] public int weapon_id;
    private int weapon_num, min, max;               //public?

    [Header("AMMO")]
    public int bullets;
     

    private void Awake()
    {
        anim = GetComponent<Animator>();
        weapon_id = 0;
    }

    void Start()
    {
        //WEAPON INITIALISATION
        AddWeapon(SaveManager.weapon_nothing);
        AddWeapon(SaveManager.weapon_original_shotgun);
        AddWeapon(SaveManager.weapon_platinum_shotgun);
         
        //RESET ID
        weapon_id = SaveManager.savedWeaponID; 
    }

    private void OnDestroy()
    {
        SaveManager.savedWeaponID = weapon_id;
    }

    private void Update()
    { 
        SwitchWeapon();
        AmmoCounter();
        WeaponNumCalculator();
        Restrictions();
    }

    private void AmmoCounter()
    {
        bullets = weapons[weapon_id].bullets;
    }

    public void ReduceBullet(int n)
    {
        weapons[weapon_id].bullets -= n;
    }


    private void Restrictions()
    {
        // CAN SHOOT? 
        if (weapon_id != 0) { GetComponent<PlayerOne>().stopShooting = false; }
        else { GetComponent<PlayerOne>().stopShooting = true; } 
    }
     

    private void AddWeapon(Weapon weapon)
    {
        weapons[weapon_id] = weapon;
        weapon_id++;
    }

    public void PickUpWeapon(string name)
    {
        anim.SetLayerWeight(FindWeaponID(FindWeaponByID(weapon_id).name), 0);
        weapon_id = FindWeaponByName(name).id;

        FindWeaponByName(name).unlocked = true;
        anim.SetLayerWeight(FindWeaponID(name), 1);
    }

    public void DropWeapon(string name)
    {
        weapon_id = 0;                      //Resets Id to empty handed
        FindWeaponByName(name).unlocked = false;
        anim.SetLayerWeight(FindWeaponID(name), 0);
    } 

    private void WeaponNumCalculator()
    {
        // Live weapon Counter; Clamp ID; Always remember MAX & MIN;
        min = 0;
        max = weapons.Length;
        Mathf.Clamp(weapon_id, min, max);

        // Number of UNLOCKED weapons
        weapon_num = 0;
        foreach (Weapon weapon in weapons)
        {
            if (weapon.unlocked == true) { weapon_num++; }
        }
    }

    // ID FINDER
    private Weapon FindWeaponByID(int id)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.id == id) { return weapon; }
        }
        Debug.Log("ERROR: '" + id + "' 'ID' not Found! #Inventory.cs");
        return null;
    }

    private int FindWeaponID(string name)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.name.Equals(name)) { return weapon.id; }
        }
        Debug.Log("ERROR: '" + name + "' identification *ID* not Found! #Inventory.cs");
        return 0;
    }


    // NAME FINDER 
    private Weapon FindWeaponByName(string name)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.name.Equals(name)) { return weapon; }
        }
        Debug.Log("ERROR: '" + name + "' weapon not Found! #Inventory.cs");
        return null;
    }


    // SWITCH WEAPONS

    void SwitchWeapon()
    {
        if (Input.GetButtonDown("ChangeWeapon") && !GetComponent<PlayerOne>().stopMovement)
        { 
            weapon_id++;                                            //Sledece oruzje(prebaci index) 
            if (weapon_id > weapon_num - 1) { weapon_id = min; }    //Resets index if out of range 
        }

        // ALWAYS PREVIEW ANIMATIONS
        anim.SetLayerWeight(FindWeaponID(FindWeaponByID(weapon_id).name), 1);
        for (int i = 1; i < max; i++)
        {
            if (i != weapon_id)
            {
                anim.SetLayerWeight(FindWeaponID(FindWeaponByID(i).name), 0);
            }
        }
    } 
}
