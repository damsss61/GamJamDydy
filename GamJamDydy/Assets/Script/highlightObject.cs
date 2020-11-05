using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlightObject : MonoBehaviour
{

    Material notHighLight;
    Material highLight;
    SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        highLight = Resources.Load<Material>("OutLineMaterial");
        notHighLight = Resources.Load<Material>("SpritePixelSnap");
        renderer = GetComponentInChildren<SpriteRenderer>();
        renderer.material = notHighLight;
        
    }

    private void OnMouseEnter()
    {
        renderer.material = highLight;
    }

    private void OnMouseExit()
    {
        renderer.material = notHighLight;
    }
}
