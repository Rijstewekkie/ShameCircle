using UnityEngine;

public class PauzeManager : MonoBehaviour
{
    public static PauzeManager SPauseManager;
    
    public bool PauzeActive;
    void Start()
    {
        if (SPauseManager != this)
        {
            Destroy(this);
        }        
        else
        {
            SPauseManager = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (PauzeActive)
        {
            runPauze();
        }
    }

    void runPauze()
    {
        
    }
}
