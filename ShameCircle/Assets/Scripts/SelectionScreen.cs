using UnityEngine;

public class SelectionScreen : MonoBehaviour
{
    public static bool SSelectionActive;
    
    private bool selectionActiveDelay;
    
    [SerializeField] GameObject selectionScreen;
    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject TouchRegister;
    private TouchRegister actualTouchRegister;
    
    void Update()
    {
        if (SSelectionActive)
        {
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
                Debug.Log("Test1 true = " + SSelectionActive);
                actualTouchRegister.TouchLocation1 = Vector2.zero;
                SSelectionActive = false;
                Debug.Log("Test2 false = " + SSelectionActive);
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
}
