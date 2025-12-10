using TMPro;
using UnityEngine;

public class SelectionScreen : MonoBehaviour
{
    public static bool SSelectionActive;
    
    private bool selectionActiveDelay;
    
    [SerializeField] GameObject selectionScreen;
    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject TouchRegister;
    private TouchRegister actualTouchRegister;

    [SerializeField] TMP_Text basicInfo;
    [SerializeField] TMP_Text extraInfo;
    
    void Update()
    {
        if (SSelectionActive)
        {
            runInfo();
            if (!selectionActiveDelay)
            {
                actualTouchRegister = TouchRegister.GetComponent<TouchRegister>();
                actualTouchRegister.TouchLocation1 = Vector2.zero;
                actualTouchRegister.TouchLocation2 = Vector2.zero;
                actualTouchRegister.TouchInput1 = default;
                actualTouchRegister.TouchInput2 = default;
            }
            selectionScreen.SetActive(true);
            MainCamera.SetActive(false);
            
            GameSpeedManager.sPauzeGame = true;
            
            selectionActiveDelay = true;

            if (actualTouchRegister.TouchLocation1 != Vector2.zero)
            {
                actualTouchRegister.TouchLocation1 = Vector2.zero;
                SSelectionActive = false;
            }
        }
        else if (!SSelectionActive)
        {
            selectionScreen.SetActive(false);
            MainCamera.SetActive(true);

            if (selectionActiveDelay)
            {
                GameSpeedManager.sPauzeGame = false;
            }
            
            selectionActiveDelay = false;
        }
    }

    void runInfo()
    {
        //if 
    }
}
