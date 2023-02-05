using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mushroom", menuName = "Fungi/Mushroom")]

public class Mushroom : ScriptableObject
{
    public string Name = "New Mushroom";
    public Sprite Image = null;

    [Tooltip("Spore count needed to encounter Cordy <3")]
    public int Threshold;
    public DialogueEvent DialogueEvent;
}
