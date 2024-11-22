using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DealDamage : MonoBehaviour
{

    [SerializeField] private LayerMask enemyLayer;
    private ContactFilter2D contactFilter2D;
    private Weapon weapon;

    [SerializeField] public int damage = 10;
    private int attackCost = 10;
    private void Awake()
    {
        contactFilter2D.SetLayerMask(enemyLayer);
        weapon = GetComponent<Weapon>();
        contactFilter2D.useLayerMask = true;
        contactFilter2D.useTriggers = false;
    }

    private void OnEnable()
    {
        GameManager.Instance.player.dealDamageEvent.OnDealDamage += DealDamageEvent_OnDealDamage;
    }
    private void OnDisable()
    {
        GameManager.Instance.player.dealDamageEvent.OnDealDamage -= DealDamageEvent_OnDealDamage;
    }

    private void DealDamageEvent_OnDealDamage(DealDamageEvent dealDamageEvent, DealDamageEventAgrs dealDamageEventAgrs)
    {
        weapon.GetWeaponCollider();
        GameManager.Instance.player.stamina.UseStamina(attackCost);
        SoundEffectManager.Instance.PlaySoundEffect(GameManager.Instance.player.attackSoundEffect);

        Collider2D[] hitColliders = new Collider2D[1];
        int numColliders = Physics2D.OverlapCollider(GetComponent<Collider2D>(), contactFilter2D, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            Collider2D collider = hitColliders[i];
            if (collider != null)
            {
                GameManager.Instance.player.rage.IncreaseRage(Settings.rageAmount);
                DealDamageHandle(collider);


            }
        }
    }
    private void DealDamageHandle(Collider2D collision)
    {

        //TakeDame(); sẽ gọi destroy object lúc health về 0, lúc đó DamageEffect vẫn được gọi vì gameobject vẫn còn tồn tại, dù có check != thì object đó bị hủy trong lúc hàm đó đang chạy nên không giải quyết đươc, vì không thể xác định được 
        // lúc này object hủy, nên ta sẽ damageEffect trước TakeDamage, để damagepush được thực hiện trước khi object bị hủy, 
        // 1 bài test gọi thông qua event CallDamagePushEfectEvent(); thì không bị lỗi vì call event có lẽ được gọi chậm hơn gọi trước tiếp, nên object enemy đã bị hủy callevent mới được gọi, mà object đã hủy rồi nên sub không được gọi(ví dụ call pusheffeft đc gọi 5 lần còn callevent đc gọi 4 lần)
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>()?.enemyEffect.PushEnemyByWeapon(GameManager.Instance.player.transform.position);
            collision.GetComponent<ReceiveDamage>().TakeDamage(damage);
            collision.GetComponent<Enemy>().enemyEffect.CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponents<SpriteRenderer>());
            collision.GetComponent<Enemy>().enemyEffect.BloodEffect();
        }
        else if (collision.GetComponent<Boss>() != null)
        {
            collision.GetComponent<BossEffect>().PushBossByWeapon(GameManager.Instance.player.transform.position);
            collision.GetComponent<ReceiveDamage>().TakeDamage(damage);
            collision.GetComponent<BossEffect>().OuchEffect();
        }

    }


}
