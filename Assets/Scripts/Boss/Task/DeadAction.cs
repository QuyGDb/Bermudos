using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAction : Action
{
    public SharedMaterial deadMaterial;
    [ColorUsage(true, true)]
    public Color fadeColor;
    public float fadeTime = 3.0f;
    private float fadeAmount = 0f;
    private float fadeTime2;
    Color defaultColor;

    public override void OnStart()
    {
        ColorUtility.TryParseHtmlString("#FFFF00", out defaultColor);
        deadMaterial.Value.SetFloat("_FadeAmount", fadeAmount);
        fadeTime2 = fadeTime;
        deadMaterial.Value.SetColor("_FadeBurnColor", fadeColor);
    }
    public override TaskStatus OnUpdate()
    {
        if (fadeTime > 0)
        {
            fadeAmount = fadeAmount + (Time.deltaTime / fadeTime2);
            deadMaterial.Value.SetFloat("_FadeAmount", fadeAmount);
            fadeTime -= Time.deltaTime;
            return TaskStatus.Running;
        }
        GameManager.Instance.HandleGameState(GameState.Won);
        return TaskStatus.Failure;
    }
    public override void OnEnd()
    {

        deadMaterial.Value.SetColor("_FadeBurnColor", defaultColor);
        gameObject.SetActive(false);
        deadMaterial.Value.SetFloat("_FadeAmount", 0.0f);
    }

}
