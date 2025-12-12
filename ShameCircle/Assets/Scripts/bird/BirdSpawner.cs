using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] birdPrefabArray;

    [SerializeField] private float birdSpawnRate;
    
    [SerializeField] private float birdSizeDropoff;
    
    [SerializeField] private float birdMaxSize;

    [SerializeField] private float birdSpeedDropoff;

    [SerializeField] private float birdMaxSpeed;
    
    [SerializeField] private float floorIncrease;

    [SerializeField] private float maxSpawnHeight;
    [SerializeField] private float minFlyHeight;
    
    [SerializeField] private float floorHeight;

    [SerializeField] private float birdSpawnDistance;

    private Vector3 birdSpawnSize;
    private float activeSizeDropoff;

    private float birdHeight;

    private float birdLocation;
    
    private float birdSpawnTimer = 3;
    private float activeSpawnTimer;

    private GameObject activeBird;
    private BirdIdentityHolder activeBirdScript;
    private Birdmovement activeBirdMovementScript;

    void Start()
    {
        if (birdSpawnRate == 0)
        {
            birdSpawnRate = 1;
        }

        if (birdSpawnDistance == 0)
        {
            birdSpawnDistance = 10;
        }

        if (birdSizeDropoff == 0)
        {
            birdSizeDropoff = 0f;
        }

        if (birdSpeedDropoff == 0)
        {
            birdSpeedDropoff = .1f;
        }

        if (floorIncrease == 0)
        {
            floorIncrease = .1f;
        }
    }

    void FixedUpdate()
    {
        //kleine timer
        if (!GameSpeedManager.sPauzeGame)
        {
            activeSpawnTimer -= Time.deltaTime * (birdSpawnRate / GameSpeedManager.SGameSpeed);
        }

        if (activeSpawnTimer <= 0)
        {
            spawnBird();
            activeSpawnTimer = birdSpawnTimer;
        }
    }

    void spawnBird()
    {
        birdLocation = Random.Range(1, 5); //Kies de locatie voor de vogel
        
        activeBird = Instantiate(birdPrefabArray[Random.Range(0, birdPrefabArray.Length)]); //Spawn een random bird
        activeBirdScript = activeBird.GetComponent<BirdIdentityHolder>(); //pak het script
        activeBirdMovementScript = activeBird.GetComponent<Birdmovement>(); //deze ook
        
        birdSpawnSize = activeBird.transform.localScale;
        activeSizeDropoff = birdSizeDropoff * birdLocation; //pak de scale enz
        
        int ffChance = Random.Range(0, 2);
        if (ffChance <= .5)
        {
            activeBirdMovementScript.birdDirection = false;
        }
        else if (ffChance > .5)
        {
            activeBirdMovementScript.birdDirection = true;
        }

        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            if (!activeBirdMovementScript.birdDirection) //zet de vogel links/rechts neer
            {
                activeBird.transform.position = new Vector3(birdSpawnDistance, activeBird.transform.position.y,
                    activeBird.transform.position.z);
                activeBird.transform.rotation = Quaternion.Euler(0, 180, 0);

            }
            else if (activeBirdMovementScript.birdDirection)
            {
                activeBird.transform.position = new Vector3(-birdSpawnDistance, activeBird.transform.position.y,
                    activeBird.transform.position.z);
                activeBird.transform.rotation = Quaternion.Euler(0, 0, 0);

            }


            activeBird.layer = LayerMask.NameToLayer("Bird Layer " + birdLocation); //zet de goede layer
            foreach (Transform child in activeBird.transform.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer =
                    LayerMask.NameToLayer("Bird Layer " + birdLocation); //zet de layer ook in children
            }

            activeBird.transform.localScale = new Vector3(birdSpawnSize.x - activeSizeDropoff,
                birdSpawnSize.y - activeSizeDropoff, birdSpawnSize.z - activeSizeDropoff);
            //zet de goede scale

            if (activeBirdScript.Flying)
            {
                activeBird.transform.position = new Vector3(activeBird.transform.position.x,
                    Random.Range(minFlyHeight, maxSpawnHeight) + floorIncrease * (birdLocation + 1),
                    activeBird.transform.position.z);
                //laat vogel vliegen
            }
            else if (!activeBirdScript.Flying)
            {
                activeBird.transform.position = new Vector3(activeBird.transform.position.x,
                    floorHeight, activeBird.transform.position.z);
                //zet vogel op de grond
            }

            activeBirdMovementScript.Movespeed = birdMaxSpeed - (birdSpeedDropoff - birdLocation);
            activeBirdMovementScript.Movespeed -= (birdSizeDropoff * birdLocation);

            activeBirdMovementScript.spawnPos = activeBird.transform.position;

            activeBird.transform.position = new Vector3(activeBird.transform.position.x,
                activeBird.transform.position.y, -20 - 5 * birdLocation);
        }
        else
        {
            // --- NEW Z AXIS LOGIC ---
            if (!activeBirdMovementScript.birdDirection)
            {
                // Spawn positive Z
                activeBird.transform.position = new Vector3(
                    activeBird.transform.position.x,
                    activeBird.transform.position.y,
                    birdSpawnDistance
                );
                activeBird.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                // Spawn negative Z
                activeBird.transform.position = new Vector3(
                    activeBird.transform.position.x,
                    activeBird.transform.position.y,
                    -birdSpawnDistance
                );
                activeBird.transform.rotation = Quaternion.Euler(0, -90, 0);
            }

            activeBird.layer = LayerMask.NameToLayer("Bird Layer " + birdLocation);
            foreach (Transform child in activeBird.transform.GetComponentsInChildren<Transform>())
                child.gameObject.layer = LayerMask.NameToLayer("Bird Layer " + birdLocation);

            activeBird.transform.localScale = new Vector3(
                birdSpawnSize.x - activeSizeDropoff,
                birdSpawnSize.y - activeSizeDropoff,
                birdSpawnSize.z - activeSizeDropoff
            );

            if (activeBirdScript.Flying)
            {
                activeBird.transform.position = new Vector3(
                    activeBird.transform.position.x,
                    Random.Range(minFlyHeight, maxSpawnHeight) + floorIncrease * (birdLocation + 1),
                    activeBird.transform.position.z
                );
            }
            else
            {
                activeBird.transform.position = new Vector3(
                    activeBird.transform.position.x,
                    floorHeight,
                    activeBird.transform.position.z
                );
            }

            activeBirdMovementScript.Movespeed = birdMaxSpeed - (birdSpeedDropoff - birdLocation);
            activeBirdMovementScript.Movespeed -= (birdSizeDropoff * birdLocation);

            activeBirdMovementScript.spawnPos = activeBird.transform.position;

            // Put bird somewhere on X depending on depth
            activeBird.transform.position = new Vector3(
                -20 - 5 * birdLocation,
                activeBird.transform.position.y,
                activeBird.transform.position.z
            );
        }   
    }
}
