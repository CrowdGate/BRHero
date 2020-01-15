using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Reaction : MonoBehaviour
{
    // リザルト演出クラス

    public Animator animator { get; private set; }

    public Vector3 cameraPos { get; private set; }

    public event Action OnMoveCamera;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // 成功演出アニメーション再生
    public void PlayAnimation(int stageNo)
    {
        animator.Play("Reaction_" + stageNo);
    }

    // Event通過時に発火されるカメラ移動情報取得
    public void MoveCamera()
    {
        OnMoveCamera?.Invoke();
    }
}
