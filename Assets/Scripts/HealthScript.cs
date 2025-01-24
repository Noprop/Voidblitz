using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int hp = 1;
    public bool isEnemy = true;


    public void Damage(int damageCount){
        hp -= damageCount;
        if (hp <= 0) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        ShotScript shot = other.gameObject.GetComponent<ShotScript>();
        if (shot != null) {
            // prevent friendly fire
            if (shot.forEnemy && isEnemy) {
                Damage(shot.damage);
                Destroy(shot.gameObject);
            }
        }
    }
}
