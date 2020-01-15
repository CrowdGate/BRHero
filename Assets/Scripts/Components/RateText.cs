using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RateText : MonoBehaviour
{
    // レート表記クラス

    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetRateText(int rate)
    {
        var str = rate + "%";
        text.text = str;
    }
}
