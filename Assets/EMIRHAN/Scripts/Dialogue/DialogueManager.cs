using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueData _data;

    [SerializeField] GameManager levelManager;
    [SerializeField] private PlayerManager _playerManager;
    
    private string DialogueText;
    private int textValue = 0;
    
    [SerializeField] GameObject DialogueUI;

    public TextMeshProUGUI textDialogue;
    [SerializeField] float typingSpeedRiddle = 0.03f;
    [SerializeField] bool TypeEffect = false;

    private bool callOne = false;
    private bool typing = false;
    
    void Start()
    {
        levelManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_playerManager != null)
        {
            _playerManager.InputEnable = false;   
        }
        
        DialogueOnScreen(getText());
    }
    
    void Update()
    {
        nextDialogue();
        getText();
        endOfDialogue();
    }

    void endOfDialogue()
    {
        if (textValue > _data.Dialogues.Length && callOne == false)
        {
            callOne = true;
            bossEnable();
            DialogueOffScreen();
            
            if (_playerManager != null)
            {
                _playerManager.InputEnable = true;   
            }
        }
    }

    string getText()
    {
        if(_data != null && _data.Dialogues.Length > textValue)
        {
            DialogueText = _data.Dialogues[textValue];
        }

        return DialogueText;
    }

    void nextDialogue()
    {
        if (Input.anyKeyDown)
        {
            if (_data.Dialogues.Length >= textValue && typing == false)
            {
                textValue++;
                Debug.Log(textValue);
            }
            
            if (_data.Dialogues.Length > textValue && typing == false)
            {
                DialogueOnScreen(getText());
            }

            // if (typing == true)
            // {
            //     StopCoroutine(TypeEffectRiddle(getText()));
            //     // textDialogue.text = null;
            //     // textDialogue.text = getText().Replace("\\n", "\n");
            //     typing = false;
            // }
        }
    }

    void bossEnable()
    {
        if (levelManager != null && levelManager._bossManager != null)
        {
            levelManager._bossManager.enabled = true;
        }
    }
    
    public void DialogueOnScreen(string dialogue)
    {
        typing = true;
        
        textDialogue.text = null;
        
        textDialogue.fontSize = 12;

        if (DialogueUI != null)
        {
            DialogueUI.SetActive(true);
        }

        if(TypeEffect == true)
        {
            StartCoroutine(TypeEffectRiddle(dialogue.Replace("\\n", "\n")));
        }
        else
        {
            textDialogue.text = dialogue.Replace("\\n", "\n");
        }
    }

    public void DialogueOffScreen()
    {
        if (DialogueUI != null)
        {
            DialogueUI.SetActive(false);
        }
        textDialogue.text = null;
    }
    
    IEnumerator TypeEffectRiddle(string UIText)
    {
        foreach (char c in UIText)
        {
            textDialogue.text += c;
            yield return new WaitForSeconds(typingSpeedRiddle);

            if(textDialogue.text == null)
            {
                break;
            }
        }

        typing = false;
    }
}
