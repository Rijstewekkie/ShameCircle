using UnityEngine;

public class BirdCheckBox : MonoBehaviour
{
    private string BirdName;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BirdIdentityHolder>() != null)
        {
            BirdIdentityHolder BirdIdentity = other.GetComponent<BirdIdentityHolder>();
            BirdName = BirdIdentity.BirdType.ToString();
            BirdSelected();
        }
    }

    void BirdSelected()
    {
        Debug.Log(BirdName);
        
    }
}
