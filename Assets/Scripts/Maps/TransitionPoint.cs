using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class TransitionPoint : MonoBehaviour
{
    public TransitionPointDetailsSO TransitionPointDetailsSO;
    private bool isCollider = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !isCollider)
        {
            if (SceneManager.GetSceneByName(TransitionPointDetailsSO.transitionPointName).isLoaded)
            {
                SceneManager.UnloadSceneAsync(TransitionPointDetailsSO.transitionPointName);
            }
            SceneManager.LoadScene(TransitionPointDetailsSO.transitionMap, LoadSceneMode.Additive);

            collision.GetComponent<Player>().transform.position = TransitionPointDetailsSO.playerSpawnPoint;
            isCollider = true;
        }
    }
}
