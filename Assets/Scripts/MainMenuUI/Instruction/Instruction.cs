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
    }
    private void OnEnable()
    {
        StaticEventHandler.OnInstructionChanged += OnInstructionChanged;
    }
    private void OnDestroy()
    {
        StaticEventHandler.OnInstructionChanged -= OnInstructionChanged;
    }
    private void OnInstructionChanged(string instruction, int displayCount)
    {
        if (displayCount < 3)
        {
            gameObject.SetActive(true);
            instructionText.text = instruction;
            string displayCountString = displayCount.ToString();
            this.displayCount.text = $"{displayCountString}/3";
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
