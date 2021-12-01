using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallKicker : NPC, IWanderer
{
    [SerializeField] private LayerMask layerBall = 0;

    private bool wait = false;
    public IEnumerator DelayMove()
    {
        wait = true;
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        Move();
        wait = false;
    }

    protected override void Update()
    {
        base.Update();

        if (!wait && transform.position == agent.destination)
            StartCoroutine(DelayMove());

        RaycastHit _hit;
        if (Physics.SphereCast(transform.position, 1, Vector3.up, out _hit, layerBall))
        {
            if(_hit.transform && _hit.transform.GetComponent<IInteractableNPC>() != null)
                _hit.transform.GetComponent<IInteractableNPC>().Interact(transform);
        }
    }

}
