using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleteState : State
{
    public GameCompleteState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        gameManager.finalVictoryScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        GameManager.Instance.eventMessage.OpenMenu("Congratulations! Press return to continue.");

        yield return Utils.WaitForKeyPress(KeyCode.Return);

        gameManager.finalVictoryScreen.gameObject.SetActive(false);
        gameManager.titleScreenManager.gameObject.SetActive(true);
    }
}
