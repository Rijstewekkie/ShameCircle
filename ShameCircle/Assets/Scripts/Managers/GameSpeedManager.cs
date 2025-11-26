using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    private float GameSpeed; //actuale speed
    public static float SSpeedToApply; //speed storage
    public static bool sPauzeGame;
    
    private PauzeManager pauzeManager;
    
    public static GameSpeedManager SSpeedManager;
    void Start()
    {
        pauzeManager = gameObject.GetComponent<PauzeManager>();
        
        if (SSpeedManager != this)
        {
            Destroy(this);
        }        
        else
        {
            SSpeedManager = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
