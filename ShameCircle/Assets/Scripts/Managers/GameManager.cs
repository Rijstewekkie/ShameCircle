using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager SGameManager;
    public static string SPlayerName;

    public int Health;

    private Scoremanager scoremanager;
    private LevelBeatScript LevelBeat;

    public BirdIdentityHolder SelectedBird;
    public string SelectedBirdName;
    
    void Start()
    {
        scoremanager = GetComponent<Scoremanager>();
        LevelBeat = GetComponent<LevelBeatScript>();
    }

    void Update()
    {
        if (Health < 1)
        {
            LevelBeat.Dead = true;
        }
        else if (LevelBeat.Dead == true && Health > 0)
        {
            LevelBeat.Dead = false;
            Debug.Log(LevelBeat.Dead);
        }
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
            Health--;
        }
    }
}
