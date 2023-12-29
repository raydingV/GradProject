using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public bool clicked = false;
    public bool mouseOver = false;

    private void OnMouseDown()
    {
        Debug.Log("Clicked");
        Click(true, false);
    }

    private void OnMouseUp()
    {
        Click(false, true);
    }

    void Click(bool isClick, bool clickOver)
    {
        clicked = isClick;
        mouseOver = clickOver;
    }
}
