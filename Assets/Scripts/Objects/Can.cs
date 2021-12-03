using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour, IPickable, IKickable
{
    [SerializeField] private Rigidbody physics = null;
    [SerializeField] private Collider hitbox = null;

    public void Kick(Transform _from)
    {
        physics.AddForce((transform.position - _from.position) * 5, ForceMode.Impulse);
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
