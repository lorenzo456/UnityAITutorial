using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Here we declare that this class inherits from BaseEnemy. 
 * This means that it includes all the variables and methods 
 * of the BaseEnemy class (including everything that's private)
 */
public class SphereEnemy : BaseEnemy {

    /*The variables declared here are exclusive to this class, BaseEnemy 
     * nor the classes that inherit from it can access it, only this class can.
     */
    bool initialMove = true;
    public SightColliderScript sightCollider;

    public override void Start()
    {
        base.Start();
        sightCollider = GetComponentInChildren<SightColliderScript>();
    }

    public override void Move()
    {
        base.Move();

        if (currentWaypoint != null && Vector3.Distance(gameObject.transform.position, currentWaypoint.transform.position) < offsetToWaypoint && isWalkingToWaypoint || initialMove)
        {
            isWalkingToWaypoint = false;
            initialMove = false;
        }

        if (!isWalkingToWaypoint || currentWaypoint == null)
        {
            currentWaypoint = waypoints[Random.Range(0, waypoints.Count - 1)];
            agent.SetDestination(currentWaypoint.transform.position);
            isWalkingToWaypoint = true;
        }

        if (sightCollider.playerInSight)
        {
            isWalkingToWaypoint = false;
            currentState = State.Attack;
            currentWaypoint = player;
            agent.SetDestination(currentWaypoint.transform.position);
        }
    }

    public override void Attack()
    {
        base.Attack();
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) > attackDistance ||!sightCollider.gameObject.activeSelf)
        {
            sightCollider.playerInSight = false;
            currentState = State.Idle;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sightCollider.playerInSight = false;
            other.gameObject.SetActive(false);
            currentState = State.Idle;
        }
    }
}
