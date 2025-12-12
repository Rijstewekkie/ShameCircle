using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager SUIManager;
    
    public bool PauzeActive;
    void Start()
    {
        if (SUIManager != this)
        {
            Destroy(this);
        }        
        else
        {
            SUIManager = this;
        }
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
