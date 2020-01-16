using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ImageMove : MonoBehaviour
{
    // ImageやSpriteをDoTweenで制御するクラス

    public void MoveImage(Vector2 move, float time = 1f)
    {
        transform.DOLocalMove(move, time);
    }
}
