using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DealContactDamage : MonoBehaviour
{
    private PoiseEvent poiseEvent;
    #region Header DEAL DAMAGE
    [Space(10)]
    [Header("DEAL DAMAGE")]
    #endregion
    #region Tooltip
    [Tooltip("The contact damage to deal (is overridden by the receiver)")]
    #endregion
    [SerializeField] private int contactDamageAmount;
    #region Tooltip
    [Tooltip("Specify what layers objects should be on to receive contact damage")]
    #endregion
    private LayerMask playerlayerMask;
    private bool isColliding = false;
    private float stunTime;
    public bool isRemoved = false;
    private void Awake()
    {
        poiseEvent = GetComponent<PoiseEvent>();
    }
    private void OnEnable()
    {
        poiseEvent.OnPoise += PoiseEvent_OnPoise;
    }
    private void OnDisable()
    {
        poiseEvent.OnPoise -= PoiseEvent_OnPoise;
    }
    private void PoiseEvent_OnPoise(PoiseEvent poiseEvent, PoiseEventArgs poiseEventArgs)
    {
        if (poiseEventArgs.currentPoise <= 0)
        {
            stunTime = poiseEventArgs.stunTime;
        }
    }
    private void Start()
    {
        playerlayerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRemoved) return;
        // If already colliding with something return
        if (isColliding || stunTime > 0) return;
        int collisionObjectLayerMask = (1 << collision.gameObject.layer);

        if ((playerlayerMask.value & collisionObjectLayerMask) == 0)
            return;
        ContactDamage(collision);

        collision.GetComponent<PlayerEffect>().CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponentsInChildren<SpriteRenderer>());
    }

    // Trigger contact damage when staying withing a collider
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isRemoved) return;
        // If already colliding with something return
        if (isColliding || stunTime > 0) return;
        int collisionObjectLayerMask = (1 << collision.gameObject.layer);

        if ((playerlayerMask.value & collisionObjectLayerMask) == 0)
            return;
        ContactDamage(collision);
        collision.GetComponent<PlayerEffect>().CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponentsInChildren<SpriteRenderer>());
    }

    private void ContactDamage(Collider2D collision)
    {
        // if the collision object isn't in the specified layer then return (use bitwise comparison)
        // Check to see if the colliding object should take contact damage
        ReceiveContactDamage receiveContactDamage = collision.gameObject.GetComponent<ReceiveContactDamage>();
        if (receiveContactDamage != null)
        {
            isColliding = true;

            // Reset the contact collision after set time
            Invoke("ResetContactCollision", Settings.contactDamageCollisionResetDelay);
            receiveContactDamage.TakeContactDamage(contactDamageAmount);

        }

    }

    /// <summary>
    /// Reset the isColliding bool
    /// </summary>
    private void ResetContactCollision()
    {
        isColliding = false;
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(contactDamageAmount), contactDamageAmount, true);
    }
#endif
    #endregion
}
