using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdCheckBox : MonoBehaviour
{
    private string BirdName;
    private BirdIdentityHolder bird;
    
    private GameManager gameManager;

    public DrawBoxController Parent;
    
    [SerializeField] bool releaseAction;

    private List<GameObject> birds = new List<GameObject>();

    private BirdIdentityHolder otherScript;
    
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

        if (birds.Count > 0 && releaseAction)
        {
            SelectClosestBird();
        }
        else if (!releaseAction && SelectedBird != null)
        {
            ClearSelection();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (!releaseAction) return;
        
        otherScript = other.GetComponent<BirdIdentityHolder>();
        Debug.Log(releaseAction);
        if (otherScript != null && !birds.Contains(other.gameObject))
        {
            birds.Add(other.gameObject);
            Debug.Log(birds.Count);
        }
    }

    void SelectClosestBird()
    {
        float closest = float.MaxValue;

        foreach (GameObject b in birds)
        {
            Vector3 pos = new Vector3(b.transform.position.x, b.transform.position.y, 0);
            float dist = Vector3.Distance(thisPosition, pos);

            if (dist < closest)
            {
                closest = dist;
                SelectedBird = b;
                selectedPosition = pos;
            }
        }

        bird = SelectedBird.GetComponent<BirdIdentityHolder>();
        BirdName = bird.BirdType.ToString();
        BirdSelected();
    }

    void ClearSelection()
    {
        birds.Clear();
        SelectedBird = null;
        selectedPosition = Vector3.zero;
    }

    void BirdSelected()
    {
        Debug.Log("BirdSelected");
        gameManager.SelectedBirdName = BirdName;
        gameManager.SelectedBird = bird;
        gameManager.BirdCaught();
        releaseAction = false;
        Parent.ReleaseActionActive = false;
    }
}
