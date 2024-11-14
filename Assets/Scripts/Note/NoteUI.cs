using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private void Awake()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StaticEventHandler.OnNoteOpened += OnNoteOpened;
    }
    private void OnDestroy()
    {
        StaticEventHandler.OnNoteOpened += OnNoteOpened;
    }
    private void OnNoteOpened(OnNoteOpenedEventArgs onNoteOpenedEventArgs)
    {
        if (onNoteOpenedEventArgs.isOpening)
        {
            gameObject.SetActive(true);
            textMeshPro.text = onNoteOpenedEventArgs.note;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
