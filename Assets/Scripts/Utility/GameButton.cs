using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Button))]

public class GameButton : MonoBehaviour
{
    public enum Type
    {
        None,

        Dicide,
        Skip,
        Retry,

        End_Retry,
        End_Home,
        End_Craim,
        End_Next,

        Start,
        Skin,
        Select,
        Config_OnOff,
        Config_Sound,

        Chest_Open,
        Chest_Home,

        Skin_Home,
        Skin_Craim,
        Skin_Icon,
        Skin_TypePoint,
        Skin_TypeSurprise,

        Select_Home,
        Select_Icon,
    }

    [SerializeField] Type type;
    [SerializeField] bool IsOnePush = false;
    [SerializeField] bool IsPlaySe = true;

    private List<Image> childImages = new List<Image>();
    private List<Text> childTexts = new List<Text>();
    private Button myButton;
    private Animation myAnimation;

    public event Action<Type> OnMyType;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => OnMyType?.Invoke(type));
        GetComponent<Button>().onClick.AddListener(PlaySE);
        GetComponent<Button>().onClick.AddListener(SetInterractable);

        // 子供のImageやTextを設定
        foreach (Transform child in transform)
        {
            childImages.Add(child.GetComponent<Image>());
            childTexts.Add(child.GetComponent<Text>());
        }
        childImages.RemoveAll(item => item == null);
        childTexts.RemoveAll(item => item == null);

        myButton = GetComponent<Button>();
        myAnimation = GetComponent<Animation>();
    }

    public Type GetButtonType()
    {
        return type;
    }

    public void SetInteractable(bool OnOff)
    {
        myButton.interactable = OnOff;
    }

    // 一度押されたボタンの表示を非活性にする
    private void SetInterractable()
    {
        if (IsOnePush)
        {
            myButton.interactable = false;
            if (myAnimation != null) myAnimation.Stop();

            foreach (Image image in childImages)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
            }
            foreach (Text text in childTexts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0.5f);
            }
        }
    }
    // SEを再生する
    private void PlaySE()
    {
        //if (IsPlaySe) Sound.PlaySe("Button_1", 3, 0.6f);
    }
}
