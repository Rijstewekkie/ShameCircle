using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBeatScript : MonoBehaviour
{
    private GameManager gameManager;
    
    public bool LevelBeat;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    
    void Update()
    {
        if (LevelBeat)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
