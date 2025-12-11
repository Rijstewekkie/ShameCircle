using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBeatScript : MonoBehaviour
{
    private GameManager gameManager;
    
    public static bool LevelBeat;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    
    void Update()
    {
        if (LevelBeat)
        {
            Debug.Log("Level Beat");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            LevelBeat = false;
            Scenemanager.NextScene();
        }
    }
}
