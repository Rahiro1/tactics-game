using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealState : State
{
    public HealState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Cancel()
    {
        
        gameManager.SetState(new MenuState(gameManager));
        gameManager.mainGameMenuManager.ShowMenu();
        return base.Cancel();
    }

    public override IEnumerator ClickPlayer(UnitController unit)
    {
        // unit here is the target unit

        UnitController sourceUnit = gameManager.selectedPlayer;

        if (unit != sourceUnit)         // can't cast healing magic on yoursefl
        {
            int distanceBetweenSubjects = Mathf.Abs(sourceUnit.Location.x - unit.Location.x) + Mathf.Abs(sourceUnit.Location.y - unit.Location.y);

            HealingMagic selectedHeal = gameManager.selectedPlayer.Character.EquippedHeal;

            if (selectedHeal.range < distanceBetweenSubjects)       // test if in range of heal TODO- allow for dynamic healing range here
            {
                yield break;
            }

            unit.RecieveHeal(selectedHeal.power, selectedHeal.armourHealing);
            unit.Character.OnItemUse(selectedHeal);
            sourceUnit.SetHasActed(true);
            gameManager.selectedPlayer = null;
            gameManager.SetState(new PlayerTurnState(gameManager));
        }
    }

    public override IEnumerator Start()
    {
        // display heal range
        return base.Start();
    }
}
