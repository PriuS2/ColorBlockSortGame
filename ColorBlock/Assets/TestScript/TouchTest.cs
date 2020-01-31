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
    
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam= Camera.main;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        
//__________Android__________
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            //Debug.Log("Touch!! : "+ Input.touchCount);
            Touch touch = Input.GetTouch(0);
            Vector3 touchPoint = mainCam.ScreenToWorldPoint(touch.position);
            //Debug.Log("TouchPoint = "+ touchPoint);
            //Debug.DrawLine(Vector3.zero, touchPoint, Color.magenta);
            var temp = new Vector3(touchPoint.x, touchPoint.y, -1);
            transform.position = temp;
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
            Vector3 mousePoint = mainCam.ScreenToWorldPoint(mousePosition);
            var temp = new Vector3(mousePoint.x, mousePoint.y, -1);
            transform.position = temp;
            Debug.Log(mousePoint);
        }
        else
        {
            transform.position = new Vector3(0,0,-20);
        }


#endif







    }

}
