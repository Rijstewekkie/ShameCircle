using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndDelete : MonoBehaviour
{
    public float FadeDuration = 0.01f;

    IEnumerator FadeDelete()
    {
        RawImage img = GetComponent<RawImage>();
        Color originalColor = img.color;
        for (int i = 0; i < 100; i++)
        {
            originalColor.a -= 0.01f;
            img.color = originalColor;
            yield return new WaitForSeconds(FadeDuration);
            
        }
        if(img.color.a <= 0)
        {
            Destroy(GameObject.FindWithTag("BirdCube"));
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartCoroutine(FadeDelete());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
