using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private protected NavMeshAgent agent = null;
    [SerializeField] private float randomPointRange = 10.0f;
    [SerializeField] private Animator animator = null;
    private readonly int hash_Move = Animator.StringToHash("Distance");

    private bool wait = false;

    private protected Vector3 targetPoint = Vector3.zero;


    private protected void Start()
    {
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(targetPoint, .2f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPoint);
    }

    protected virtual void Update()
    {
        if (!wait && Vector3.Distance(transform.position, agent.destination) < .1f)
            StartCoroutine(DelayMove());

        animator.SetFloat(hash_Move, Mathf.MoveTowards(animator.GetFloat(hash_Move), Vector3.Distance(transform.position, agent.destination), Time.deltaTime * 2));
    }


    private bool GetRandomPoint(Vector3 _center, float _range, out Vector3 _result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 _randomPoint = _center + Random.insideUnitSphere * _range;
            NavMeshHit _hit;
            if (NavMesh.SamplePosition(_randomPoint, out _hit, 1.0f, NavMesh.AllAreas))
            {
                NavMeshPath navMeshPath = new NavMeshPath();
                _result = _hit.position;
                agent.CalculatePath(_result, navMeshPath);
                if (navMeshPath.status == NavMeshPathStatus.PathComplete)
                    return true;
            }
        }
        _result = Vector3.zero;
        return false;
    }

    private void Move()
    {
        if (GetRandomPoint(transform.position, randomPointRange, out targetPoint))
        {
            agent.SetDestination(targetPoint);
        }
        else
            Move();
    }

    IEnumerator DelayMove()
    {
        wait = true;
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        Move();
        wait = false;
    }
}
