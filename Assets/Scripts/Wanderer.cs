using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : NPC, IWanderer
{
    private bool wait = false;

    protected override void Update()
    {
        base.Update();

        if (!wait && Vector3.Distance(transform.position, agent.destination) < .1f)
            StartCoroutine(DelayMove());
    }

    public IEnumerator DelayMove()
    {
        wait = true;
        yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(Random.Range(3f, 7f));
        Move();
        wait = false;
    }

}
