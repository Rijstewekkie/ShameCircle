using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager SGameManager;
    public static string SPlayerName;
    
    private LevelBeatScript LevelBeat;
    private int currentLevel;

    public static BirdIdentityHolder SelectedBird;
    public static string SelectedBirdName;
    void Start()
    {
        LevelBeat = GetComponent<LevelBeatScript>();
    }

    public static void BirdCaught()
    {
        if (!SelectionScreen.SSelectionActive)
        {
            SelectionScreen.SSelectionActive = true;
            Debug.Log("SelectedBird = " + SelectedBird);
        }
        if (SelectedBird.Imposter)
        {
            
        }
        else
        {
            Scoremanager.WrongGuesses++;
        }
    }
}