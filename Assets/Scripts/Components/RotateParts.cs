using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateParts : MonoBehaviour, IRotateMoveable
{
    // 回転するパーツの制御クラス

    // 回転方向
    private enum ROTATE_TYPE
    {
        X_L,
        X_R,
        Y_L,
        Y_R,
        Z_L,
        Z_R,
    };

    [SerializeField] ROTATE_TYPE type = ROTATE_TYPE.Z_L;

    Vector3 rotate = new Vector3();

    private void Awake()
    {
        rotate = gameObject.transform.eulerAngles;
    }

    // 自身を回転させる
    public void OnRotate(Vector2 movePos)
    {
        // 回転軸vecA
        Vector2 vecA = gameObject.transform.position;
        // タッチ軸vecB
        Vector2 vecB = movePos;

        float angle = GetAngle(vecA, vecB);
        Quaternion qua = new Quaternion();

        switch (type)
        {
            case ROTATE_TYPE.X_R:
                angle = -angle;
                qua = Quaternion.Euler(angle, rotate.y, rotate.z);
                break;
            case ROTATE_TYPE.X_L:
                qua = Quaternion.Euler(angle, rotate.y, rotate.z);
                break;
            case ROTATE_TYPE.Y_R:
                angle = -angle;
                qua = Quaternion.Euler(rotate.x, angle, rotate.z);
                break;
            case ROTATE_TYPE.Y_L:
                qua = Quaternion.Euler(rotate.x, angle, rotate.z);
                break;
            case ROTATE_TYPE.Z_R:
                angle = -angle;
                qua = Quaternion.Euler(rotate.x, rotate.y, angle);
                break;
            case ROTATE_TYPE.Z_L:
                qua = Quaternion.Euler(rotate.x, rotate.y, angle);
                break;
            default:
                break;
        };

        gameObject.transform.rotation = qua;

        Debug.Log("angle = " + angle);
        Debug.Log("qua = " + qua);
        Debug.Log("RotateParts = " + gameObject.transform.rotation);
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
