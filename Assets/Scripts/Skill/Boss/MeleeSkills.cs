using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class MeleeSkills : MonoBehaviour
{
    private Boss boss;
    private Weapon weapon;
    public float minDistance = 2f;
    [SerializeField] private LayerMask playerLayer;
    private MovementToPositionEvent movementToPositionEvent;
    private ContactFilter2D contactFilter2D;
    private Coroutine activeSkillCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    [SerializeField] private SoundEffectSO slashSoundEffect;
    public bool isAttacking;
    public float speed;
    public float cooldown;
    private float previousDistance;
    int count = 0;

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

    public void DealDamage(int damage)
    {
        weapon.GetWeaponCollider();
        SoundEffectManager.Instance.PlaySoundEffect(slashSoundEffect);
        Collider2D[] hitColliders = new Collider2D[1];
        int numColliders = Physics2D.OverlapCollider(weapon.polygonCollider2D, contactFilter2D, hitColliders);
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
        var targetPosition = GameManager.Instance.player.transform.position;
        var direction = Vector3.Normalize(targetPosition - transform.position);
        boss.animator.SetTrigger(Settings.Walk2);

        while (Vector2.Distance(targetPosition, transform.position) > minDistance && count <= 2)
        {
            if (Vector2.Distance(targetPosition, transform.position) == previousDistance)
            {
                count++;
            }
            previousDistance = Vector2.Distance(targetPosition, transform.position);
            movementToPositionEvent.CallMovementToPositionEvent(targetPosition, transform.position, speed, direction, true);
            yield return waitForFixedUpdate;
        }
        count = 0;
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



}
