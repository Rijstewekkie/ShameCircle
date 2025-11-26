using UnityEngine;

public class BirdCheckBox : MonoBehaviour
{
    private string BirdName;
    private BirdIdentityHolder bird;
    
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("ManagerManager").GetComponent<GameManager>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BirdIdentityHolder>() != null)
        {
            bird = other.GetComponent<BirdIdentityHolder>();
            BirdName = bird.BirdType.ToString();
            BirdSelected();
        }
    }

    void BirdSelected()
    {
        gameManager.SelectedBirdName = BirdName;
        gameManager.SelectedBird = bird;
        gameManager.BirdCaught();
    }
}
