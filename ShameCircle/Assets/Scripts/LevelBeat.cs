using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBeatScript : MonoBehaviour
{
    private GameManager gameManager;
    
    public bool LevelBeat;
    public bool Dead;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    
    void Update()
    {
        if (LevelBeat)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            gameManager.Health = 3;
        }
        
        if (Dead)
        {
            if (SceneManager.GetSceneByName("Dead").IsValid())
            {
                SceneManager.LoadScene("Dead");
            }
            else
            {
                Debug.LogError("Death Scene not found, terminating process");
                Application.Quit();
                gameManager.Health = 3;
                Debug.LogError("Game loaded in editor, termination invalid");
            }
        }
    }
}
