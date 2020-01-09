using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateParts : MonoBehaviour, IRotateMoveable
{
    // 回転するパーツの制御クラス

    //[SerializeField] float maxAngle = 180;
    //[SerializeField] float minAngle = 0;

    Vector3 rotate = new Vector3();

    Vector3 myStartPos { get; set; } = new Vector3();
    Quaternion myQuaternion { get; set; } = new Quaternion();

    private void Awake()
    {
        rotate = gameObject.transform.eulerAngles;
        Debug.Log(rotate);
    }

    // 自身を回転させる
    public void OnRotate(Vector2 movePos)
    {
        // 回転軸vecA
        Vector2 vecA = gameObject.transform.position;
        // タッチ軸vecB
        Vector2 vecB = movePos;

        float angle = GetAngle(vecA, vecB);

        //if (angle < minAngle) angle = minAngle;
        //else if (angle > maxAngle) angle = maxAngle;

        gameObject.transform.rotation = Quaternion.Euler(rotate.x, rotate.y, -angle);
    }

    // 開始点から目標点へのアングルを取得
    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.x, dt.y);
        float degree = rad * Mathf.Rad2Deg;

        if (degree < 0)
        {
            degree += 360;
        }
        else if (degree > 360)
        {
            degree -= 360;
        }

        return degree;
    }
}
