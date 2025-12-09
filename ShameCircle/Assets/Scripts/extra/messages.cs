using UnityEngine;

public class messages : MonoBehaviour
{
    public GameObject targetObject;
    public float time = 10f;   // tijd in seconden

    private float timer;

    private void Awake()
    {
        targetObject.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= time)
        {
            targetObject.SetActive(true);
           
        }
    }
}
