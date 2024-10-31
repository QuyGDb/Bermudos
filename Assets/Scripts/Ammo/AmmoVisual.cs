using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AmmoVisual : MonoBehaviour
{
    //[SerializeField] private Transform ammoShadow;
    private Ammo ammo;

    private Vector2 target;
    //private Vector2 trajectoryStartPosition;
    //private float shadowPositionYDivider = 6f;
    private void Awake()
    {
        ammo = GetComponent<Ammo>();
    }

    private void Start()
    {
        // trajectoryStartPosition = transform.position;
    }
    private void Update()
    {
        UpdateAmmoRotation();

        //UpdateAmmoShadowPosition();
    }

    private void UpdateAmmoRotation()
    {
        Vector3 ammoMovedDirection = ammo.GetAmmoMoveDirection();
        float angle = Mathf.Atan2(ammoMovedDirection.y, ammoMovedDirection.x) * Mathf.Rad2Deg;
        ammo.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    //private void UpdateAmmoShadowPosition()
    //{
    //    Vector2 newPosition = transform.position;
    //    newPosition.y = trajectoryStartPosition.y + ammo.GetNextYTrajectoryPosition() / shadowPositionYDivider + ammo.GetNextPositionYCorrectionAbsolute();
    //    ammoShadow.position = newPosition;
    //}
    public void SetTarget(Vector2 target)
    {
        this.target = target;
    }

}
