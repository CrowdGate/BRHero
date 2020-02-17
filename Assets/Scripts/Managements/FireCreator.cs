using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCreator : MonoBehaviour
{
    // 自身の周囲に最大n個の炎を生成するクラス

    [SerializeField] List<Vector3> createPosList = new List<Vector3>();

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

                if (!Stage.isChapterBegin)
                {
                    fire.Play();
                }

                fire.OnHit += () => {

                };
                fire.OnDead += () => {

                };
                fire.OnFire += () => {
                    effectList.ForEach(effect => {
                        effect.Play();
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
