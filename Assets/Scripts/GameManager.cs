using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    
    [HideInInspector]
    public int selectedShipIndex = 0;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("Menu");
        } else {
            Destroy(gameObject);
        }
    }
}