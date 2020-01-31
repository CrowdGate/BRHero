using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IKHandle : MonoBehaviour
{
    // タップ先に自身を移動させるクラス
    [SerializeField] Camera orthographic;
    [SerializeField] public float distanceZ = 10f;

    SpriteRenderer sprite;
    public bool OnOff { get; private set; } = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && OnOff)
        {
            var pos = orthographic.ScreenToWorldPoint(Input.mousePosition);
            pos.z = distanceZ;
            transform.position = pos;
        }
    }

    public void SetOnOff(bool onOff)
    {
        OnOff = onOff;
    }
    public void SetView(bool onOff)
    {
        sprite.enabled = onOff;
    }
}
