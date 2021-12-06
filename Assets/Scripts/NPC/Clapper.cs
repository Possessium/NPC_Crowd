using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clapper : NPC
{
    private readonly int hash_Clapping = Animator.StringToHash("Clapping");

    [SerializeField] private float radius = 2;

    public bool IsClapping { get { return GetPickerNear(); } }

    protected override void Update()
    {
        base.Update();

        animator.SetBool(hash_Clapping, IsClapping);

        agent.isStopped = IsClapping;
    }

    bool GetPickerNear()
    {
        RaycastHit[] _hits;
        if((_hits = Physics.SphereCastAll(transform.position, radius, Vector3.up)).Length > 0)
        {
            foreach (RaycastHit _hit in _hits)
            {
                if (_hit.transform.GetComponent<Picker>() && _hit.transform.GetComponent<Picker>().GrabbedObject != null)
                    return true;
            }
        }
        return false;
    }

}
