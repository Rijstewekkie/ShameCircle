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

    public void BirdCaught()
    {
        Debug.Log(SelectedBird);
        if (SelectedBird.Imposter)
        {
            LevelBeat.LevelBeat = true;
        }
        else
        {
            SelectedBird = null;
            SelectedBirdName = null;
            scoremanager.WrongGuesses++;
        }
    }
}