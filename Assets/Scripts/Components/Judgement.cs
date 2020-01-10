using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    // ノルマ達成判定クラス

    // セーフ判定範囲
    float safeZone = 2.5f;

    // ノルマ部位と操作部位の回転の差で達成率を取得
    public int NormaJudgement(List<Vector3> playerList, List<Vector3> normaList)
    {
        int rate = 0;
        int addRateMax = 100 / normaList.Count;

        for (int i = 0; i < normaList.Count; i++)
        {
            int getRate = addRateMax;

            Vector3 diff = normaList[i] - playerList[i];

            if (diff.x < -safeZone) getRate += Mathf.CeilToInt(diff.x + safeZone);
            else if (diff.x > safeZone) getRate -= Mathf.CeilToInt(diff.x - safeZone);

            if (diff.y < -safeZone) getRate += Mathf.CeilToInt(diff.y + safeZone);
            else if (diff.y > safeZone) getRate -= Mathf.CeilToInt(diff.y - safeZone);

            if (diff.z < -safeZone) getRate += Mathf.CeilToInt(diff.z + safeZone);
            else if (diff.z > safeZone) getRate -= Mathf.CeilToInt(diff.z - safeZone);

            if (getRate < 0) getRate = 0;
            else if (getRate > addRateMax) getRate = addRateMax;

            rate += getRate;
        }

        return rate;
    }
}
