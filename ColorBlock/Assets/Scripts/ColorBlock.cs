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
    [SerializeField]private Vector2 correctPos;
    private Vector2 _currentPos;
    private GameObject _fixedIcon;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _fixedIcon = transform.Find("FixedIcon").gameObject;
        _fixedIcon.SetActive(false);
    }
    
    public void SetCorrectPos(Vector2 correctPos)
    {
        this.correctPos = correctPos;
        _currentPos = correctPos;
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

    //이동 함수
    
}
