using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder; 
    private Weapon weapon; 

    private void Awake()
    {
        if (weaponHolder == null)
        {
            Debug.LogError("WeaponHolder belum di-assign pada " + gameObject.name);
            return;
        }

        weapon = Instantiate(weaponHolder, transform.position, transform.rotation);
        
        weapon.transform.SetParent(transform);
        
        weapon.transform.localPosition = Vector3.zero;

        TurnVisual(false); 
    }

    private void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
            Debug.Log("Weapon initialized and visibility set to false");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Objek Player Memasuki trigger");
            
            Weapon existingWeapon = other.transform.GetComponentInChildren<Weapon>();
            
            if (existingWeapon != null)
            {
                Debug.Log("Mengembalikan weapon yang terpasang ke posisi pickup");
                existingWeapon.transform.SetParent(transform); 
                existingWeapon.transform.localPosition = Vector3.zero; 
                TurnVisual(false, existingWeapon); 
            }

            weapon.parentTransform = other.transform; 
            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = Vector3.zero;
            TurnVisual(true, weapon); 
        }
        else
        {
            Debug.Log("Bukan Objek Player yang memasuki Trigger");
        }
    }

    private void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
            Debug.Log("Weapon visibility set to: " + on);
        }
    }

    private void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
            Debug.Log("Weapon visibility set to: " + on + " for specific weapon.");
        }
    }
}
