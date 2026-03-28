using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // lets any script access GameManager

    private Database database;
    private CatchSystem catchSystem;

    void Awake()
    {
        // Awake runs before Start — good for setup
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);  // persists across scenes
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        database = new Database();
        catchSystem = new CatchSystem();
        Debug.Log("Game initialized.");
    }
}