using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdIdentityHolder : MonoBehaviour
{
    public enum BirdTypes
    {
        NULL,
        Duck,
        Pidgeon,
        Emu,
        SecretaryBird,
        SnowyOwl,
        GreatSpottedWoodpecker,
        EurasianRobin,
        Cockatoo,
        RockPtarmigan
    }
    
    [SerializeField] public BirdTypes BirdType;

    [SerializeField] public bool Flying;
    
    [SerializeField] public bool Imposter;

    private GameObject IgnoreAsset;

    public int RandomChance;

    private int[] scene1Birds = { 1, 2, 7 };
    private int[] scene2Birds = { 3, 4, 8 };
    private int[] scene3Birds = { 6, 9, 5 };
    
    void Start()
    {
        if (BirdType == BirdTypes.NULL)
        {
            Debug.LogError("BirdType is NULL, Deleting object");
            Destroy(this.gameObject);
        }

        if (BirdType == BirdTypes.EurasianRobin)
        { 
            RandomChance = Random.Range(0, 100);
            if (RandomChance == 69)
            {
                IgnoreAsset = Resources.Load($"IgnoreThis") as GameObject;

                if (IgnoreAsset != null)
                {
                    GameObject asset = Instantiate(IgnoreAsset, transform.position, transform.rotation, transform);
                    
                    asset.layer = gameObject.layer;
                    foreach (Transform child in asset.transform.GetComponentsInChildren<Transform>())
                    {
                        child.gameObject.layer = gameObject.layer;
                    }
                }
                else
                {
                    Debug.Log("easteregg popped");
                }   
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 1 && !scene1Birds.Contains((int)BirdType))
        {
            Imposter = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2 && !scene2Birds.Contains((int)BirdType))
        {
            Imposter = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3 && !scene3Birds.Contains((int)BirdType))
        {
            Imposter = true;
        }
    }
}