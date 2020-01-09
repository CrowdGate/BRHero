using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class Test : MonoBehaviour
{
    bool isTouch = false;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTouch)
        {
            isTouch = true;
            transform.DOLocalMoveX(1, 1f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isTouch = false;
        }

    }

}
