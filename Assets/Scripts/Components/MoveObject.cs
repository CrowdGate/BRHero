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
    public event Action<Vector2> OnDraging;
    public event Action OnDragEnd;

    private void Start()
    {
        isDrag = false;
    }

    public void Diside()
    {
        Destroy(gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDeactiveTrigger = true;
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(eventData.position);
        TargetPos.z = gameObject.transform.position.z;
        hitStartPos = TargetPos;

        OnDragStart?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(eventData.position);
        TargetPos.z = gameObject.transform.position.z;

        OnDraging?.Invoke(TargetPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEnd?.Invoke();
    }
}
