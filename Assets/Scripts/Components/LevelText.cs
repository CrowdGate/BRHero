using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelText : MonoBehaviour
{
    // レベル表記クラス

    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        int stageNo = PlayerPrefs.GetInt("CurrentStageNo", 1);
        var idStr = stageNo.ToString();
        var levelText = "Level " + idStr.PadLeft(3, '0');
        text.text = levelText;
    }
}
