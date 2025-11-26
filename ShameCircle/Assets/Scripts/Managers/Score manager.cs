using UnityEngine;

public class Scoremanager : MonoBehaviour
{
    public static Scoremanager SScoreManager;
    
    public static int SScore;
    void Start()
    {
        if (SScoreManager != this)
        {
            Destroy(this);
        }        
        else
        {
            SScoreManager = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
