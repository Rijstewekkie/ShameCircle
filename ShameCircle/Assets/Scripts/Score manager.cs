using UnityEngine;

public class Scoremanager : MonoBehaviour
{
    public static Scoremanager SScoreInstance;
    
    public static int SScore;
    void Start()
    {
        if (SScoreInstance != this)
        {
            Destroy(this);
        }        
        else
        {
            SScoreInstance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
