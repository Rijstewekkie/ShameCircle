using UnityEngine;

public class DrawBoxController : MonoBehaviour
{
    private TouchRegister Parent;

    [SerializeField] private GameObject drawBox;
    private GameObject activeDrawBox;

    private bool drawBoxInitialized = false;

    //Box min/max coordinates
    private float boxMinX;
    private float boxMaxX;
    private float boxMinY;
    private float boxMaxY;

    //Box size en center
    private float boxWidth;
    private float boxHeight;
    private Vector2 boxCenter;

    //Camera rect params (normalized)
    private float rectX;
    private float rectY;
    private float rectWidth;
    private float rectHeight;

    [SerializeField] private Camera screenshotCamera;
    
    //Collision Cube
    [SerializeField] GameObject CollisionCube;
    private GameObject activeCollisionCube;
    [SerializeField] private float collisionCubeDepth;


    void Start()
    {
        Parent = GetComponent<TouchRegister>();

        if (collisionCubeDepth == 0)
        {
            collisionCubeDepth = 100;
        }
    }
    
    void Update()
    {
        if (Parent == null)
        {
            Debug.LogError("TouchRegister not found");
            return;
        }

        //Maak de diengen als er 2 rouches zijn
        if (!drawBoxInitialized && Parent.TouchLocation1 != Vector2.zero && Parent.TouchLocation2 != Vector2.zero)
        {
            InitializeBox();
            calculateBoxSize();
        }
        //Haal doos weg als de vingers weg zijn
        else if (drawBoxInitialized && (Parent.TouchLocation1 == Vector2.zero || Parent.TouchLocation2 == Vector2.zero))
        {
            UninitializeBox();
        }
        //Doos movement en placement hiero
        else if (drawBoxInitialized)
        {
            calculateBoxSize();
        }
    }

    void InitializeBox() //Spawn de doos en Image
    {
        activeDrawBox = Instantiate(drawBox, Parent.TouchMap.transform);
        activeCollisionCube = Instantiate(CollisionCube);
        drawBoxInitialized = true;
        Debug.Log("Box initialized");
    }

    void UninitializeBox() //De doos is ol' yeller
    {
        Destroy(activeDrawBox);
        Destroy(activeCollisionCube);

        activeDrawBox = null;
        activeCollisionCube = null;

        drawBoxInitialized = false;
        ResetBoxCoords();
        
        Debug.Log("Box Destroyed");
    }

    void calculateBoxSize()
    {
        //Pak de hoekjes
        boxMinX = Mathf.Min(Parent.TouchLocation1.x, Parent.TouchLocation2.x);
        boxMaxX = Mathf.Max(Parent.TouchLocation1.x, Parent.TouchLocation2.x);
        boxMinY = Mathf.Min(Parent.TouchLocation1.y, Parent.TouchLocation2.y);
        boxMaxY = Mathf.Max(Parent.TouchLocation1.y, Parent.TouchLocation2.y);

        //Zet de doos perportions
        boxWidth = boxMaxX - boxMinX;
        boxHeight = boxMaxY - boxMinY;
        boxCenter = new Vector2(boxMinX + boxWidth / 2f, boxMinY + boxHeight / 2f);

        //Laat de doos grooven en beweegbaar zijn
        RectTransform rt = activeDrawBox.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(boxWidth, boxHeight);

        Vector2 uiPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            Parent.TouchMap.transform as RectTransform,
            boxCenter,
            null,
            out uiPosition
        );
        rt.anchoredPosition = uiPosition;

        //Pas perpotions toe
        rectX = boxMinX / Screen.width;
        rectY = boxMinY / Screen.height;
        rectWidth = boxWidth / Screen.width;
        rectHeight = boxHeight / Screen.height;
        screenshotCamera.rect = new Rect(rectX, rectY, rectWidth, rectHeight);
        
        //Mix de canvas en "echte" wereld
        Vector3 screenCenter = new Vector3(boxCenter.x, boxCenter.y, collisionCubeDepth);
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);
        activeCollisionCube.transform.position = worldCenter;

        //Scherm groote bs
        Vector3 screenLeft  = new Vector3(boxMinX, boxCenter.y, collisionCubeDepth);
        Vector3 screenRight = new Vector3(boxMaxX, boxCenter.y, collisionCubeDepth);

        float worldWidth = Vector3.Distance(
            Camera.main.ScreenToWorldPoint(screenLeft),
            Camera.main.ScreenToWorldPoint(screenRight)
        );

        //Meer scherm groote bs
        Vector3 screenBottom = new Vector3(boxCenter.x, boxMinY, collisionCubeDepth);
        Vector3 screenTop    = new Vector3(boxCenter.x, boxMaxY, collisionCubeDepth);

        float worldHeight = Vector3.Distance(
            Camera.main.ScreenToWorldPoint(screenBottom),
            Camera.main.ScreenToWorldPoint(screenTop)
        );

        //Placement (Daan, raak gras aan <3)
        activeCollisionCube.transform.localScale = new Vector3(worldWidth, worldHeight, 500f);
    }

    void ResetBoxCoords()
    {
        boxMinX = boxMaxX = 0;
        boxMinY = boxMaxY = 0;
        boxWidth = boxHeight = 0;
        boxCenter = Vector2.zero;

        rectX = rectY = 0;
        rectWidth = rectHeight = 0;
    }
}