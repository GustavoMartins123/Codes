using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public int weaponNumber;
    public void ChooseWeapon()
    {
        Save.weaponChoice = weaponNumber;
        Save.weaponChange = true;
        Save.carryingWeapon = true;
        AudioManager.instance.Sounds(18);
        if(weaponNumber == 0)
        {
            Player.anim.Play("ShortSword");
        }
        else if(weaponNumber == 1)
        {
            Player.anim.Play("LongSword");
        }
    }
}
