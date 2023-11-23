using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    public int minAttackDamage;
    public int maxAttackDamage;
    public int minAttackCooldown;
    public int maxAttackCooldown;
    public GameObject weaponPrefab;
    public Sprite[] meleeWeaponSprites;
    public Sprite[] rangedWeaponSprites;

    public GameObject GenerateWeapon(Vector3 position){
        GameObject newWeapon = Instantiate(weaponPrefab, position, Quaternion.identity);
        Weapon weaponComponent = newWeapon.GetComponent<Weapon>();
        weaponComponent.attackDamage = Random.Range(minAttackDamage, maxAttackDamage);
        weaponComponent.attackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
        if (Random.Range(0f, 1f) >= 0.5f){
            weaponComponent.weaponType = "Melee";
            newWeapon.GetComponent<SpriteRenderer>().sprite = meleeWeaponSprites[Random.Range(0, meleeWeaponSprites.Length)];
        } else {
            weaponComponent.weaponType = "Ranged";
            newWeapon.GetComponent<SpriteRenderer>().sprite = rangedWeaponSprites[Random.Range(0, rangedWeaponSprites.Length)];
        }
        return newWeapon;
    }

    [ContextMenu("Generate Random weapon")]
    public void TestWeaponGeneration(){
        GenerateWeapon(transform.position);
    }
}
