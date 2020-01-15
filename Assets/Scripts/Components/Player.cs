using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 操作キャラクラス

    // 操作対象角度リスト
    List<Vector3> rotateList = new List<Vector3>();

    [SerializeField] List<GameObject> partsList = new List<GameObject>();

    [SerializeField] List<MoveObject> moveObjectList = new List<MoveObject>();

    public List<Vector3> GetRotateList()
    {
        rotateList.Clear();

        partsList.ForEach(info => {
            rotateList.Add(info.transform.localEulerAngles);
        });

        moveObjectList.ForEach(info => {
            info.Diside();
        });

        return rotateList;
    }
}
