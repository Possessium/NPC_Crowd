using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour, IPickable, IKickable
{
    [SerializeField] private Rigidbody physics = null;
    public Rigidbody Physic { get { return physics; } }

    [SerializeField] private Collider hitbox = null;

    public void Kick(Transform _from)
    {
        physics.AddForce((transform.position - _from.position) * 5, ForceMode.Impulse);
        physics.velocity = new Vector3(Mathf.Clamp(physics.velocity.x, 0f, 5f), Mathf.Clamp(physics.velocity.y, 0f, 5f), Mathf.Clamp(physics.velocity.z, 0f, 5f));
    }

    public void Drop()
    {
        transform.parent = null;
        hitbox.enabled = true;
        physics.useGravity = true;
    }

    public void PickUp(Transform _parent)
    {
        transform.position = _parent.position;
        transform.parent = _parent;
        physics.velocity = Vector3.zero;
        physics.useGravity = false;
        hitbox.enabled = false;
    }
}
