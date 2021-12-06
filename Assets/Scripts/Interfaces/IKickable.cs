using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKickable
{
    public Rigidbody Physic { get; }
    public void Kick(Transform _from);



}
