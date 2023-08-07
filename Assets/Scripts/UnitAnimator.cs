using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform BulletProjectilePrefab;
    [SerializeField] private Transform ShootPointTransform;

    private void Awake()
    {
        if (TryGetComponent<MoveAction>(out var moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }

        if (TryGetComponent<ShootAction>(out var shootAction))
        {
            shootAction.OnShoot += ShootAction_OnShoot;
        }
    }

    private void ShootAction_OnShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        animator.SetTrigger("Shoot");

        var bulletProjectileTransform = Instantiate(BulletProjectilePrefab, ShootPointTransform.position, Quaternion.identity);

        var bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

        Vector3 targetUnitShootAtPostion = e.TargetUnit.GetWorldPosition();

        targetUnitShootAtPostion.y = ShootPointTransform.position.y;

        bulletProjectile.Setup(targetUnitShootAtPostion);
    }

    private void MoveAction_OnStopMoving(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", false);
    }

    private void MoveAction_OnStartMoving(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", true);
    }
}
