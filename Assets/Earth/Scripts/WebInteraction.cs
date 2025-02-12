using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebInteraction : MonoBehaviour
{
    // 자바스크립트에서 호출할 함수
    public void ChangeColor(string color)
    {
        Image image = GetComponent<Image>();

        switch(color.ToLower())
        {
            case "red":
                image.color = Color.red;
                break;
            case "green":
                image.color = Color.green;
                break;
            case "blue":
                image.color = Color.blue;
                break;
            default:
                image.color = Color.white;
                break;
        }
    }
}

