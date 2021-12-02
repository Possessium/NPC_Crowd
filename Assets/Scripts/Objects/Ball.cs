using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IKickable
{
    [SerializeField] private Rigidbody physics = null;

    public void Kick(Transform _from)
    {
        physics.AddForce((transform.position - _from.position) * 5, ForceMode.Impulse);
    }
}
