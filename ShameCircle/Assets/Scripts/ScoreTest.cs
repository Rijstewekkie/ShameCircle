using UnityEngine;

public class ScoreTest : MonoBehaviour , IAddScore
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void IAddScore.AddScore(int AddedScore)
    {
        Scoremanager.SScore += AddedScore;
    }
}
