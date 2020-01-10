using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Norma : MonoBehaviour
{
    // ノルマクラス

    // ノルマの角度リスト
    public List<Vector3> normaList { get; private set; } = new List<Vector3>();

    [SerializeField] List<GameObject> normaObjectList = new List<GameObject>();

    // ステージデータ
    private StageData stageData;

    void Start()
    {
        stageData = Resources.Load<StageData>(StageData.classPass);

        normaObjectList.ForEach(info => {
            normaList.Add(info.transform.localEulerAngles);
        });

        //normaList.ForEach(info => {
        //    Debug.Log("norma = " + info);
        //});
    }
}
