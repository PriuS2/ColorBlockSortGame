using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem_Ingame : MonoBehaviour
{
    [Tooltip("if check show touch point")] public bool showTouchPoint;

    private Camera mainCam;


    private Transform _cursor;
    private SpriteRenderer _cursorSpriteRenderer;

    private Vector3 TouchPositon;

#if UNITY_EDITOR
    private int _screenWidth;
    private int _screenHeight;
#endif

    // Start is called before the first frame update
    void Start()
    {
        _cursor = transform.Find("Cursor");
        _cursorSpriteRenderer = _cursor.GetComponent<SpriteRenderer>();
        _cursorSpriteRenderer.enabled = showTouchPoint;
        mainCam = Camera.main;

#if UNITY_EDITOR
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
#endif
    }

    // Update is called once per frame
    void Update()
    {
//__________Unity Editor__________
#if UNITY_EDITOR
        var mousePosition = Input.mousePosition;
        
        //Cursor position debug
        if (showTouchPoint)
        {
            var xPosOnScreen = mousePosition.x >= 0 & mousePosition.x <= _screenWidth;
            var yPosOnScreen = mousePosition.y >= 0 & mousePosition.y <= _screenHeight;
            if (xPosOnScreen & yPosOnScreen)//화면안에 커서있음
            {
            
            }
            else//화면안에 커서없음
            {
                
            }
        }
#endif

//__________Android__________
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            TouchPositon = mainCam.ScreenToWorldPoint(touch.position);

            //Touch position debug
            if (showTouchPoint)
            {
                _cursor.position = new Vector3(TouchPositon.x, TouchPositon.y, -1);
                Debug.Log(_cursor.position);
            }

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("Began Touch");
                    BeganTouch();
                    break;
                case TouchPhase.Moved:
                    Debug.Log("Moved Touch");
                    MovedTouch();
                    break;
                case TouchPhase.Stationary:
                    Debug.Log("Stationary Touch");
                    StationaryTouch();
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Ended Touch");
                    EndedTouch();
                    break;
            }
        }
#endif
    }


    private void BeganTouch()
    {
    }

    private void MovedTouch()
    {
    }

    private void StationaryTouch()
    {
    }

    private void EndedTouch()
    {
    }
}