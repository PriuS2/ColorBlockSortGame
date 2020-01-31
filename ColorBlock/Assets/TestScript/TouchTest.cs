using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class TouchTest : MonoBehaviour
{
    private Camera mainCam;
    private int screenWidth;
    private int screenHeight;

    public bool showTouchPoint = false;
    private Transform _pointer;
    private SpriteRenderer _pointer_SpriteRenderer;

    public Vector3 CursorPositon;
    
    // Start is called before the first frame update
    void Start()
    {
        _pointer = transform.Find("Pointer");
        _pointer_SpriteRenderer = _pointer.GetComponent<SpriteRenderer>();
        mainCam= Camera.main;
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        _pointer_SpriteRenderer.enabled = showTouchPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
//__________Android__________
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            CursorPositon = mainCam.ScreenToWorldPoint(touch.position);
            
            //Debug
            var temp = new Vector3(CursorPositon.x, CursorPositon.y, -1);
            transform.position = temp;
            
            
            
        }
        else
        {
            transform.position = new Vector3(0,0,-20);
        }
#endif
        
//__________Unity Editor__________
#if UNITY_EDITOR
        //if(Input.mouse)
        //Debug.Log(Input.mousePosition);
        var mousePosition = Input.mousePosition;

        var checkWidth = mousePosition.x >= 0 & mousePosition.x <= screenWidth;
        var checkHeight = mousePosition.y >= 0 & mousePosition.y <= screenHeight;
        
        if (checkWidth & checkHeight)
        {
            Vector3 CursorPositon = mainCam.ScreenToWorldPoint(mousePosition);
            var temp = new Vector3(CursorPositon.x, CursorPositon.y, -1);
            transform.position = temp;
            Debug.Log(CursorPositon);


            if (Input.GetMouseButtonDown(0))
            {
                _pointer_SpriteRenderer.color = Color.black;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _pointer_SpriteRenderer.color = Color.white;
            }
            
            
        }
        else
        {
            transform.position = new Vector3(0,0,-20);
        }


#endif
        
    }

}
