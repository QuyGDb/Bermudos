using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera zoomCamera;
    private Cinemachine.CinemachineConfiner2D cinemachineConfiner2D;
    private Collider2D mapCollider;

    private void Awake()
    {
        cinemachineConfiner2D = GetComponentInChildren<Cinemachine.CinemachineConfiner2D>();

    }

    private void OnEnable()
    {
        StaticEventHandler.OnMapChanged += StaticEventHandler_OnMapChanged;
        GameManager.Instance.OnGameStateChange += GameManager_OnGameStateChange;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnMapChanged -= StaticEventHandler_OnMapChanged;
    }

    private void GameManager_OnGameStateChange(GameState gameState)
    {
        if (gameState == GameState.Intro)
        {
            StartCoroutine(ZoomCam());
        }

        if (gameState == GameState.Begin)
        {
            float currentOrthoSize = zoomCamera.m_Lens.OrthographicSize;
            DOTween.To(() => currentOrthoSize, x =>
            {
                currentOrthoSize = x;
                zoomCamera.m_Lens.OrthographicSize = currentOrthoSize;
            }
            , Settings.orthoSize, 5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                zoomCamera.gameObject.SetActive(false);
                cinemachineVirtualCamera.gameObject.SetActive(true);
                GameManager.Instance.OnGameStateChange -= GameManager_OnGameStateChange;
                GameManager.Instance.HandleGameState(GameState.Instruct);
            });

        }
        if (gameState == GameState.Play)
        {
            zoomCamera.gameObject.SetActive(false);
            cinemachineVirtualCamera.gameObject.SetActive(true);
            GameManager.Instance.OnGameStateChange -= GameManager_OnGameStateChange;
        }
    }
    private void StaticEventHandler_OnMapChanged(MapChangedEventArgs mapChangedEventArgs)
    {
        Confine(mapChangedEventArgs.map);
    }
    private void Start()
    {
        cinemachineVirtualCamera.Follow = GameManager.Instance.player.transform;
        zoomCamera.Follow = GameManager.Instance.player.transform;
    }
    private IEnumerator ZoomCam()
    {
        yield return new WaitForSeconds(0.1f);
        cinemachineVirtualCamera.gameObject.SetActive(false);
    }
    private void Confine(Map map)
    {
        if (map == null)
            return;
        mapCollider = map.GetComponent<Collider2D>();

        if (mapCollider != null)
        {
            cinemachineConfiner2D.m_BoundingShape2D = mapCollider;
        }
    }
}
