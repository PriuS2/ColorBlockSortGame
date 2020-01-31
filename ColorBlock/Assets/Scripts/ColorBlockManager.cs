using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ColorBlockManager : MonoBehaviour
{
    public Texture2D testStage;
    public Texture2D testStageFixed;
    
    private Transform _pivot;
    
    public GameObject blockTemplate;
    private int[] _gridScale = new int[2];
    private GameObject[,] _blocks;// = new GameObject[4,5];
    private int _blockWidth;
    private int _blockHeight;
    
    
    // resources 폴더에 이미지 저장
    // 칸수(Width, Height)에 상관없이 카메라에서 볼때 너비 높이 같아야됨


    private void Start()
    {
        _pivot = transform.Find("Pivot");
        //var colors = sampleMap.GetPixels(0, 0, 5, 7);
        _blockWidth = testStage.width;
        _blockHeight = testStage.height;
        _blocks = new GameObject[_blockWidth,_blockHeight];
        CreateBlocks(_blockWidth, _blockHeight);
    }


    #region 초기 셋팅 : 생성, 색설정, 기본데이터
    private void CreateBlocks(int width, int height)
    {
        //_bolcks.Length
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var deltaPos = CalDeltaPos(i, j);
                _blocks[i,j] = Instantiate(blockTemplate, _pivot);
                _blocks[i, j].transform.position = deltaPos;
            }
        }

        SetBlocks();
    }
    
    private void SetBlocks()
    {
        int width = testStage.width;
        int height = testStage.height;

        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j< height; j++)
            {
                ColorBlock colorBlock = _blocks[i, j].GetComponent<ColorBlock>();

                var color = testStage.GetPixel(i, j);
                colorBlock.SetColor(color);
                
                colorBlock.SetCorrectPos(new Vector2(i,j));
                var color_fixed = testStageFixed.GetPixel(i, j);
                if (color_fixed.r != 1)
                {
                    colorBlock.ToggleFixedIcon(true);
                }
            }
        }
    }

    private Vector3 CalDeltaPos(int xPos, int yPos)
    {
        var deltaPos = _pivot.position + new Vector3(xPos, yPos, 0);
        return deltaPos;
    }
    #endregion


    //
    // #region 블럭 섞기
    //
    // // private void MixBlocks()
    // // {
    // //     
    // // }
    //
    // private IEnumerator MixBlocks()
    // {
    //     for (int i = 0; i < _blockWidth; i++)
    //     {
    //         for (int j = 0; j < _blockHeight; j++)
    //         {
    //             var block = _blocks[i, j].GetComponent<ColorBlock>();
    //             block.
    //         }
    //     }
    //     yield return null;
    // }
    //
    // #endregion
    //
    
    
    
}
