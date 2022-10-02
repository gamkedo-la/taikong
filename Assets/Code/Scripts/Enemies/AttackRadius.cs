using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class AttackRadius : MonoBehaviour
{
    public static event Action<IDamageable> OnAttack;

    private List<IDamageable> Damageables = new List<IDamageable>();
    public int damage = 10;
    public float attackDelay = 0.5f;
    public float minDistance = 5f;
    public delegate void AttackEvent(IDamageable Target);
    private Coroutine AttackCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if(damageable != null)
        {
            Damageables.Add(damageable);

            if(AttackCoroutine == null)
            {
                AttackCoroutine = StartCoroutine(Attack());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Damageables.Remove(damageable);
            if(Damageables.Count == 0)
            {
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
            }
        }
    }

    private IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);

        IDamageable closestDamageable = null;
        float closestDistance = float.MaxValue;

        while(Damageables.Count > 0)
        {
            for (int i = 0; i < Damageables.Count; i++)
            {
                Transform damageableTransform = Damageables[i].GetTransform();
                float distance = Vector3.Distance(transform.position, damageableTransform.position);

                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDamageable = Damageables[i];
                    Debug.Log("Distance of " + closestDamageable + " is " + closestDistance);
                }
            }

            if(closestDamageable != null && closestDistance <= minDistance)
            {
                OnAttack?.Invoke(closestDamageable);
                closestDamageable.ChangeHealth((damage * -1), closestDamageable.GetTransform().gameObject);
            }

            closestDamageable = null;
            closestDistance = float.MaxValue;

            yield return wait;

            Damageables.RemoveAll(DisabledDamageables);
        }

        AttackCoroutine = null;
    }

    private bool DisabledDamageables(IDamageable damageable)
    {
        return damageable != null && !damageable.GetTransform().gameObject.activeSelf;
    }
}
