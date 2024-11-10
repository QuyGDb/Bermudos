using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
public class Item : MonoBehaviour
{
    private float timeToDestroy = 10f;
    private LayerMask playerLayerMark;
    public ItemSO itemSO;
    private Tween itemTween;
    public float jumDuration = 0.5f;
    public int numJumps = 1;
    public float jumpPower = 1f;
    public float shakeDuration = 3f;
    public float shakeStrength = 0.1f;
    public int shakeVibrato = 2;
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
        itemTween.Kill();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayerMark.value & 1 << collision.gameObject.layer) > 0)
        {
            collision.GetComponent<InventoryManager>()?.CollectIntentoryItem(this.itemSO);
            Destroy(this.gameObject);
        }
    }

    public void DropItemEffect()
    {
        Vector3 direction = (transform.position - GameManager.Instance.player.transform.position).normalized;
        Vector3 targetPosition = transform.position + direction * 2f;
        itemTween = transform.DOJump(targetPosition, jumpPower, numJumps, jumDuration).OnComplete(() =>
        {
            transform.DOShakePosition(shakeDuration, new Vector3(0, shakeStrength, 0), shakeVibrato, randomness: 0, fadeOut: true).SetLoops(-1);
        });
    }
}
