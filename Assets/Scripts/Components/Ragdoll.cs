using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    // ラグドール制御クラス

    List<Rigidbody> list = new List<Rigidbody>();

    private void Start()
    {
        Rigidbody[] rbs = gameObject.GetComponentsInChildren<Rigidbody>();
        //子要素がいなければ終了
        if (rbs.Length > 0)
        {
            foreach (Rigidbody rb in rbs)
            {
                list.Add(rb);
            }
        }
    }

    public void SetRagdoll()
    {
        list.ForEach(p => {
            p.isKinematic = false;
        });
    }
}
