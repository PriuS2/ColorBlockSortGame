using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class ColorBlock : MonoBehaviour
{
    private SpriteRenderer _sprite;
    [SerializeField] private Color spriteColor;
    public bool isCorrect;
    public bool isFixedBlock;
    [SerializeField] private Vector2 correctPos;
    [SerializeField] private Vector2 currentPos;
    private GameObject _fixedIcon;
    private bool _attached;
    private Transform _cursor;
    private Vector2 _deltaPos;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _fixedIcon = transform.Find("FixedIcon").gameObject;
        _fixedIcon.SetActive(false);
    }

    private void Update()
    {
        if (_attached)
        {
            transform.position = _cursor.position - new Vector3(_deltaPos.x, _deltaPos.y, 0);
        }
    }

    public void SetCorrectPos(Vector2 correctPos)
    {
        this.correctPos = correctPos;
        currentPos = correctPos;
    }

    public void SetCurrentPos(Vector2 currentPos)
    {
        this.currentPos = currentPos;
    }

    public Vector2 GetCurrentPos()
    {
        return currentPos;
    }

    public void SetColor(Color color)
    {
        spriteColor = color;
        _sprite.color = color;
    }

    public void ToggleFixedIcon(bool check)
    {
        _fixedIcon.SetActive(check);
    }


    public void Attach(Transform cursorTransform)
    {
        this._cursor = cursorTransform;
        _deltaPos = CalDeltaPos(_cursor);
        _attached = true;
        
        transform.localScale = Vector3.one * 1.3f;
    }

    public void Shake()
    {
    }

    public void Detach(int[] newPos)
    {
        _attached = false;
        transform.position = new Vector3(newPos[0], newPos[1], 0);
        SetCurrentPos(new Vector2(newPos[0], newPos[1]));
        transform.localScale = Vector3.one;
    }

    public void ReturnBlock()
    {
        _attached = false;
        transform.position = currentPos;
    }

    public void MoveBlock(int[] pos)
    {
        _attached = false;
        SetCurrentPos(new Vector2(pos[0], pos[1]));
        StartCoroutine(MoveBlockAnim(pos));
    }

    private IEnumerator MoveBlockAnim(int[] pos)
    {
        GetComponent<BoxCollider>().enabled = false;
        transform.localScale = Vector3.one * 0.8f;
        transform.position -= Vector3.forward;
        float time = 0;
        var firstPos = transform.position;
        var targetPos = new Vector3(pos[0], pos[1], 0);
        while (time < .3f)
        {
            Debug.Log(time);
            var temp = Vector3.Lerp(firstPos, targetPos, time/0.3f);
            transform.position = temp;
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        transform.localScale = Vector3.one;
        GetComponent<BoxCollider>().enabled = true;
    }


    private Vector2 CalDeltaPos(Transform cursorTransform)
    {
        var temp = cursorTransform.position - transform.position;
        return temp;
    }


    //이동 함수
}