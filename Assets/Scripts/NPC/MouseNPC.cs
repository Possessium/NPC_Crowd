using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseNPC : NPC
{
    protected override void Start()
    {
        // Populates fields
        TryGetComponent(out agent);
        TryGetComponent(out animator);
    }

    protected override void Update()
    {
        // No base.Update() because this NPC changes destination with the mouse input instead of random delay

        // Animate the character
        Animate();

        // When the left click is pressed the npc try to change it's position
        if (Input.GetKey(KeyCode.Mouse0))
            GetNavMeshPosition(Input.mousePosition);
    }

    /// <summary>
    /// Try to get a navmeshposition based on the mouse position
    /// </summary>
    /// <param name="_mousePosition">current mouse position</param>
    /// <returns>Vector3 next agent destination</returns>
    private void GetNavMeshPosition(Vector3 _mousePosition)
    {
        // Set default fields
        Vector3 _result = agent.destination;
        NavMeshHit _hit;
        RaycastHit _physicsHit;
        // If something is found below the mouse position
        if(Physics.Raycast(Camera.main.ScreenPointToRay(_mousePosition), out _physicsHit))
        {
            // Try to find a point on the navmesh
            if (NavMesh.SamplePosition(_physicsHit.point, out _hit, 1.0f, NavMesh.AllAreas))
            {
                // Calculate a path from the character to the navmesh point
                NavMeshPath navMeshPath = new NavMeshPath();
                _result = _hit.position;
                agent.CalculatePath(_result, navMeshPath);
                // If the path is completed change the destination to _result
                if (navMeshPath.status == NavMeshPathStatus.PathComplete)
                    ChangeDestination(_result);
            }
        }
    }
}
