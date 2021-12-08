using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : NPC
{
    [SerializeField] Transform hand = null;
    public IPickable GrabbedObject { get; private set; } = null;
    bool canGrab = true;

    protected override void Update()
    {
        base.Update();

        // If the character can grab an object and doesn't already have one
        if (GrabbedObject == null && canGrab)
            TryGrab();
    }

    /// <summary>
    /// Try to grab any near IPickable
    /// </summary>
    private void TryGrab()
    {
        // Create an array with all the RaycastHit around the character
        RaycastHit[] _hits;
        _hits = Physics.SphereCastAll(transform.position, 3, Vector3.up);

        // Loop through every RaycastHit found
        foreach (RaycastHit _hit in _hits)
        {
            // If an IPickable has been found
            if (_hit.transform && _hit.transform.GetComponent<IPickable>() != null)
            {
                // Changes the agent destination to move him towards the object
                ChangeDestination(_hit.transform.position);
                // If the IPickable is close enough
                if (Vector3.Distance(_hit.transform.position, transform.position) <= 1)
                {
                    // Picks it up
                    GrabbedObject = _hit.transform.GetComponent<IPickable>();
                    GrabbedObject.PickUp(hand);
                    // Start a Coroutine to drop the object
                    StartCoroutine(WaitDrop());
                }
                break;
            }
        }
    }

    /// <summary>
    /// Coroutine to delay the drop of the object
    /// </summary>
    IEnumerator WaitDrop()
    {
        // Set canGrab to true
        canGrab = false;
        // Wait a random time
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        // Drop the object and reset the GrabbedObject field
        GrabbedObject.Drop();
        GrabbedObject = null;
        // Wait 10 seconds
        yield return new WaitForSeconds(10);
        // Reset canGrab to true
        canGrab = true;
    }
}
