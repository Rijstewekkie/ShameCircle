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
            selectionScreen.SetActive(true);
            MainCamera.SetActive(false);
            
            actualTouchRegister = TouchRegister.GetComponent<TouchRegister>();
            actualTouchRegister.TouchLocation1 = Vector2.zero;
            actualTouchRegister.TouchLocation2 = Vector2.zero;
            actualTouchRegister.TouchInput1 = default;
            actualTouchRegister.TouchInput2 = default;

            TouchRegister.SetActive(false);

            GameSpeedManager.sPauzeGame = true;
            
            selectionActiveDelay = true;
        }
        else if (!SSelectionActive)
        {
            selectionScreen.SetActive(false);
            MainCamera.SetActive(true);
            TouchRegister.SetActive(true);

            if (selectionActiveDelay)
            {
                GameSpeedManager.sPauzeGame = true;
            }
            
            selectionActiveDelay = false;
        }
    }

    public void OnClick()
    {
        SSelectionActive = false;
    }
}
