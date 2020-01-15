using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReactionCamera : MonoBehaviour
{
    // リアクションカメラクラス
    public Camera reactionCamera { get; set; }

    private void Start()
    {
        reactionCamera = GetComponent<Camera>();
    }

    // 対象の位置にカメラを移動する処理
    public void MoveCamera(Vector3 endPos, float time)
    {
        reactionCamera.transform.DOMove(endPos, time);
    }
    // 対象の位置にカメラを回転する処理
    public void RotateCamera(Vector3 endPos, float time)
    {
        reactionCamera.transform.DORotate(endPos, time);
    }
}
