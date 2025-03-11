using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryState : State
{
    public StoryState(GameManager gameManager) : base(gameManager)
    {
    }

    public StorySO currentStorySO;
    public int storyProgress;
    public int dialogueProgress;

    public override IEnumerator Start()
    {
        
        yield break;
    }

    public void InitiateStory(StorySO storySO)
    {
        currentStorySO = storySO;
        storyProgress = 0;
        dialogueProgress = 0;
        AdvanceStory();
    }

    public void AdvanceStory()
    {
        // if at the start or middle of dialogue advance dialogue,  else Advance Story 
        if (currentStorySO.storyDatas[storyProgress].dialogueList == null)
        {
            AdvanceStoryData();
        }
        else
        {
            if (dialogueProgress != currentStorySO.storyDatas[storyProgress].dialogueList.Count)
            {
                AdvanceDialogue();

            }
            else
            {
                AdvanceStoryData();
            }
        }

        
        
    }

    private void AdvanceStoryData()
    {
        GameManager.Instance.dialogueUI.CloseMenu();
        storyProgress++;
        dialogueProgress = 0;

        if (storyProgress >= currentStorySO.storyDatas.Count)
        {
            gameManager.PerformQueuedAction();
            return;
        }

        if (currentStorySO.storyDatas[storyProgress].dialogueList != null)
        {
            AdvanceDialogue();
        }
        else if (currentStorySO.storyDatas[storyProgress].storyEvent != null)
        {
            PlayStoryEvent();
        }
    }

    private void PlayStoryEvent()
    {
        //TODO - add Story Events
    }

    private void StartDialogue()
    {

    }

    public void AdvanceDialogue()
    {
        /*if (dialogueProgress == 0)
        {
            StartDialogue();
        } else                                                                                                                                                    
        {
            
        }*/

        GameManager.Instance.dialogueUI.playDialogue(currentStorySO.storyDatas[storyProgress].dialogueList[dialogueProgress], currentStorySO.storyDatas[storyProgress].background);
        dialogueProgress++;
    }

    public override IEnumerator LeftClickGeneral()
    {
        AdvanceStory();
        yield break;
    }

    public override IEnumerator SkipButton()
    {
        AdvanceStoryData();
        yield break;
    }
}
