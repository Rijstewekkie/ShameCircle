using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager SGameManager;
    public static string SPlayerName;
    
    private Scoremanager scoremanager;
    private LevelBeatScript LevelBeat;
    private int currentLevel;

    public BirdIdentityHolder SelectedBird;
    public string SelectedBirdName;
    void Start()
    {
        scoremanager = GetComponent<Scoremanager>();
        LevelBeat = GetComponent<LevelBeatScript>();
    }

    private void Update()
    {
        if (SelectedBird != null)
        {
            SelectionScreen.SSelectionActive = true;
        }
        else
        {
            SelectionScreen.SSelectionActive = false;
        }
    }

    public void BirdCaught()
    {
        SelectionScreen.SSelectionActive = true;
        Debug.Log("SelectedBird = " + SelectedBird);
        if (SelectedBird.Imposter)
        {
            
        }
        else
        {
            scoremanager.WrongGuesses++;
        }
    }
}