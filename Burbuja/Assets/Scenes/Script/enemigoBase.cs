using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemigoBase : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform[] wayPoints;
    public int indexWayPoints = 0;
    public float distanceToWayPoint = 2f;

    
    void Start()
    {
       if (agent == null)
       {
        agent = GetComponent<NavMeshAgent>();
       }
    }


    void Update()
    {
        HacerRonda();
    }

    private void HacerRonda()
    {
        agent.SetDestination(wayPoints[indexWayPoints].position);

        if (Vector3.Distance(transform.position, wayPoints[indexWayPoints].position) <= distanceToWayPoint)
        {
            if (indexWayPoints!= wayPoints.Length - 1)
            {
                indexWayPoints ++;
            }
            else
            {
                indexWayPoints = 0;
            }
        }
    }

}
