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
        strengthSlider.LoadSlider(character.Strength.GetbaseValue(), character.Strength.GetModifiedValue());
        magicSlider.LoadSlider(character.Magic.GetbaseValue(), character.Magic.GetModifiedValue());
        offenceSlider.LoadSlider(character.Offence.GetbaseValue(), character.Offence.GetModifiedValue());
        defenceSlider.LoadSlider(character.Defence.GetbaseValue(), character.Defence.GetModifiedValue());
        resistanceSlider.LoadSlider(character.Resistance.GetbaseValue(), character.Resistance.GetModifiedValue());
        speedSlider.LoadSlider(character.Speed.GetbaseValue(), character.Speed.GetModifiedValue());
        moveSlider.LoadSlider(character.Move.GetbaseValue(), character.Move.GetModifiedValue());
    }
}
