using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntroSO_", menuName = "ScriptableObjects/IntroSO", order = 1)]
public class IntroSO : ScriptableObject
{
    public Sprite introSprite;

    [TextArea]
    public string introText;
}
