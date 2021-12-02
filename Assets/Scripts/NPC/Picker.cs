using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : NPC
{
    [SerializeField] Transform hand = null;
    private IPickable grabbedObject = null;
    bool canGrab = true;

    protected override void Update()
    {
        base.Update();

        if(grabbedObject == null && canGrab)
        {
            RaycastHit[] _hits;
            _hits = Physics.SphereCastAll(transform.position, 1, Vector3.up);

            if (_hits.Length > 0)
            {
                foreach (RaycastHit _hit in _hits)
                {
                    if (_hit.transform && _hit.transform.GetComponent<IPickable>() != null)
                    {
                        grabbedObject = _hit.transform.GetComponent<IPickable>();
                        grabbedObject.PickUp(hand);
                        StartCoroutine(WaitDrop());
                        break;
                    }
                }
            }
        }
    }

    IEnumerator WaitDrop()
    {
        canGrab = false;
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        grabbedObject.Drop();
        grabbedObject = null;
        yield return new WaitForSeconds(10);
        canGrab = true;
    }
}
