using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitter : NPC
{
    private bool sitting = false;

    private readonly int hash_Sit = Animator.StringToHash("Sit");
    private readonly int hash_Stand = Animator.StringToHash("Stand");

    protected override void Update()
    {
        base.Update();

        if(!sitting)
        {
            RaycastHit[] _hits;
            _hits = Physics.SphereCastAll(transform.position, 1, Vector3.up);

            if (_hits.Length > 0)
            {
                foreach (RaycastHit _hit in _hits)
                {
                    if (_hit.transform && _hit.transform.GetComponent<ISeat>() != null)
                    {
                        Sit(_hit.transform.GetComponent<ISeat>().Sit());
                        break;
                    }
                }
            }
        }
    }

    private void Sit(Vector3 _position)
    {
        agent.isStopped = true;
        sitting = true;
        animator.SetTrigger(hash_Sit);
        transform.rotation = Quaternion.LookRotation(-_position);
        transform.position = _position + transform.forward;
        StartCoroutine(DelaySit());
    }

    private IEnumerator DelaySit()
    {
        yield return new WaitForSeconds(5);
        animator.SetTrigger(hash_Stand);
        yield return new WaitForSeconds(2);
        agent.isStopped = false;
        Move();
        yield return new WaitForSeconds(5);
        sitting = false;
    }
}
