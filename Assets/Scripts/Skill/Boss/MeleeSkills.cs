using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeSkills : MonoBehaviour
{
    private Animator animator;
    private Weapon weapon;
    [SerializeField] private LayerMask playerLayer;
    private MovementToPositionEvent movementToPositionEvent;
    private ContactFilter2D contactFilter2D;
    private Coroutine activeSkillCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    public float speed;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponent<Weapon>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
    }

    private void Start()
    {
        contactFilter2D.SetLayerMask(playerLayer);
        contactFilter2D.useLayerMask = true;
        contactFilter2D.useTriggers = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            PlayActiveSkill(Settings.spearAtk);
        }
    }
    public void DealDamage(int damage)
    {
        weapon.GetWeaponCollider();
        Collider2D[] hitColliders = new Collider2D[1];
        int numColliders = Physics2D.OverlapCollider(GetComponent<Collider2D>(), contactFilter2D, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            Collider2D collider = hitColliders[i];
            if (collider != null)
            {
                collider.GetComponent<ReceiveDamage>()?.TakeDamage(damage);
                collider.GetComponent<PlayerEffect>()?.DamagePushEfect(transform.position);
                collider.GetComponent<PlayerEffect>()?.CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collider.GetComponentsInChildren<SpriteRenderer>());
            }
        }

    }
    public void PlayActiveSkill(int skill)
    {
        StopActiveSkillRoutine();
        activeSkillCoroutine = StartCoroutine(ActiveSkill(skill));
    }
    private IEnumerator ActiveSkill(int skill)
    {
        float minDistance = 0.15f;
        var targetPosition = GameManager.Instance.player.transform.position;
        var direction = Vector3.Normalize(targetPosition - transform.position);
        while (Vector2.Distance(targetPosition, transform.position) > minDistance)
        {
            movementToPositionEvent.CallMovementToPositionEvent(targetPosition, transform.position, speed, direction, true);
            yield return waitForFixedUpdate;
        }
        transform.position = targetPosition;
        animator.SetTrigger(Settings.isPhase2);
        animator.SetTrigger(skill);
    }
    private void StopActiveSkillRoutine()
    {
        if (activeSkillCoroutine != null)
        {
            StopCoroutine(activeSkillCoroutine);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        StopActiveSkillRoutine();
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay");
        StopActiveSkillRoutine();
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("OnTriggerEnter");
        StopActiveSkillRoutine();
    }
    private void OnTriggerStay(Collider collision)
    {
        Debug.Log("OnTriggerStay");
        StopActiveSkillRoutine();
    }

}
