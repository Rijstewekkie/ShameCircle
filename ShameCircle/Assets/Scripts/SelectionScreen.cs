using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    public static bool SSelectionActive;
    
    private bool selectionActiveDelay;
    
    [SerializeField] GameObject selectionScreen;
    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject TouchRegister;
    private TouchRegister actualTouchRegister;

    [SerializeField] TMP_Text basicInfo;
    [SerializeField] TMP_Text extraInfo;
    
    [SerializeField] string[] basicInfoArr;
    [SerializeField] string[] extraInfoArr;

    [SerializeField] GameObject[] Models;
    private GameObject model;
    
    [SerializeField] GameObject modelCube;
    
    [SerializeField] GameObject juist;
    [SerializeField] GameObject fout;

    private int totalTimer = 3;
    private float activeTimer;
    void Update()
    {
        if (SSelectionActive)
        {
            activeTimer -= Time.deltaTime;
            if (!selectionActiveDelay)
            {
                activeTimer = totalTimer; 
                runInfo();
                actualTouchRegister = TouchRegister.GetComponent<TouchRegister>();
                actualTouchRegister.TouchLocation1 = Vector2.zero;
                actualTouchRegister.TouchLocation2 = Vector2.zero;
                actualTouchRegister.TouchInput1 = default;
                actualTouchRegister.TouchInput2 = default;
            }
            selectionScreen.SetActive(true);
            MainCamera.SetActive(false);
            UI.gameObject.SetActive(false);

            if (GameManager.SelectedBird.Imposter)
            {
                juist.SetActive(true);
                fout.SetActive(false);
            }
            else if (!GameManager.SelectedBird.Imposter)
            {
                juist.SetActive(false);
                fout.SetActive(true);
            }
            else
            {
                juist.SetActive(false);
                fout.SetActive(false);
            }
            
            GameSpeedManager.sPauzeGame = true;
            
            selectionActiveDelay = true;

            if (actualTouchRegister.TouchLocation1 != Vector2.zero && activeTimer <= 0)
            {
                actualTouchRegister.TouchLocation1 = Vector2.zero;
                Debug.Log("Run close");
                CloseMenu();
            }
            else if (actualTouchRegister.TouchLocation1 != Vector2.zero && activeTimer >= 0)
            {
                actualTouchRegister.TouchLocation1 = Vector2.zero;
            }
        }
        else if (!SSelectionActive)
        {
            selectionScreen.SetActive(false);
            MainCamera.SetActive(true);
            UI.SetActive(true);

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
        
        extraInfo.text = extraInfoArr[(int)GameManager.SelectedBird.BirdType - 1];

        model = Instantiate(Models[(int)GameManager.SelectedBird.BirdType - 1] as GameObject, modelCube.transform);
        model.GetComponent<BirdIdentityHolder>().RandomChance = GameManager.SelectedBird.RandomChance;
    }

    void CloseMenu()
    {
        if (GameManager.SelectedBird.Imposter)
        {
            if (GameManager.ImposerCaught == null)
            {
                GameManager.ImposerCaught = GameManager.SelectedBird.BirdType.ToString();
                Debug.Log(GameManager.ImposerCaught);
            }
            else if (GameManager.ImposerCaught != GameManager.SelectedBird.BirdType.ToString())
            {
                Debug.Log("Second Bird Found");
                GameManager.ImposerCaught = null;
                LevelBeatScript.LevelBeat = true;
            }
        }
        else
        {
            Scoremanager.WrongGuesses++;
        }
        
        GameSpeedManager.sPauzeGame = false;
        actualTouchRegister.TouchLocation1 = Vector2.zero;
        SSelectionActive = false;
        
        if(LevelBeatScript.LevelBeat)
        {
            Scenemanager.NextScene();
        }
        Scenemanager.ReloadScene();
    }
}
