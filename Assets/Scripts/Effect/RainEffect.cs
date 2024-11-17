using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffect : MonoBehaviour
{
    private int appearanceRate;
    void Start()
    {
        gameObject.SetActive(false);
        appearanceRate = Random.Range(0, 4);
        if (appearanceRate == 0)
        {
            gameObject.SetActive(true);
        }
    }
}
