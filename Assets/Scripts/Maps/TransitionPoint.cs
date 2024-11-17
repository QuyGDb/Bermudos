using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class TransitionPoint : MonoBehaviour
{
    public TransitionPointDetailsSO TransitionPointDetailsSO;
    private bool isCollider = false;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !isCollider)
        {
            StartCoroutine(MapTransitionCoroutine(collision));

            isCollider = true;
        }
    }
    private IEnumerator MapTransitionCoroutine(Collider2D collision)
    {
        StaticEventHandler.CallMapTransitionEvent();
        yield return waitForSeconds;
        if (SceneManager.GetSceneByName(TransitionPointDetailsSO.transitionPointName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(TransitionPointDetailsSO.transitionPointName);
        }
        SceneManager.LoadScene(TransitionPointDetailsSO.transitionMap, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        collision.GetComponent<Player>().transform.position = TransitionPointDetailsSO.playerSpawnPoint;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

        if (TransitionPointDetailsSO.transitionMap == "North Of The Forest")
            GameManager.Instance.HandleGameState(GameState.EngagedBoss);
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
}
