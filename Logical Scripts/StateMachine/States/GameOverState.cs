using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    public GameOverState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        

        GameManager.Instance.eventMessage.OpenMenu("Game over! Press return to continue.");

        yield return new WaitForSeconds(1f);
        yield return Utils.WaitForKeyPress(KeyCode.Return);

        // TODO make some way of accessing title screen from in game
        gameManager.titleScreenManager.gameObject.SetActive(true);
    }
}
