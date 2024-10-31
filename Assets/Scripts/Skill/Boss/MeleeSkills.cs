using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class MeleeSkills : MonoBehaviour
{
    private Boss boss;
    private Weapon weapon;
    [SerializeField] private LayerMask playerLayer;
    private MovementToPositionEvent movementToPositionEvent;
    private ContactFilter2D contactFilter2D;
    private Coroutine activeSkillCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    public bool isAttacking;
    public float speed;
    private void Awake()
    {
        boss = GetComponent<Boss>();
        weapon = GetComponent<Weapon>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
    }

    private void Start()
    {
        isAttacking = false;
        contactFilter2D.SetLayerMask(playerLayer);
        contactFilter2D.useLayerMask = true;
        contactFilter2D.useTriggers = true;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    PlayActiveSkill(Settings.spearAtk2);
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    boss.animator.Play("spearAtkD");
        //}
    }
    public void DealDamage(int damage)
    {
        weapon.GetWeaponCollider();
        Collider2D[] hitColliders = new Collider2D[1];
        int numColliders = Physics2D.OverlapCollider(weapon.polygonCollider2D, contactFilter2D, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            Collider2D collider = hitColliders[i];
            Debug.Log(collider.gameObject.name + Time.frameCount);
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
        boss.animator.SetTrigger(Settings.Walk2);
        while (Vector2.Distance(targetPosition, transform.position) > minDistance)
        {
            movementToPositionEvent.CallMovementToPositionEvent(targetPosition, transform.position, speed, direction, true);
            yield return waitForFixedUpdate;
        }
        boss.animator.SetTrigger(skill);
        isAttacking = true;

    }

    private void StopActiveSkillRoutine()
    {
        if (activeSkillCoroutine != null)
        {
            StopCoroutine(activeSkillCoroutine);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.gameObject.layer == LayerMask.NameToLayer("Player"))
            return;
        StopActiveSkillRoutine();
        isAttacking = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.gameObject.layer == LayerMask.NameToLayer("Player"))
            return;
        StopActiveSkillRoutine();
        isAttacking = true;
    }

}
