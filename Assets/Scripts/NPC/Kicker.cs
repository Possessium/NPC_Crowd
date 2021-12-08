using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : NPC
{
    protected override void Update()
    {
        // Create an array with all the RaycastHit around the character
        RaycastHit[] _hits;
        _hits = Physics.SphereCastAll(transform.position, 3, Vector3.up);

        // Loop through the entire array
        foreach (RaycastHit _hit in _hits)
        {
            // If there is an IKickable object and it's velocity is small enough
            if (_hit.transform && _hit.transform.GetComponent<IKickable>() != null && _hit.transform.GetComponent<IKickable>().Physic.velocity.magnitude < .1f)
            {
                // Changes the agent destination to move him towards the object
                ChangeDestination(_hit.transform.position);
                // If the distance is small enough
                if (Vector3.Distance(_hit.transform.position, transform.position) <= 1)
                {
                    // Kick the object
                    _hit.transform.GetComponent<IKickable>().Kick(transform);
                    // Find a new destination
                    Move();
                }
                break;
            }
        }

        base.Update();

    }

}
