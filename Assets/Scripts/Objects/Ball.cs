using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IKickable
{
    [SerializeField] private Rigidbody physics = null;

    public Rigidbody Physic { get { return physics; } }

    public void Kick(Transform _from)
    {
        physics.AddForce((transform.position - _from.position) * 5, ForceMode.Impulse);
        physics.velocity = new Vector3(Mathf.Clamp(physics.velocity.x, 0f, 5f), Mathf.Clamp(physics.velocity.y, 0f, 5f), Mathf.Clamp(physics.velocity.z, 0f, 5f));
    }
}
