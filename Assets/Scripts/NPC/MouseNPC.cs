using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseNPC : NPC
{
    protected override void Start()
    {
        //
    }

    protected override void Update()
    {
        Animate();

        if (Input.GetKey(KeyCode.Mouse0))
            agent.SetDestination(GetNavMeshPosition(Input.mousePosition));
    }

    //[SerializeField] Vector3 _deb = Vector3.zero;

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(_deb, .5f);
    //}

    private Vector3 GetNavMeshPosition(Vector3 _mousePosition)
    {
        Vector3 _result = agent.destination;
        NavMeshHit _hit;
        RaycastHit _physicsHit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(_mousePosition), out _physicsHit))
        {
            if (NavMesh.SamplePosition(_physicsHit.point, out _hit, 1.0f, NavMesh.AllAreas))
            {
                NavMeshPath navMeshPath = new NavMeshPath();
                _result = _hit.position;
                agent.CalculatePath(_result, navMeshPath);
                if (navMeshPath.status == NavMeshPathStatus.PathComplete)
                    return _result;
            }
        }
        return agent.destination;
    }
}
