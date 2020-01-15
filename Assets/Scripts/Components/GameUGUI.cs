using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUGUI : MonoBehaviour
{
    [SerializeField] List<Button> buttons;
    
    // Dicideボタンが押されたときの処理
    public void OnDicide()
    {
        buttons.ForEach(btn => {
            btn.interactable = false;
        });
    }
}
