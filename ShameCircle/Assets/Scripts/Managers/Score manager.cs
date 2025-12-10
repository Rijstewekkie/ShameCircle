using UnityEngine;

public class Scoremanager : MonoBehaviour
{
    public static Scoremanager SScoreInstance;
    
    public static int SScore;

    public static int WrongGuesses;
    void Start()
    {
        if (SScoreInstance == null)
        {
            SScoreInstance = this;
        }        
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
