using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 操作キャラクラス

    // 操作対象角度リスト
    List<Vector3> rotateList = new List<Vector3>();

    [SerializeField] List<GameObject> partsList;

    public List<Vector3> GetRotateList()
    {
        rotateList.Clear();

        partsList.ForEach(info => {
            rotateList.Add(info.transform.localEulerAngles);
        });

        return rotateList;
    }
}
