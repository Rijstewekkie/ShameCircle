using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] birdPrefabArray;
    
    [SerializeField] private float birdSpawnRate;
    [SerializeField] private float birdSizeDropoff;
    
    private float birdHeight;
    
    private float birdLocation;
    private enum BirdDirection {left, right}
    private BirdDirection birdDirection;

    private float birdSpawnTimer;
    private float activeSpawnTimer;

    private GameObject activeBird;
    
    [SerializeField] private float birdSpawnDistance;

    void FixedUpdate()
    {
        if (birdSpawnTimer == 0)
        {
            birdSpawnTimer = 1;
        }

        if (birdSpawnDistance == 0)
        {
            birdSpawnDistance = 10;
        }
        
        activeSpawnTimer -= Time.deltaTime * birdSpawnRate;
        if (activeSpawnTimer <= 0)
        {
            spawnBird();
            activeSpawnTimer = birdSpawnTimer;
        }
    }

    void spawnBird()
    {
        birdLocation = Random.Range(0, 5);
        
        if (birdDirection == BirdDirection.left)
        {
            birdDirection = BirdDirection.right;
        }
        else
        {
            birdDirection = BirdDirection.left;
        }
        
        activeBird = Instantiate(birdPrefabArray[Random.Range(0, birdPrefabArray.Length)]);

        if (birdDirection == BirdDirection.right)
        {
            activeBird.transform.position = new Vector3(birdSpawnDistance, activeBird.transform.position.y, activeBird.transform.position.z);
        }
        else if (birdDirection == BirdDirection.left)
        {
            activeBird.transform.position = new Vector3(-birdSpawnDistance, activeBird.transform.position.y, activeBird.transform.position.z);
        }
        
        activeBird.transform.LookAt(new Vector3(0, activeBird.transform.position.y, activeBird.transform.position.z));
    }
}
