using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour, IPickable, IKickable
{
    [SerializeField] private Rigidbody physics = null;
    public Rigidbody Physic { get { return physics; } }

    [SerializeField] private Collider hitbox = null;

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

    /// <summary>
    /// Drop the object from it's parent
    /// </summary>
    public void Drop()
    {
        // Remove the object from it's parent
        transform.parent = null;
        // Activate the hitbox and gravity of the object
        hitbox.enabled = true;
        physics.useGravity = true;
    }

    /// <summary>
    /// Pickup the object to the parent
    /// </summary>
    /// <param name="_parent">The parent given to this object</param>
    public void PickUp(Transform _parent)
    {
        // Set the position to the parent
        transform.position = _parent.position;
        // Set the parent
        transform.parent = _parent;
        // Disable the gravity and hitbox aswell as resetting the velocity
        physics.velocity = Vector3.zero;
        physics.useGravity = false;
        hitbox.enabled = false;
    }
}
