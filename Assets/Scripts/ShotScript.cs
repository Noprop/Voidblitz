using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public int damage = 1;
    public bool forEnemy = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
