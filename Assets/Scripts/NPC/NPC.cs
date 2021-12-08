using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    /*[SerializeField]*/ private protected NavMeshAgent agent = null;
    /*[SerializeField]*/ private protected Animator animator = null;
    [SerializeField] private float randomPointRange = 10.0f;
    private readonly int hash_Move = Animator.StringToHash("Distance");

    private bool wait = false;

    private protected Vector3 targetPoint = Vector3.zero;


    protected virtual void Start()
    {
        // Populates fields
        TryGetComponent(out agent);
        TryGetComponent(out animator);

        // Get first destination
        Move();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(targetPoint, .2f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPoint);
    }

    protected virtual void Update()
    {
        // If isn't in wait mode and near it's destination, start the DelayMoveCoroutine
        if (!wait && Vector3.Distance(transform.position, agent.destination) < .1f)
            StartCoroutine(DelayMove());

        // Animate the character
        Animate();
    }

    /// <summary>
    /// Set the correct values of the animator based on the character movement
    /// </summary>
    private protected virtual void Animate() => animator.SetFloat(hash_Move, Mathf.MoveTowards(animator.GetFloat(hash_Move), Vector3.Distance(transform.position, agent.destination), Time.deltaTime * 2));

    /// <summary>
    /// Try to find a random point in the range of the character and placed on the navmesh
    /// </summary>
    /// <param name="_center">Vector3 center of the sphere</param>
    /// <param name="_range">float range of the sphere</param>
    /// <param name="_result">out Vector3 point found</param>
    /// <returns>true if a point has been found</returns>
    private bool GetRandomPoint(Vector3 _center, float _range, out Vector3 _result)
    {
        // Iterated 30 times
        for (int i = 0; i < 30; i++)
        {
            // Get a point random in a sphere around the _center with the _range
            Vector3 _randomPoint = _center + Random.insideUnitSphere * _range;
            NavMeshHit _hit;

            // If the navmesh have a position near the random point found
            if (NavMesh.SamplePosition(_randomPoint, out _hit, 1.0f, NavMesh.AllAreas))
            {
                // Calculate a path from the character to the navmesh point
                NavMeshPath navMeshPath = new NavMeshPath();
                _result = _hit.position;
                agent.CalculatePath(_result, navMeshPath);
                // If the path is completed return true
                if (navMeshPath.status == NavMeshPathStatus.PathComplete)
                    return true;
            }
        }
        // If no point with correct path has been found resets the _result parameter and return false
        _result = Vector3.zero;
        return false;
    }

    /// <summary>
    /// Method to get the agent a new destination
    /// </summary>
    private protected void Move()
    {
        // Try to get a random point on the navmesh
        if (GetRandomPoint(transform.position, randomPointRange, out targetPoint))
        {
            // If found set it to the agent destination
            ChangeDestination(targetPoint);
        }
        // If none found, try again
        else
            Move();
    }

    /// <summary>
    /// Coroutine to put a delay for the reset of the wait bool
    /// </summary>
    IEnumerator DelayMove()
    {
        // Set the wait bool to true
        wait = true;
        // Wait a random time
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        // Gets a new point and set wait to false
        Move();
        wait = false;
    }

    /// <summary>
    /// Changes the destination of the agent and the targetPoint field
    /// </summary>
    /// <param name="_d">Vector3 the new destination</param>
    private protected void ChangeDestination(Vector3 _d)
    {
        targetPoint = _d;
        agent.SetDestination(_d);
    }
}
