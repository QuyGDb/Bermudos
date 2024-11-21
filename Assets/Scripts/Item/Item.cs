using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
public class Item : MonoBehaviour
{
    private float timeToDestroy = 10f;
    private LayerMask playerLayerMark;
    public ItemSO itemSO;
    private float jumDuration = 0.5f;
    private int numJumps = 2;
    private float jumpPower = 1f;
    private float shakeDuration = 0.46f;
    private float shakeStrength = 0.5f;
    private int shakeVibrato = 3;
    private bool isColliding = true;
    [SerializeField] private SoundEffectSO dropItemSoundEffect;
    private void Start()
    {
        playerLayerMark.value = LayerMask.GetMask("Player");
        DropItemEffect();
    }
    private void Update()
    {
        if (timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timeToDestroy -= Time.deltaTime;
        }

    }
    private void OnDestroy()
    {
        DOTween.KillAll(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayerMark.value & 1 << collision.gameObject.layer) > 0 && isColliding)
        {
            SoundEffectManager.Instance.PlaySoundEffect(dropItemSoundEffect);
            collision.GetComponent<InventoryManager>()?.CollectIntentoryItem(this.itemSO);
            Destroy(this.gameObject);
            isColliding = false;
        }
    }

    public void DropItemEffect()
    {
        Vector3 direction = (transform.position - GameManager.Instance.player.transform.position).normalized;
        Vector3 targetPosition = transform.position + direction * 2f;
        transform.DOJump(targetPosition, jumpPower, numJumps, jumDuration).OnComplete(() =>
           {
               transform.DOShakePosition(shakeDuration, new Vector3(0, shakeStrength, 0), shakeVibrato, randomness: 0, fadeOut: true).SetLoops(-1);
           });
    }
}
