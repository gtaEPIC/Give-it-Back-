using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireMap : MonoBehaviour
{
    public float attackRadius;
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRadius);
    }
}
