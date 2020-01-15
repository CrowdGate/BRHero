using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormaProvider : MonoBehaviour
{
    // 操作キャラ提供クラス

    public Norma norma { get; private set; }

    public List<Vector3> allRotateList { get; private set; } = new List<Vector3>();

    private int stageNo = 1;

    private void Start()
    {
        stageNo = PlayerPrefs.GetInt("CurrentStageNo", 1);

        // ノルマの読み込み
        var prefab = Resources.Load<GameObject>("Norma/Norma_" + stageNo);
        var obj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.transform.parent = this.transform;
        Norma objNorma = obj.GetComponent<Norma>();
        norma = objNorma;
    }
}
