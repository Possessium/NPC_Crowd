using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, ISeat
{
    [SerializeField] private Transform buttPosition = null;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(buttPosition.position, .2f);
    }

    public Vector3 Sit()
    {
        return buttPosition.position;
    }
}
