using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BossClearGimmick : MonoBehaviour
{
    // ボスクリア時の演出ギミック

    [SerializeField] GameObject human;
    [SerializeField] List<Animator> humans = new List<Animator>();
    [SerializeField] List<Vector3> humanMovePosList = new List<Vector3>();

    public IEnumerator BossClearRoutine()
    {
        transform.DOMoveX(3, 1f);

        for (int i = 0; i < humans.Count; i++)
        {
            humans[i].transform.DOScale(Vector3.one, 1f);
            humans[i].transform.DOLocalMove(humanMovePosList[i], 1f);
        }

        yield return new WaitForSeconds(1f);

        humans.ForEach(animator => {
            animator.Play("Walk");
        });
        human.transform.DOMoveX(20, 5f);
    }

    public GameObject GetHuman()
    {
        return human;
    }
}
