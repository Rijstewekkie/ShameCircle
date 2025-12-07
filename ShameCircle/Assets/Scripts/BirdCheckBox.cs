using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdCheckBox : MonoBehaviour
{
    private string BirdName;
    private BirdIdentityHolder bird;
    
    private GameManager gameManager;

    public DrawBoxController Parent;
    
    private bool releaseAction;

    private List<GameObject> birds = new List<GameObject>();
    
    public GameObject SelectedBird;
    
    private Vector3 thisPosition;
    private Vector3 otherPosition;
    private Vector3 selectedPosition;
    
    void Awake()
    {
        gameManager = GameObject.Find("ManagerManager").GetComponent<GameManager>();
    }

    void Update()
    {
        releaseAction = Parent.ReleaseActionActive;
        thisPosition = new Vector3(transform.position.x, transform.position.y, 0);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (releaseAction)
        {
            if (other.GetComponent<BirdIdentityHolder>() != null)
            {
                birds.Add(other.gameObject);
                foreach (GameObject bird in birds)
                {
                    otherPosition = new Vector3(other.transform.position.x, other.transform.position.y, 0);
                    if (Vector3.Distance(thisPosition, otherPosition) < Vector3.Distance(thisPosition, selectedPosition))
                    {
                        SelectedBird = other.gameObject;
                        selectedPosition = new Vector3(SelectedBird.transform.position.x, SelectedBird.transform.position.y, 0);
                    }
                    if (selectedPosition == Vector3.zero)
                    {
                        SelectedBird = other.gameObject;
                        selectedPosition = new Vector3(SelectedBird.transform.position.x, SelectedBird.transform.position.y, 0);
                    }
                }
                bird = SelectedBird.GetComponent<BirdIdentityHolder>();
                BirdName = bird.BirdType.ToString();
                BirdSelected();
            }
        }
        else
        {
            birds.Clear();
            SelectedBird = null;
            selectedPosition = Vector3.zero;
        }
    }

    void BirdSelected()
    {
        gameManager.SelectedBirdName = BirdName;
        gameManager.SelectedBird = bird;
        gameManager.BirdCaught();
        releaseAction = false;
        Parent.ReleaseActionActive = false;
    }
}
