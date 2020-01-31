using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;//플레이중 화면 안꺼짐
        Screen.SetResolution(720, 1280, true);
    }
}
