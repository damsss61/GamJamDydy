using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnchor : MonoBehaviour
{
    private void Start()
    {
        GameObject music = GameObject.Find("Music");
        if (music!=null)
        {
            if (music!=gameObject)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
