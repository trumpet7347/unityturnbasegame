using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField] private Transform _radgollPrefab;
    [SerializeField] private Transform _originalRootBone;

    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Transform ragdollTransform = Instantiate(_radgollPrefab, transform.position, transform.rotation);

        var unitRagdoll = ragdollTransform.GetComponent<UnitRagdoll>();

        unitRagdoll.Setup(_originalRootBone);
    }
}
