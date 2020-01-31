using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCreator : MonoBehaviour
{
    // 自身の周囲に最大n個の炎を生成するクラス

    [SerializeField] List<Vector3> createPosList = new List<Vector3>();
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem fireHitEffect;

    List<Fire> effectList = new List<Fire>();

    private void Start()
    {
        // ノルマの読み込み
        var prefab = Resources.Load<GameObject>("Effect/Fire");

        createPosList.ForEach(createPos => {

            if (transform.position.x > Stage.GetStartPos().x)
            {
                var obj = Instantiate(prefab, transform.position, Quaternion.identity);
                obj.transform.parent = this.transform;
                obj.transform.localPosition = createPos;
                Fire fire = obj.GetComponent<Fire>();

                if (Stage.isCheckPoint)
                {
                    fire.Play();
                }

                fire.OnHit += () => {
                    hitEffect.transform.localPosition = fire.transform.localPosition;
                    hitEffect.Play();
                    Debug.Log("Hit Fire");
                };
                fire.OnDead += () => {
                    hitEffect.transform.localPosition = fire.transform.localPosition;
                    hitEffect.Play();
                    Debug.Log("Dead Fire");
                };
                fire.OnFire += () => {
                    effectList.ForEach(effect => {
                        effect.Play();
                        fireHitEffect.transform.localPosition = fire.transform.localPosition;
                        fireHitEffect.Play();
                    });
                };

                effectList.Add(fire);
            }
        });
    }

    public void GameStart()
    {
        effectList.ForEach(fire => {
            fire.Play();
        });
    }
}
