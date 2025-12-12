using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    private static float loadTimer = 3;

    void Update()
    {
        loadTimer -= Time.deltaTime;
    }
    
    public static void NextScene()
    {
        if (loadTimer <= 0 && SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public static void ReloadScene()
    {
        if (loadTimer <= 0 && SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
