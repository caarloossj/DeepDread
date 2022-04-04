using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterSheet : ScriptableObject
{
    public string CharacterName;
    public Color characterColor;
    public string[] emotionAnims;
    public StoryTrigger[] storyTriggers;
}

[Serializable]
 public struct StoryTrigger {
    public string triggerName;
    public bool isTrue;
 }