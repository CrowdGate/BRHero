using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageOnOff : MonoBehaviour
{
    // ImageOnOffクラス

    [SerializeField] Sprite onImage;
    [SerializeField] Sprite offImage;
    Image myImage;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    public void ChangeSetting(bool onOff)
    {
        if (onOff)
        {
            myImage.sprite = onImage;
        }
        else
        {
            myImage.sprite = offImage;
        }
    }
}
