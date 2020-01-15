using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProvider : MonoBehaviour
{
    // 操作キャラ提供クラス

    public Player player { get; private set; }

    public List<Vector3> allRotateList { get; private set; } = new List<Vector3>();

    private int stageNo = 1;

    List<Ragdoll> ragdollList = new List<Ragdoll>();

    private void Start()
    {
        stageNo = PlayerPrefs.GetInt("CurrentStageNo", 1);

        // 操作対象キャラの読み込み
        var prefab = Resources.Load<GameObject>("Player/Dynamic_" + stageNo);
        var publicObj = Instantiate(prefab, transform.position, Quaternion.identity);
        publicObj.transform.parent = this.transform;
        Player playerObj = publicObj.GetComponent<Player>();
        player = playerObj;
        Ragdoll ragdoll = publicObj.GetComponent<Ragdoll>();
        if (ragdoll != null) ragdollList.Add(ragdoll);

        // 配置用キャラの読み込み
        prefab = Resources.Load<GameObject>("Player/Static_" + stageNo);
        var staticObj = Instantiate(prefab, transform.position, Quaternion.identity);
        staticObj.transform.parent = this.transform;
        ragdoll = staticObj.GetComponent<Ragdoll>();
        if (ragdoll != null) ragdollList.Add(ragdoll);
    }

    public void SetRotateEnd()
    {
        allRotateList.Clear();

        var list = player.GetRotateList();
        list.ForEach(r => {
            allRotateList.Add(r);
        });
    }
    public void PlayFailed()
    {
        ragdollList.ForEach(rd => {
            rd.SetRagdoll();
        });
    }
}
