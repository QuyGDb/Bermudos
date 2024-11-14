using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameBtn;
    [SerializeField] private Button continueGameBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button exitGameBtn;
    private Intro intro;

    private void Awake()
    {
        intro = GetComponentInChildren<Intro>();
    }
    private void Start()
    {
        newGameBtn.onClick.AddListener(OnNewGameClick);
    }
    private void OnNewGameClick()
    {

    }
}
