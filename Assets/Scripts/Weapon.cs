using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {
    [System.Serializable]
    public class WeaponEntry {
        public Transform shotPrefab;
        public float shootingRate = 0.3f;
        public bool isHoming = false;
    }

    public List<WeaponEntry> weapons = new List<WeaponEntry>();

    // Track separate cooldowns for each weapon slot
    private float[] shootCooldowns;

    private void Start() {
        shootCooldowns = new float[weapons.Count];
        for (int i = 0; i < shootCooldowns.Length; i++)
            shootCooldowns[i] = 0f;
    }

    private void Update() {
        for (int i = 0; i < shootCooldowns.Length; i++) {
            if (shootCooldowns[i] > 0f)
                shootCooldowns[i] -= Time.deltaTime;
        }
    }

    // Check if a particular weapon slot can attack
    public bool CanAttack(int weaponIndex) {
        if (0 <= weaponIndex && weaponIndex < weapons.Count)
            return shootCooldowns[weaponIndex] <= 0f;
        else
            return false;
    }

    public void Attack(int weaponIndex, bool isPlayer) {
        if (weaponIndex < 0 || weaponIndex >= weapons.Count)
            return;
        if (!CanAttack(weaponIndex)) 
            return;

        // Set new cooldown
        WeaponEntry wpn = weapons[weaponIndex];
        shootCooldowns[weaponIndex] = wpn.shootingRate;

        // Create shot in the world
        Transform shotTransform = Instantiate(wpn.shotPrefab, transform.position, Quaternion.identity);
        Shot shot = shotTransform.GetComponent<Shot>();
        if (shot != null) {
            shot.fromPlayer = isPlayer;
        }

        if (wpn.isHoming) {
            // If homing, we simply ensure the HomingMissile script knows its target type (player or enemy).
            HomingMissile homing = shotTransform.GetComponent<HomingMissile>();
            if (homing != null) {
                homing.fromPlayer = isPlayer;
            }
        } else {
            MoveScript move = shotTransform.GetComponent<MoveScript>();
            if (move != null) {
                if (isPlayer) {
                    // 1) Get the mouse position in world space
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    
                    // 2) Calculate direction from the ship’s position to the mouse position
                    Vector2 shootDirection = (mousePos - transform.position).normalized;
                    move.direction = shootDirection;

                    // 3) Adjust rotation so the projectile sprite faces the cursor
                    float shootAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg - 90f;
                    shotTransform.rotation = Quaternion.Euler(0, 0, shootAngle);
                } else {
                    shotTransform.rotation = Quaternion.Euler(0, 0, 90);
                    move.direction = new Vector2(-1, 0);
                }
            }
        }
    }
}
