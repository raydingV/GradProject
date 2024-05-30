using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private DialogueManager _dialogueManager;
    
    [SerializeField] private Animator animator;
    
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera CutSceneCamera;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject boss;

    [SerializeField] private PlayerManager _playerManager;

    [SerializeField] private float transitionDuration = 1.0f;
    private float transitionTime = 0.0f;
    
    private bool isOrthographic = false;
    private bool isTransitioning = false;

    private bool callOne = false;

    private void Start()
    {
        if (_playerManager != null)
        {
            _playerManager.InputEnable = false;
        }
        
        if (_dialogueManager != null)
        {
            _dialogueManager.enabled = true;
        }
        else
        {
            if (_playerManager != null)
            {
                _playerManager.InputEnable = true;
            }
        }
    }

    void Update()
    { 
        ControlCutsceneEnd();
        CameraTransition();
    }

    void ControlCutsceneEnd()
    {
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsTag("Finish") && callOne == false)
        {
            callOne = true;
            MainCamera.gameObject.SetActive(true);

            if (boss != null)
            {
                boss.SetActive(true);   
            }

            if (canvas != null)
            {
                canvas.SetActive(false);   
            }

            // isTransitioning = true;
            // transitionTime = 0.0f;
        }
    }

    void CameraTransition()
    {
        if (isTransitioning)
        {
            transitionTime += Time.deltaTime;
            float t = transitionTime / transitionDuration;

            if (isOrthographic)
            {
                CutSceneCamera.fieldOfView = Mathf.Lerp(60, 0, t);
                CutSceneCamera.orthographicSize = Mathf.Lerp(0, 5, t);
            }
            else
            {
                CutSceneCamera.fieldOfView = Mathf.Lerp(0, 120, t);
                CutSceneCamera.orthographicSize = Mathf.Lerp(5, 0, t);
            }

            if (t >= 1.0f)
            {
                isTransitioning = false;
                // isOrthographic = !isOrthographic;
                // CutSceneCamera.orthographic = isOrthographic;
                MainCamera.gameObject.SetActive(true);
                // if (isOrthographic)
                // {
                //     CutSceneCamera.fieldOfView = 60;
                // }
                gameObject.SetActive(false);
            }
        }
    }
}
