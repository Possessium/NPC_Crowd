using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IKickable
{
    [SerializeField] private Rigidbody physics = null;

    public Rigidbody Physic { get { return physics; } }

    /// <summary>
    /// Add an impulse force from the given transform
    /// </summary>
    /// <param name="_from">Where the force comes</param>
    public void Kick(Transform _from)
    {
        // Add a force in the direction of the _from to transform in Impulse mode
        physics.AddForce((transform.position - _from.position) * 5, ForceMode.Impulse);
        // Clamp the velocity so the ball doesn't go mach 10
        physics.velocity = new Vector3(Mathf.Clamp(physics.velocity.x, 0f, 5f), Mathf.Clamp(physics.velocity.y, 0f, 5f), Mathf.Clamp(physics.velocity.z, 0f, 5f));
    }
}
