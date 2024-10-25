using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoss : MonoBehaviour
{
    private Animator animator;
    public Transform shootPosition;
    public AmmoDetailsSO phase1Ammo;
    float offset = 90;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Shoot(AmmoDetailsSO ammoDetailsSO, Vector3 shootPosition, Vector3 target)
    {
        // Shoot
        IFireable fireable = (IFireable)PoolManager.Instance.ReuseComponent(ammoDetailsSO.ammoPrefab, shootPosition, Quaternion.identity);
        fireable?.InitialiseAmmo(ammoDetailsSO, target);

    }

    #region Base 
    [Header("BASE")]

    public Vector3[] shootPositions;
    public void BaseShoot()
    {
        for (int i = 0; i < shootPositions.Length; i++)
        {

            Vector3 target = transform.position + shootPositions[i];
            Shoot(phase1Ammo, transform.position, target);
        }
    }
    #endregion

    #region eyeAttack

    [Header("EYE ATTACK")]
    public GameObject Beam;

    public void EyeAttackEnd()
    {

        if (transform.childCount > 1)
            Destroy(transform.GetChild(1).gameObject);
    }

    public void EyeAttack()
    {
        var direction = GameManager.Instance.player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle + offset);
        GameObject beam = Instantiate(Beam, shootPosition.position, rotation);
        beam.transform.parent = this.transform;
    }

    #endregion

    #region eyeLoop
    [Header("EYE LOOP")]
    private float eyeLoopRate = 0.5f;
    private float eyeLoopDuration = 5f;
    public void StartEyeLoop()
    {
        StartCoroutine(EyeLoop());
    }
    public IEnumerator EyeLoop()
    {
        while (eyeLoopDuration < 0f)
        {
            EyeAttack();
            eyeLoopDuration -= eyeLoopRate;
            yield return new WaitForSeconds(eyeLoopRate);
        }
    }
    #endregion

    #region openMouth
    public float[] offsetArr;
    public void OpenMouth()
    {
        var targetPositionList = GetTargetPositionList();

        foreach (var targetPosition in targetPositionList)
        {
            Debug.Log(targetPosition);
            Shoot(phase1Ammo, shootPosition.position, targetPosition);
        }
    }

    public List<Vector3> GetTargetPositionList()
    {
        var direction = GameManager.Instance.player.transform.position - transform.position;
        var targetPositionList = new List<Vector3>();

        if (Mathf.Abs(direction.y) < Mathf.Abs(direction.x))
        {
            for (int i = 0; i < offsetArr.Length; i++)
            {
                targetPositionList.Add((Vector3)GameManager.Instance.player.GetPlayerPosition() + new Vector3(direction.x, direction.y + offsetArr[i]));
            }
        }
        else if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            for (int i = 0; i < offsetArr.Length; i++)
            {
                targetPositionList.Add((Vector3)GameManager.Instance.player.GetPlayerPosition() + new Vector3(direction.x + offsetArr[i], direction.y));
            }
        }
        return targetPositionList;
    }
    #endregion
}
