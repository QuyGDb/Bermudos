using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI instructionText;
    [SerializeField] TextMeshProUGUI displayCount;
    private void Start()
    {
        gameObject.SetActive(false);
        StaticEventHandler.OnInstructionChanged += OnInstructionChanged;
    }
    private void OnEnable()
    {

        transform.localScale = Vector3.one * 2;
        transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

    private void OnDestroy()
    {
        StaticEventHandler.OnInstructionChanged -= OnInstructionChanged;
        DOTween.Kill(this.transform);
    }
    private void OnInstructionChanged(string instruction, int displayCount = -1, bool isActive = true)
    {
        if (displayCount == -1)
        {
            gameObject.SetActive(true);
            instructionText.text = instruction;
            this.displayCount.text = "";
            return;
        }
        if (displayCount >= 0 && displayCount < 3)
        {
            transform.localScale = Vector3.one * 2;
            transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
            gameObject.SetActive(true);
            instructionText.text = instruction;
            string displayCountString = displayCount.ToString();
            this.displayCount.text = $"{displayCountString}/3";
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (!isActive)
        {
            gameObject.SetActive(false);
        }
    }
}
