using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteUGUI : MonoBehaviour
{
    // CompleteUGUI表示クラス

    [SerializeField] Slider slider;
    [SerializeField] RateText rateText;

    int compRate = 0;

    public void SetRateState(int rate)
    {
        compRate = rate;
        StartCoroutine(SliderRoutine());
    }

    // 達成率演出コルーチン
    IEnumerator SliderRoutine()
    {
        int num = 0;

        while (true)
        {
            num++;
            slider.value = num;
            rateText.SetRateText(num);

            if (num >= compRate) yield break;
            else yield return new WaitForSeconds(0.01f);
        }
    }
}
