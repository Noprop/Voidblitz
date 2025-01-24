using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private WeaponScript weapon;


    void Start() {
       weapon = GetComponentInChildren<WeaponScript>(); 
    }

    void Update() {
        if (weapon != null && weapon.CanAttack) {
            weapon.Attack(false);
        }
    }
}
