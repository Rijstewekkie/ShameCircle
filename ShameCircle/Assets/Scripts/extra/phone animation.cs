using UnityEngine;
public class phoneanimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    private void Awake()
    {

        animator = GetComponent<Animator>();
    }
    public void Triggeranimation()
    {
       animator.SetTrigger("phonedown");
     }
     
    
    
}

