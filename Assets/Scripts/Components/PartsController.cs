using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PartsController : MonoBehaviour
{
    // 操作対象のhumanの制御を行うクラス
    [SerializeField] RotateParts parts;
    [SerializeField] MoveObject moveObj;

    private void Start()
    {
        moveObj.OnDraging += (angle) => {
            Debug.Log(angle);
            parts.OnRotate(angle);
        };
    }
}