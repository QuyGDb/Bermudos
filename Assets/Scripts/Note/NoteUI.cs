using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    private void OnEnable()
    {
        StaticEventHandler.OnNoteOpened += OnNoteOpened;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnNoteOpened += OnNoteOpened;
    }
    private void OnNoteOpened()
    {

    }
}
