using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class RangedSkills : MonoBehaviour
{
    private Animator animator;
    public Transform shootPosition;
    public AmmoDetailsSO ammo;
    public AmmoDetailsSO ammoPhase2;
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
    private Vector2[] Get15ShootDirections()
    {
        Vector2[] shootDirection = new Vector2[15];
        Vector2 startDirection = Vector2.right * 30;
        shootDirection[0] = startDirection;
        for (int i = 1; i < 15; i++)
        {
            shootDirection[i] = Quaternion.Euler(0, 0, 24 * i) * startDirection;
        }
        return shootDirection;
    }
    public void BaseShoot()
    {
        var directions = Get15ShootDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Vector3 target = transform.position + (Vector3)directions[i];
            Shoot(ammo, transform.position, target);
        }
    }
    #endregion

    #region eyeAttack

    [Header("EYE ATTACK")]
    public GameObject Beam;



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

    public void StartEyeLoop(float eyeLoopDuration, float eyeLoopRate)
    {
        StartCoroutine(EyeLoop(eyeLoopDuration, eyeLoopRate));
    }
    public IEnumerator EyeLoop(float eyeLoopDuration, float eyeLoopRate)
    {
        while (eyeLoopDuration > 0f)
        {
            EyeAttack();
            eyeLoopDuration -= eyeLoopRate;
            yield return new WaitForSeconds(eyeLoopRate);
        }
        eyeLoopDuration = 5f;

    }
    #endregion

    #region openMouth
    public float[] offsetArr;
    public void OpenMouth()
    {
        var targetPositionList = GetTargetPositionList();

        foreach (var targetPosition in targetPositionList)
        {
            Shoot(ammo, shootPosition.position, targetPosition);
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

    #region mouthOpenLoop

    public void StartMouthOpenLoopMouth(float mouthOpenLoopRate, float mouthOpenLoopDuration)
    {

        StartCoroutine(MouthOpenLoop(mouthOpenLoopRate, mouthOpenLoopDuration));
    }
    public IEnumerator MouthOpenLoop(float mouthOpenLoopRate, float mouthOpenLoopDuration)
    {
        while (mouthOpenLoopDuration > 0f)
        {
            Shoot(ammo, shootPosition.position, GameManager.Instance.player.transform.position);
            mouthOpenLoopDuration -= mouthOpenLoopRate;
            yield return new WaitForSeconds(mouthOpenLoopRate);
        }
        mouthOpenLoopDuration = 5f;
    }
    #endregion


}
