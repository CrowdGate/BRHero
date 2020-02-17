using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IKHandle : MonoBehaviour
{
    // タップ先に自身を移動させるクラス
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50.0f))
            {
                Vector3 pos = hit.point;
                pos.z = 10f;
                transform.position = pos;
            }
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
