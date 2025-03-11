using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public Image background;
    public Image characterImageOne;
    public Image characterImageTwo;
    public Image characterImageThree;
    public Image characterImageFour;
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void SetBackground(Sprite backgroundSprite)
    {
        if(backgroundSprite != null)
        {
            background.sprite = backgroundSprite;
        }
        
    }

    public void playDialogue(Define.DialogueData dialogueData, Sprite background)
    {

        SetBackground(background);
        
        // load all sprites
        if (dialogueData.characterOne != null)
        {
            characterImageOne.sprite = dialogueData.characterOne.characterSprite;
            characterImageOne.gameObject.SetActive(true);
        } else
        {
            characterImageOne.gameObject.SetActive(false);
        }

        if (dialogueData.characterTwo != null)
        {
            characterImageTwo.sprite = dialogueData.characterTwo.characterSprite;
            characterImageTwo.gameObject.SetActive(true);
        }
        else
        {
            characterImageTwo.gameObject.SetActive(false);
        }

        if (dialogueData.characterThree != null)
        {
            characterImageThree.sprite = dialogueData.characterThree.characterSprite;
            characterImageThree.gameObject.SetActive(true);
        }
        else
        {
            characterImageThree.gameObject.SetActive(false);
        }

        if (dialogueData.characterFour != null)
        {
            characterImageFour.sprite = dialogueData.characterFour.characterSprite;
            characterImageFour.gameObject.SetActive(true);
        }
        else
        {
            characterImageFour.gameObject.SetActive(false);
        }


        switch (dialogueData.speaker)
        {
            case 1:
                if (dialogueData.characterOne == null)
                {
                    break;
                }
                //highlight char
                speakerNameText.text = dialogueData.characterOne.characterName;
                break;
            case 2:
                if (dialogueData.characterTwo == null)
                {
                    break;
                }
                //highlight char
                speakerNameText.text = dialogueData.characterTwo.characterName;
                break;
            case 3:
                if (dialogueData.characterThree == null)
                {
                    break;
                }
                //highlight char
                speakerNameText.text = dialogueData.characterThree.characterName;
                break;
            case 4:
                if (dialogueData.characterFour == null)
                {
                    break;
                }
                //highlight char
                speakerNameText.text = dialogueData.characterFour.characterName;
                break;
            default:
                //TODO-default case
                break;
        }

        // highlight speaking character
        // add spoken text
        DisplayDialogue(dialogueData.text);
        OpenMenu();
    }

    private void DisplayDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }

}
