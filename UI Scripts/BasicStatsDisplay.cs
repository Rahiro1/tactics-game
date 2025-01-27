using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicStatsDisplay : MonoBehaviour
{
    [SerializeField]
    private StatSlider strengthSlider, magicSlider, offenceSlider, defenceSlider, resistanceSlider, speedSlider, moveSlider;

    public void LoadMenu(Character character)
    {
        strengthSlider.LoadSlider(character.UnmodifiedStrength.value, character.ModifiedStrength);
        magicSlider.LoadSlider(character.UnmodifiedMagic.value, character.ModifiedMagic);
        offenceSlider.LoadSlider(character.UnmodifiedOffence.value, character.ModifiedOffence);
        defenceSlider.LoadSlider(character.UnmodifiedDefence.value, character.ModifiedDefence);
        resistanceSlider.LoadSlider(character.UnmodifiedResistance.value, character.ModifiedResistance);
        speedSlider.LoadSlider(character.UnmodifiedSpeed.value, character.ModifiedSpeed);
        moveSlider.LoadSlider(character.Move.value, character.Move.value);
    }
}
