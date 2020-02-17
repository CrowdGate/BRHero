using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraControll : MonoBehaviour
{
    // カメラ操作クラス
    public Camera camera { get; private set; }

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    // カメラの位置、回転の設定
    public void SetCameraPos(Vector3 pos)
    {
        camera.transform.position = pos;
    }
    public void SetCameraRotate(Vector3 pos)
    {
        camera.transform.rotation = Quaternion.Euler(pos);
    }
    public void SetCameraLocalPos(Vector3 pos)
    {
        camera.transform.localPosition = pos;
    }
    public void SetCameraLocalRotate(Vector3 pos)
    {
        camera.transform.localRotation = Quaternion.Euler(pos);
    }

    public void SetOrthographicSize(float size)
    {
        camera.orthographicSize = size;
    }

    // 対象の位置にカメラを移動する処理
    public void MoveCamera(Vector3 endPos, float time)
    {
        camera.transform.DOMove(endPos, time);
    }
    public void MoveLocalCamera(Vector3 endPos, float time)
    {
        camera.transform.DOLocalMove(endPos, time);
    }
    // 対象の位置にカメラを回転する処理
    public void RotateCamera(Vector3 endPos, float time)
    {
        camera.transform.DORotate(endPos, time);
    }
    public void RotateLocalCamera(Vector3 endPos, float time)
    {
        camera.transform.DOLocalRotate(endPos, time);
    }
}
