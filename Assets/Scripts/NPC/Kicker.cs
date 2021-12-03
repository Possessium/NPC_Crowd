using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : NPC
{
    protected override void Update()
    {
        base.Update();


        RaycastHit[] _hits;
        _hits = Physics.SphereCastAll(transform.position, 1, Vector3.up);

        if (_hits.Length > 0)
        {
            foreach (RaycastHit _hit in _hits)
            {
                if (_hit.transform && _hit.transform.GetComponent<IKickable>() != null)
                {
                    _hit.transform.GetComponent<IKickable>().Kick(transform);
                    break;
                }
            }
        }
    }

}
