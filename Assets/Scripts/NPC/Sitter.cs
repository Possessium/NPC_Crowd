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

        // If the character isn't already sitting, look around to find a seat
        if (!sitting)
            TrySit();
    }

    /// <summary>
    /// Try to sit in a near seat
    /// </summary>
    private void TrySit()
    {
        // Create an array with all the RaycastHit around the character
        RaycastHit[] _hits;
        _hits = Physics.SphereCastAll(transform.position, 1, Vector3.up);

        // Loop through every RaycastHit found
        foreach (RaycastHit _hit in _hits)
        {
            // If an ISeat has been found
            if (_hit.transform && _hit.transform.GetComponent<ISeat>() != null)
            {
                // Sit at the correct given position
                Sit(_hit.transform.GetComponent<ISeat>().Sit());
                break;
            }
        }
    }

    /// <summary>
    /// Set the character correctly and sit it
    /// </summary>
    /// <param name="_position">Given position of the seat</param>
    private void Sit(Vector3 _position)
    {
        // Stop the agent movement
        agent.isStopped = true;
        // Set the sitting bool to true
        sitting = true;
        // Start the sit animation
        animator.SetTrigger(hash_Sit);
        // Rotate the character so it is back to the seat
        transform.rotation = Quaternion.LookRotation(-_position);
        // Set the character position in front of the seat so it doesn't go behind
        transform.position = _position + transform.forward;
        // Start the DelaySit Coroutine
        StartCoroutine(DelaySit());
    }

    /// <summary>
    /// Coroutine to delay the stand up animation after a sit
    /// </summary>
    private IEnumerator DelaySit()
    {
        // Wait 5 seconds
        yield return new WaitForSeconds(5);
        // Start the stand animation
        animator.SetTrigger(hash_Stand);
        // Wait 2 seconds (the duration of the animation)
        yield return new WaitForSeconds(2);
        // Start the agent movement and get it a new destination
        agent.isStopped = false;
        Move();
        // Wait 5 seconds
        yield return new WaitForSeconds(5);
        // Reset sitting bool to false
        sitting = false;
    }
}
