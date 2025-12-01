using UnityEngine;

public class Birdmovement : MonoBehaviour
{
    
    public float Movespeed = 4f;            
    public float Maxturnspeed = 2f;         
    public float Glidechance = 0.2f;        
         
    public float Horizontal = 0.5f;    
    public float Coursechangeinterval = 3f; 

    private Vector3 targetPos;
    private float nextCourseChange;
    private bool gliding = false;

    void Start()
    {
        // eerste doelpunt iets voor de vogel
        targetPos = transform.position + new Vector3(5, 0, 0);
        nextCourseChange = Time.time + Random.Range(1f, Coursechangeinterval);
    }

    void Update()
    {
        UpdateTarget();
        MoveTowardsTarget();
        CheckRespawn();
    }

    void UpdateTarget()
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

        // target schuift langzaam mee naar rechts
        targetPos += Vector3.right * Time.deltaTime * Movespeed * 0.5f;
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
        // teleport terug links als te ver rechts
        if (transform.position.x > 55f)
        {
            transform.position = new Vector3(-55f, Random.Range(-12,15), 26);
            targetPos = transform.position + new Vector3(5, 0, 0);
        }
    }
}
