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
    
    [SerializeField] string[] basicInfoArr;
    [SerializeField] string[] extraInfoArr;

    [SerializeField] GameObject[] Models;
    private GameObject model;
    
    [SerializeField] GameObject modelCube;
    
    void Update()
    {
        if (SSelectionActive)
        {
            if (!selectionActiveDelay)
            {
                runInfo();
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
                Destroy(model);
                model = null;
            }
            
            selectionActiveDelay = false;
        }
    }

    void runInfo()
    {
        basicInfo.text = basicInfoArr[(int)GameManager.SelectedBird.BirdType - 1];
        ;
        extraInfo.text = extraInfoArr[(int)GameManager.SelectedBird.BirdType - 1];

        model = Instantiate(Models[(int)GameManager.SelectedBird.BirdType - 1] as GameObject, modelCube.transform);
        model.GetComponent<BirdIdentityHolder>().RandomChance = GameManager.SelectedBird.RandomChance;
    }
}
