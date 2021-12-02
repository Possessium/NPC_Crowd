using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : NPC
{
    [SerializeField] private LayerMask layerBall = 0;

    protected override void Update()
    {
        base.Update();


        RaycastHit _hit;
        if (Physics.SphereCast(transform.position, 1, Vector3.up, out _hit, layerBall))
        {
            if(_hit.transform && _hit.transform.GetComponent<IKickable>() != null)
                _hit.transform.GetComponent<IKickable>().Kick(transform);
        }
    }

}
