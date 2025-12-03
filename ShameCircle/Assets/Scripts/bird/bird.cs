using UnityEngine;

public class Birdmovement : MonoBehaviour
{
    public float Movespeed = 15f;            
    public float Maxturnspeed = 2f;         
    public float Glidechance = 0.2f;        
         
    public float Horizontal = 0.5f;    
    public float Coursechangeinterval = 3f; 

    private Vector3 targetPos;
    private float nextCourseChange;
    private bool gliding = false;

    [SerializeField] private bool respawn;
    
    private BirdIdentityHolder identityHolder;
    
    public bool birdDirection;
    
    public Vector3 spawnPos;

    void Start()
    {
        // eerste doelpunt iets voor de vogel
        targetPos = transform.position + new Vector3(5, 0, 0);
        nextCourseChange = Time.time + Random.Range(1f, Coursechangeinterval);
        identityHolder = GetComponent<BirdIdentityHolder>();
    }

    void FixedUpdate()
    {
        UpdateTarget();
        MoveTowardsTarget();
        CheckRespawn();
    }

    void UpdateTarget()
    {
        if (identityHolder.Flying)
        {
            if (birdDirection)
            {
                // om de zoveel tijd koersverandering
                if (Time.time > nextCourseChange)
                {
                    nextCourseChange = Time.time + Random.Range(1f, Coursechangeinterval);
                    gliding = Random.value < Glidechance;

                    float randomY = Random.Range(-8, 8);
                    float randomX = Random.Range(0f, Horizontal);

                    targetPos -= new Vector3(5f + +randomX, randomY, 0f);
                }
            }
            else if (!birdDirection)
            {
                // om de zoveel tijd koersverandering
                if (Time.time > nextCourseChange)
                {
                    nextCourseChange = Time.time + Random.Range(1f, Coursechangeinterval);
                    gliding = Random.value < Glidechance;

                    float randomY = Random.Range(-8, 8);
                    float randomX = Random.Range(0f, Horizontal);

                    targetPos += new Vector3(5f + randomX, randomY, 0f);
                }
            }
        }
        else if (!identityHolder.Flying)
        {
            targetPos = new Vector3(-spawnPos.x, spawnPos.y, spawnPos.z);
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 dir = (targetPos - transform.position).normalized;

        float turnFactor = gliding ? 0.2f : 1f;

        // vloeiend draaien naar richting
        Vector3 smoothDir = Vector3.Lerp(transform.right, dir, Time.deltaTime * Maxturnspeed * turnFactor).normalized;
        transform.right = smoothDir;

        float speed = gliding ? Movespeed * 1.3f : Movespeed;
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void CheckRespawn()
    {
        if (birdDirection)
        {
            if (transform.position.x !< -spawnPos.x)
            {
                return;
            }
        }
        else if (!birdDirection)
        {
            if (transform.position.x !> -spawnPos.x)
            {
                return;
            }
        }
        else
        {
            return;
        }
        
        if (respawn)
        {
            transform.position = new Vector3(-55f, Random.Range(-12,15), 26);
            targetPos = transform.position + new Vector3(5, 0, 0);
            // teleport terug links als te ver rechts
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
