using UnityEngine;

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

    [SerializeField] public bool Imposter;
    void Start()
    {
        if (BirdType == BirdTypes.NULL)
        {
            Debug.LogError("BirdType is NULL, Deleting object");
            Destroy(this.gameObject);
        }
    }
}