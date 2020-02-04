using System;
using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Serialization;

public class ColorBlockManager : MonoBehaviour
{
    public Texture2D testStage;
    public Texture2D testStageFixed;

    private Transform _pivot;

    public GameObject blockTemplate;
    private int[] _gridScale = new int[2];
    private GameObject[,] _blocks; // = new GameObject[4,5];
    private int _blockWidth;
    private int _blockHeight;
    [SerializeField] private ColorBlock attachedBlock;


    // resources 폴더에 이미지 저장
    // 칸수(Width, Height)에 상관없이 카메라에서 볼때 너비 높이 같아야됨


    private void Start()
    {
        _pivot = transform.Find("Pivot");
        //var colors = sampleMap.GetPixels(0, 0, 5, 7);
        _blockWidth = testStage.width;
        _blockHeight = testStage.height;
        _blocks = new GameObject[_blockWidth, _blockHeight];
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
                _blocks[i, j] = Instantiate(blockTemplate, _pivot);
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
            for (int j = 0; j < height; j++)
            {
                ColorBlock colorBlock = _blocks[i, j].GetComponent<ColorBlock>();

                var color = testStage.GetPixel(i, j);
                colorBlock.SetColor(color);


                colorBlock.SetCorrectPos(new Vector2(i, j)); //정답위치 설정

                var color_fixed = testStageFixed.GetPixel(i, j);
                if (color_fixed.r != 1)
                {
                    colorBlock.ToggleFixedIcon(true);
                    colorBlock.isFixedBlock = true;
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


    #region Mix Blocks

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

    #endregion


    public void Attach(Transform cursor)
    {
        RaycastHit hit;
        var isHit = Physics.Raycast(cursor.position, Vector3.forward, out hit, 15);
        if (isHit)
        {
            var temp = hit.transform.gameObject;
            attachedBlock = temp.GetComponent<ColorBlock>();

            if (attachedBlock.isFixedBlock)
            {
                //고정블럭
                attachedBlock.Shake();
                attachedBlock = null;
            }
            else
            {
                //자유블럭
                attachedBlock.Attach(cursor);
            }
        }
    }

    public void Detach(Transform cursor)
    {
        if (!attachedBlock)
        {
            return;
        }

        //커서가 일정 범위 안에 있는가?
        var cursorPos = cursor.position;
        bool checkXLimit = cursorPos.x > -0.5f & cursorPos.x < _blockWidth - 0.5f;
        bool checkYLimit = cursorPos.y > -0.5f & cursorPos.y < _blockHeight - 0.5f;

        if (checkXLimit & checkYLimit)
        {
            int[] arrivalPos = {Mathf.RoundToInt(cursorPos.x), Mathf.RoundToInt(cursorPos.y)};
            //도착좌표가 fixed인가
            var fixedBlockCheck = _blocks[arrivalPos[0], arrivalPos[1]].GetComponent<ColorBlock>().isFixedBlock;
            if (fixedBlockCheck)
            {
                attachedBlock.ReturnBlock();
                attachedBlock = null;
            }
            else
            {
                //교체
                var beforePos = attachedBlock.GetCurrentPos();
                int[] beforePosToArray = {(int) beforePos.x, (int) beforePos.y};
                attachedBlock.Detach(arrivalPos);
                _blocks[arrivalPos[0],arrivalPos[1]].GetComponent<ColorBlock>().MoveBlock(beforePosToArray);
                //스왑 _blocks
                SwapBlock(beforePosToArray, arrivalPos);
            }
        }
        else
        {
            attachedBlock.ReturnBlock();
            attachedBlock = null;
        }
    }

    private void SwapBlock(int[] blockA, int[] blockB)
    {
        var temp = _blocks[blockA[0], blockA[1]];
        _blocks[blockA[0], blockA[1]] = _blocks[blockB[0], blockB[1]];
        _blocks[blockB[0], blockB[1]] = temp;
    }
}