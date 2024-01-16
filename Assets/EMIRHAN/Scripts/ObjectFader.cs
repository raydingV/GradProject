using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    [SerializeField] private float FadeSpeed = 10;
    [SerializeField] private float FadeAmount = 0.5f;
    private float firstOpacity;

    GameManager gameManager;

    public bool DoFade = false;

    Material[] Materials;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Materials = GetComponent<Renderer>().materials;

        foreach (Material mat in Materials)
        {
            firstOpacity = mat.color.a;
        }
    }

    void Update()
    {
        //if(DoFade == true)
        //{
        //    FadeNow();
        //}
        //else
        //{
        //    ResetFade();
        //}

        if (gameManager.FadeObjects == true)
        {
            FadeNow();
        }
        else
        {
            ResetFade();
        }
    }


    void FadeNow()
    {
        foreach (Material mat in Materials)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, FadeAmount, FadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }     
    }

    void ResetFade()
    {
        foreach (Material mat in Materials)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, firstOpacity, FadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }       
    }
}
