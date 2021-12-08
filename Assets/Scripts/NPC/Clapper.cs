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

        // Allow the agent to move or not based on the IsClapping bool
        agent.isStopped = IsClapping;
    }

    /// <summary>
    /// Set the correct values of the animator based on the character movement
    /// </summary>
    private protected override void Animate()
    {
        base.Animate();

        // Add the clap animation for this NPC only
        animator.SetBool(hash_Clapping, IsClapping);
    }

    /// <summary>
    /// Search for a picker around itself
    /// </summary>
    /// <returns>true if found any</returns>
    private bool GetPickerNear()
    {
        // Create an array with all the RaycastHit around the character
        RaycastHit[] _hits;
        _hits = Physics.SphereCastAll(transform.position, radius, Vector3.up);

        // Loop through every RaycastHit found
        foreach (RaycastHit _hit in _hits)
        {
            // If there is a Picker around and he hold an object return true
            if (_hit.transform.GetComponent<Picker>() && _hit.transform.GetComponent<Picker>().GrabbedObject != null)
                return true;
        }
        // If no object or correct Picker has been found, return false
        return false;
    }

}
