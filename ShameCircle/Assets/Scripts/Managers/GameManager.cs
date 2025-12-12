using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager SGameManager;
    public static string SPlayerName;
    
    private LevelBeatScript LevelBeat;
    private int currentLevel;

    public static string ImposerCaught;

    public static BirdIdentityHolder SelectedBird;
    public static string SelectedBirdName;

    public static bool Reset;
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
    }

    void Update()
    {
        if (Reset)
        {
            Debug.Log("Reset");
            Reset = false;
            Scoremanager.WrongGuesses = 0;
            currentLevel = 0;
            ImposerCaught = null;
            SelectedBird = null;
            Start();
        }
    }
}