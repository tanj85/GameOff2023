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

    public GameObject GenerateWeapon(GameObject weapon, Vector3 position){
        GameObject newWeapon = Instantiate(weapon, position, Quaternion.identity);
        Weapon weaponComponent = newWeapon.GetComponent<Weapon>();
        weaponComponent.attackDamage = Random.Range(minAttackDamage, maxAttackDamage);
        weaponComponent.attackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
        if (Random.Range(0f, 1f) >= 0.5f){
            //weaponComponent.weaponType = weapon.WeaponType.Melee;
            weaponComponent.weaponType = "Melee";
            Sprite weaponSprite = meleeWeaponSprites[Random.Range(0, meleeWeaponSprites.Length)];
            weaponComponent.sprite = weaponSprite;
            weapon.GetComponent<SpriteRenderer>().sprite = weaponSprite;
        } else {
            //weaponComponent.weaponType = weapon.WeaponType.Ranged;
            weaponComponent.weaponType = "Ranged";
            Sprite weaponSprite = rangedWeaponSprites[Random.Range(0, rangedWeaponSprites.Length)];
            weaponComponent.sprite = weaponSprite;
            weapon.GetComponent<SpriteRenderer>().sprite = weaponSprite;
        }
        return newWeapon;
    }

    [ContextMenu("Generate Random weapon")]
    public void TestWeaponGeneration(){
        GenerateWeapon(weaponPrefab, transform.position);
    }
}
