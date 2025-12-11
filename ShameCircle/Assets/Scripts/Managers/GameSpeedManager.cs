using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    public static float SGameSpeed; //actuale speed
    public static float SSpeedToApply = 1; //speed storage
    public static bool sPauzeGame;
    public static bool sLevelBeat;
    
    private UIManager uiManager;
    
    public static GameSpeedManager SSpeedManager;
    void Start()
    {
        uiManager = gameObject.GetComponent<UIManager>();
        
        if (SSpeedManager != null && SSpeedManager != this)

        {
            Destroy(this);
        }        
        else
        {
            SSpeedManager = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (sLevelBeat && sPauzeGame)
        {
            sPauzeGame = false;
        }

        if (sPauzeGame)
        {
            SGameSpeed = 0;
        }

        else if (sLevelBeat)
        {
            SGameSpeed = 0.25f;
        }

        else
        {
            SGameSpeed = SSpeedToApply;
        }

    }

    public void ApplicationPause()
    {
        sPauzeGame = !sPauzeGame;
    }
}
