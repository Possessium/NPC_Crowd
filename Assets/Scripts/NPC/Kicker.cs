using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : NPC
{
    protected override void Update()
    {

        RaycastHit[] _hits;
        _hits = Physics.SphereCastAll(transform.position, 3, Vector3.up);

        if (_hits.Length > 0)
        {
            foreach (RaycastHit _hit in _hits)
            {
                if (_hit.transform && _hit.transform.GetComponent<IKickable>() != null && _hit.transform.GetComponent<IKickable>().Physic.velocity.magnitude < .1f)
                {
                    agent.destination = _hit.transform.position;
                    targetPoint = _hit.transform.position;
                    if(Vector3.Distance(_hit.transform.position, transform.position) <= 1)
                    {
                        _hit.transform.GetComponent<IKickable>().Kick(transform);
                        Move();
                    }
                    break;
                }
            }
        }
        base.Update();

    }

}
