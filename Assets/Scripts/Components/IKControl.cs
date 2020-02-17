using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
    Animator animator;

    float ikWeight = 0;
    Vector3 rayPos = new Vector3();
    Quaternion rayRotate = new Quaternion();

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void OnAnimatorIK()
    {
        // 位置のIK適用度
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);
        //animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
        // 回転のIK適用度
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);
        //animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeight);

        // 適用度を反映（位置）
        Vector3 pos = rayPos;
        animator.SetIKPosition(AvatarIKGoal.RightHand, pos);
        //animator.SetIKPosition(AvatarIKGoal.LeftHand, pos);
        // 適用度を反映（回転）
        Quaternion handRotation = Quaternion.LookRotation(rayPos - transform.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, handRotation);
        //animator.SetIKRotation(AvatarIKGoal.LeftHand, handRotation);
        // 上半身の向きを反映
        animator.SetLookAtWeight(ikWeight, ikWeight, 0, 0, 0);
        animator.SetLookAtPosition(pos);
    }

    public void OnActiveIK(Vector3 pos, Quaternion rotate)
    {
        rayPos = pos;
        rayRotate = rotate;
        ikWeight = 1;
        //StartCoroutine(OnIkRoutine());
    }
    public void OffActiveIK()
    {
        ikWeight = 0;
        //StartCoroutine(OffIkRoutine());
    }

    IEnumerator OnIkRoutine()
    {
        while (true)
        {
            if (ikWeight >= 1) yield break;

            ikWeight += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator OffIkRoutine()
    {
        while (true)
        {
            if (ikWeight <= 0) yield break;

            ikWeight -= Time.deltaTime * 5;

            yield return null;
        }
    }
}
