using UnityEngine;

public class EnemyScript : HealthBase {
    private Weapon weapon;

    void Start() {
       weapon = GetComponentInChildren<Weapon>(); 
    }

    void Update() {
        if (weapon != null && weapon.CanAttack) {
            weapon.Attack(false);
        }
    }
}
