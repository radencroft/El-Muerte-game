using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //PLAYER
    public static bool player_facingRight = true;

    //INVENTORY 
    public static int savedWeaponID = 0;
    //...................................................// [ID] [UNLOCKED] NAME BULLETS
    public static Weapon weapon_nothing          = new Weapon(0, true, "nothing", 0);
    public static Weapon weapon_original_shotgun = new Weapon(1, false, "pistol A", 5);
    public static Weapon weapon_platinum_shotgun = new Weapon(2, false, "shotgun A", 10);

    //LEVEL
    public static int level = 1;
     
}
