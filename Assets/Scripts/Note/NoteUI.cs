using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteUI : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private Image Image;
    private void Awake()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        Image = GetComponent<Image>();
        StaticEventHandler.OnNoteOpened += OnNoteOpened;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {

        Image.color = new Color(1, 1, 1, 0);
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0);
        Image.DOFade(1f, 1f).SetEase(Ease.InOutSine);
        textMeshPro.DOFade(1f, 1f).SetEase(Ease.InOutSine);
    }

    private void OnDestroy()
    {
        DOTween.Kill(this.transform);
        StaticEventHandler.OnNoteOpened -= OnNoteOpened;
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
