using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gimmick : MonoBehaviour
{
    // ギミック発動クラス

    public enum GIMMICK_TYPE
    {
        JUMP,
    };

    [SerializeField] GIMMICK_TYPE type;

    [SerializeField] Vector3 jumpEndPos;

    Collider collider;

    public event Action<Vector3> OnJump;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            if (type == GIMMICK_TYPE.JUMP)
            {
                OnJump?.Invoke(jumpEndPos);
            }
        }
    }
}
