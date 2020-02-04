using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem_Ingame : MonoBehaviour
{
    [Tooltip("if check show touch point")] public bool showTouchPoint;

    public ColorBlockManager colorBlockManager;
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

        // if (showTouchPoint)
        // {
        var xPosOnScreen = mousePosition.x >= 0 & mousePosition.x <= _screenWidth;
        var yPosOnScreen = mousePosition.y >= 0 & mousePosition.y <= _screenHeight;


        //화면위에 커서있음
        if (xPosOnScreen & yPosOnScreen)
        {
            var pos = mainCam.ScreenToWorldPoint(mousePosition);
            //Debug.Log(pos);
            _cursor.position = new Vector3(pos.x, pos.y, -2);
            ;

            if (Input.GetMouseButtonDown(0)) //마우스 왼클릭 Down
            {
                _cursorSpriteRenderer.color = Color.black;

                colorBlockManager.Attach(_cursor);
            }
            else if (Input.GetMouseButtonUp(0)) //마우스 왼클릭 Up
            {
                _cursorSpriteRenderer.color = Color.white;
                colorBlockManager.Detach(_cursor);
            }
        }
        else //화면밖에 커서있음
        {
            _cursor.position = Vector3.forward * -20;
        }
        //      }
#endif

//__________Android__________
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            TouchPositon = mainCam.ScreenToWorldPoint(touch.position);
            Debug.Log(TouchPositon);
            //Touch position debug
            if (showTouchPoint)
            {
                _cursor.position = new Vector3(TouchPositon.x, TouchPositon.y, -1);
                Debug.Log(_cursor.position);
            }

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //Debug.Log("Began Touch");
                    BeganTouch(TouchPositon);
                    break;
                case TouchPhase.Moved:
                    //Debug.Log("Moved Touch");
                    MovedTouch(TouchPositon);
                    break;
                case TouchPhase.Stationary:
                    //Debug.Log("Stationary Touch");
                    StationaryTouch(TouchPositon);
                    break;
                case TouchPhase.Ended:
                    //Debug.Log("Ended Touch");
                    EndedTouch(TouchPositon);
                    break;
            }
        }
#endif
    }


    //touch Fx
    private void BeganTouch(Vector3 touchPosition)
    {
        _cursor.position = new Vector3(touchPosition.x, touchPosition.y, -2);
        colorBlockManager.Attach(_cursor);
    }

    private void MovedTouch(Vector3 touchPosition)
    {
        _cursor.position = new Vector3(touchPosition.x, touchPosition.y, -2);
    }

    private void StationaryTouch(Vector3 touchPosition)
    {

    }

    private void EndedTouch(Vector3 touchPosition)
    {
        colorBlockManager.Detach(_cursor);
    }
}