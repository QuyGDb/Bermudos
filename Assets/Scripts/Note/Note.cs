using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private LayerMask playerLayer;
    private void Start()
    {
        //Sequence moveSequence = DOTween.Sequence();
        //moveSequence.Append(transform.DOMoveY(transform.position.y + 1f, 1f))
        //            .Append(transform.DOMoveY(transform.position.y, 1f))
        //            .SetLoops(-1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((playerLayer.value & 1 << collision.gameObject.layer) > 0)
        {
            StaticEventHandler.CallNoteOpenedEvent();
        }
    }
}
