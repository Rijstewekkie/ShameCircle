using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TouchRegister : MonoBehaviour
{
    public Canvas TouchMap;
    
    public Touch TouchInput1; //Click / vinger 1
    public Touch TouchInput2; //Click / vinger 2

    public Vector2 TouchLocation1;
    public Vector2 TouchLocation2;
    
    [SerializeField] private float TouchSize; //voor een visual indicator als we die gaan toevoegen
    
    [SerializeField] private bool DEBUGPCMODE; //voor PC testing
    
    public bool ReleaseActionActive;
    
    
    void Start()
    {
        TouchMap = GetComponent<Canvas>();
        
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!DEBUGPCMODE) //Voor touchscreen
        {
            pTouchLFinder();
            pTouchLocator();   
        }
        if (DEBUGPCMODE) //Voor PC
        {
            PCTouchLocator();
        }
    }

    protected void pTouchLFinder()
    {
        if (Input.touchCount == 0) //Reset de values als ze niet gebruikt worden
        {
            TouchInput1 = default;
            TouchInput2 = default;
        }
        else if (Input.touchCount == 1)
        {
            TouchInput1 = Input.GetTouch(0);
            TouchInput2 = default;
        }
        else if (Input.touchCount > 1) //Zet TouchInput tot de juiste vingers
        {
            TouchInput1 = Input.GetTouch(0);
            TouchInput2 = Input.GetTouch(1);
        }
    }

    protected void pTouchLocator() //Zelfde als hier boven maar dan location
    {
        if (Input.touchCount == 0)
        {
            TouchLocation1 = Vector2.zero;
            TouchLocation2 = Vector2.zero;
        }
        else if (Input.touchCount == 1)
        {
            if (TouchLocation1 != Vector2.zero && TouchLocation2 != Vector2.zero)
            {
                ReleaseActionActive = true;
            }
            else
            {
                ReleaseActionActive = false;
            }
            TouchLocation1 = TouchInput1.position;
            TouchLocation2 = Vector2.zero;
            
            Debug.Log(TouchLocation1);
        }
        else if (Input.touchCount > 1)
        {
            TouchLocation1 = TouchInput1.position;
            TouchLocation2 = TouchInput2.position;
            
            Debug.Log(TouchLocation1 + "&" + TouchLocation2); //kleine Debug
        }
    }

    private void PCTouchLocator()
    {
        if (Input.GetMouseButtonDown(0)) //Doet wat het hier boven deed, maar dan voor PC (met lingering clicks)
        {
            if (TouchLocation1 == Vector2.zero)
            {
                TouchLocation1 = Mouse.current.position.ReadValue();
                Debug.Log(TouchLocation1);
            }
            else if (TouchLocation2 == Vector2.zero)
            {
                ReleaseActionActive = true;
                TouchLocation2 = Mouse.current.position.ReadValue();
                Debug.Log(TouchLocation1 + "&" + TouchLocation2);
            }
            else
            {
                TouchLocation1 = Vector2.zero;
                TouchLocation2 = Vector2.zero;
                Debug.Log("Reset");
                ReleaseActionActive = false;
            }
        }
    }
}