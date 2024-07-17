using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamaManager : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("Coast", LoadSceneMode.Additive);
    }
}
