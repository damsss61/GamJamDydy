﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnchor : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
