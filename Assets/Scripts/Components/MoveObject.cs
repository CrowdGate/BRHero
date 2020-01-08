using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class MoveObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // 対象のオブジェクトを操作するクラス

    bool isDrag;
    bool isDeactiveTrigger;

    Vector2 hitStartPos = new Vector2();

    public event Action OnDragStart;
    public event Action<float> OnDraging;
    public event Action OnDragEnd;

    private void Start()
    {
        isDrag = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDeactiveTrigger = true;
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(eventData.position);
        TargetPos.z = gameObject.transform.position.z;
        //gameObject.transform.position = TargetPos;
        hitStartPos = TargetPos;

        OnDragStart?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(eventData.position);
        TargetPos.z = gameObject.transform.position.z;
        //gameObject.transform.position = TargetPos;

        Vector2 vecA = hitStartPos - (Vector2)gameObject.transform.position;
        Vector2 vecB = TargetPos - gameObject.transform.position;

        float angle = Vector2.Angle(vecA, vecB);
        Vector3 AxB = Vector3.Cross(vecA, vecB);

        if (AxB.z <= 0) angle = -angle;

        OnDraging?.Invoke(angle);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEnd?.Invoke();
    }
}
