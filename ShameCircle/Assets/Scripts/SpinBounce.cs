using UnityEngine;

public class SpinBounce : MonoBehaviour
{
    private Vector3 startPos;
    
    [Header("Spin")] [SerializeField] private bool spin;
    [SerializeField] private float spinSpeed;

    [Header("Bounce")] [SerializeField] private bool bounce;
    [SerializeField] private float bounceSpeed;
    private float actualBounceSpeed;
    [SerializeField] private float bounceRange;
    private float activeBounce;
    private bool bounceDirection;

    void Start()
    {
        startPos = transform.position;
        
        if (spin)
        {
            if (spinSpeed == 0)
            {
                spinSpeed = 2.5f;
            }
        }

        if (bounce)
        {
            if (bounceSpeed == 0)
            {
                bounceSpeed = 2.5f;
            }
            if (bounceRange == 0)
            {
                bounceRange = 1;
            }
        }

        if ((spin || bounce) && GetComponent<Collider>() != null)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    void Update()
    {
        if (spin)
        {
            Spin();
        }

        if (bounce)
        {
            Bounce();
        }
    }

    void Spin()
    {
        transform.Rotate(0f, spinSpeed * 10 * Time.deltaTime, 0f);
    }

    void Bounce()
    {
        if (bounceDirection) // up
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + actualBounceSpeed * Time.deltaTime,
                transform.position.z);
        }
        else // down
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - actualBounceSpeed * Time.deltaTime,
                transform.position.z);
        }

        activeBounce = transform.position.y - startPos.y;
       
        actualBounceSpeed = (bounceRange - activeBounce) * bounceSpeed + bounceSpeed / 100;
        
        if (activeBounce > bounceRange)
        {
            bounceDirection = false;
        }

        if (activeBounce < -bounceRange)
        {
            bounceDirection = true;
        }
    }
}
