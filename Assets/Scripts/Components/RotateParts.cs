using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateParts : MonoBehaviour, IRotateMoveable
{
    // 回転するパーツの制御クラス
    float maxAngle = 90;
    float minAngle = -90;

    Vector3 rotate = new Vector3();

    Vector3 myStartPos { get; set; } = new Vector3();
    Quaternion myQuaternion { get; set; } = new Quaternion();

    private void Awake()
    {
        rotate = gameObject.transform.localEulerAngles;
    }

    // 自身を回転させる
    public void OnRotate(float angle)
    {
        if (angle < minAngle) angle = minAngle;
        else if (maxAngle < angle) angle = maxAngle;

        gameObject.transform.localRotation = Quaternion.Euler(rotate.x, rotate.y, angle);
    }
}
