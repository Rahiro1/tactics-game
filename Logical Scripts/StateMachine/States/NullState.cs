using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullState : State
{
    // a state where no state related actions take place
    public NullState(GameManager gameManager) : base(gameManager)
    {
    }
}
