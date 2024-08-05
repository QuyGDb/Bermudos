using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private Player player;
    //[SerializeField] private DamagePushEfectEvent damagePushEfectEvent;

    [SerializeField] private LayerMask enemyLayer;
    private bool isDealDamage = false;
    private bool hasAttack = false;
    private int damage;

    private void OnEnable()
    {
        player.dealDamageEvent.OnDealDamage += DealDamageEvent_OnDealDamage;
    }
    private void OnDisable()
    {
        player.dealDamageEvent.OnDealDamage -= DealDamageEvent_OnDealDamage;
    }

    private void DealDamageEvent_OnDealDamage(DealDamageEvent dealDamageEvent, DealDamageEventAgrs dealDamageEventAgrs)
    {
        if (isDealDamage == false)
        {
            damage = dealDamageEventAgrs.damage;
            isDealDamage = true;
        }
        else
        {
            isDealDamage = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDealDamage && !hasAttack)
        {
            hasAttack = true;
            if ((1 << collision.gameObject.layer & enemyLayer.value) > 0)
            {
                Debug.Log("DealDamage");
                //TakeDame(); sẽ gọi destroy object lúc health về 0, lúc đó DamageEffect vẫn được gọi vì gameobject vẫn còn tồn tại, dù có check != thì object đó bị hủy trong lúc hàm đó đang chạy nên không giải quyết đươc, vì không thể xác định được 
                // lúc này object hủy, nên ta sẽ damageEffect trước TakeDamage, để damagepush được thực hiện trước khi object bị hủy, 
                // 1 bài test gọi thông qua event CallDamagePushEfectEvent(); thì không bị lỗi vì call event có lẽ được gọi chậm hơn gọi trước tiếp, nên object enemy đã bị hủy callevent mới được gọi, mà object đã hủy rồi nên sub không được gọi(ví dụ call pusheffeft đc gọi 5 lần còn callevent đc gọi 4 lần)
                collision.GetComponent<Enemy>().damageEfect.DamagePushEfect(false);
                collision.GetComponent<Enemy>().health.TakeDamage(damage);
                collision.GetComponent<Enemy>().damageEfect.CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponents<SpriteRenderer>());
                // damagePushEfectEvent.CallDamagePushEfectEvent();
            }
        }
        else if (!isDealDamage && hasAttack)
        {
            hasAttack = false;
        }
    }
}
