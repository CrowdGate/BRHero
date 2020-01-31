using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigFireball : MonoBehaviour
{
    // 巨大火球クラス

    [SerializeField] ParticleSystem myEffect;
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] Collider explosionCollider;
    [SerializeField] List<GameObject> targetList = new List<GameObject>();

    bool isMove = false;
    int index = 0;
    int indexMax = 0;

    private void Start()
    {
        indexMax = targetList.Count - 1;
        explosionCollider.enabled = false;
    }

    public IEnumerator PlayRoutine()
    {
        myEffect.Play();

        yield return new WaitForSeconds(2f);

        isMove = true;

        while (true)
        {
            if (isMove)
            {
                explosionEffect.transform.position = targetList[index].transform.position;
                explosionEffect.Play();
                explosionCollider.enabled = true;

                yield return new WaitForSeconds(0.1f);

                explosionCollider.enabled = false;

                yield return new WaitForSeconds(3f);

                if (!isMove) yield break;

                if (index < indexMax) index++;
                else index = 0;
            }
            else
            {
                yield break;
            }

            yield return null;
        }
    }

    public void GameEnd()
    {
        isMove = false;
        myEffect.Stop();
    }
}
