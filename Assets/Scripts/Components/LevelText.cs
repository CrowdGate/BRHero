using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelText : MonoBehaviour
{
    // レベル表記クラス

    TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        int stageNo = Stage.currentStageNo;
        var idStr = stageNo.ToString();
        var levelText = "Level " + idStr.PadLeft(3, '0');
        text.text = levelText;
    }

    public void SetLevel(int stageNo)
    {
        var idStr = stageNo.ToString();
        var levelText = idStr.PadLeft(3, '0');
        text.text = levelText;
    }
}
