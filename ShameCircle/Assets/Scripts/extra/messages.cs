using UnityEngine;

public class messages : MonoBehaviour
{
    public GameObject targetObject;
    public float time = 10f;
    public float resettime = 60f;
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
        if (timer >= resettime)
        {
            targetObject.SetActive(false);
            timer = 0;

        }
    }
}
