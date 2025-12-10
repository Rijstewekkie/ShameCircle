using System;
using UnityEngine;

public class SelectionBirds : MonoBehaviour
{
    private BirdIdentityHolder thisBird;
    private GameObject activeBird;
    
    private void Update()
    {
        if (GameManager.SelectedBird != null)
        {
            thisBird = GameManager.SelectedBird;
        }

        if (activeBird == null || activeBird.GetComponent<BirdIdentityHolder>().BirdType != thisBird.BirdType || activeBird.GetComponent<BirdIdentityHolder>().RandomChance != thisBird.RandomChance)
        {
            Destroy(activeBird);
            activeBird = Instantiate(Resources.Load($"Unscripted Animated Birds/UA Robin Prefab") as GameObject, transform);
            activeBird.GetComponent<BirdIdentityHolder>().RandomChance = GameManager.SelectedBird.RandomChance;
        }
    }
}
