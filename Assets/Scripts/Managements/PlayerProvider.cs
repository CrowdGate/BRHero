using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProvider : MonoBehaviour
{
    // 操作キャラ提供クラス

    List<GameObject> playerObjList = new List<GameObject>();
    public List<Player> playerList { get; private set; } = new List<Player>();

    public List<Vector3> allRotateList { get; private set; } = new List<Vector3>();

    private int stageNo = 1;

    private void Start()
    {
        stageNo = PlayerPrefs.GetInt("CurrentStageNo", 1);

        // 操作対象キャラの読み込み
        var prefab = Resources.Load<GameObject>("Player/Dynamic_" + stageNo + "_0");
        var obj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.transform.parent = this.transform;
        Player player = obj.GetComponent<Player>();
        playerList.Add(player);
        playerObjList.Add(obj);

        // 配置用キャラの読み込み
        prefab = Resources.Load<GameObject>("Player/Static_" + stageNo);
        var staticObj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.transform.parent = this.transform;
    }

    public void Judgement()
    {
        allRotateList.Clear();

        playerList.ForEach(p => {
            var list = p.GetRotateList();
            list.ForEach(r => {
                allRotateList.Add(r);
            });
        });

        Debug.Log(allRotateList);
    }
}
