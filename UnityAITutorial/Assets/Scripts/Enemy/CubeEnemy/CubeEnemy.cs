using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Here we declare that this class inherits from BaseEnemy. 
 * This means that it includes all the variables and methods 
 * of the BaseEnemy class (including everything that's private)
 */
public class CubeEnemy : BaseEnemy {
    /*The variables declared here are exclusive to this class, BaseEnemy 
    * nor the classes that inherit from it can access it, only this class can.
    */
    int waypointCount = -1;
    public GameObject bullet;
    float counter;

    /*
     * Here we're overriding the Move() method of BaseEnemy
     * We check if the agent is at a waypoint.
     * if the agent is at a waypoint, select the following waypoint as current waypoint
     * if the agent is not at a waypoint (or close enough), set the destination of the agent to current waypoint 
     * SetDestination() is a Unity method to move an agent from one location to the next.
     */
    public override void Move()
    {
        //base means that we'll be adding to the existing Move() method.
        base.Move();
        //agent is close enough to the current waypoint.
        if (currentWaypoint != null && Vector3.Distance(gameObject.transform.position, currentWaypoint.transform.position) < offsetToWaypoint && isWalkingToWaypoint)
        {
            isWalkingToWaypoint = false;
        }

        //If Agent is on a waypoint, find the next waypoint in the array and set it to current waypoint and agent.destination
        if (!isWalkingToWaypoint)
        {
            if(waypointCount < waypoints.Count -1)
            {
                waypointCount++;
            }
            else
            {
                waypointCount = 0;
            }
            Debug.Log(waypointCount);
            currentWaypoint = waypoints[waypointCount];
            agent.SetDestination(currentWaypoint.transform.position);
            isWalkingToWaypoint = true;
        }

        //If the player's distance to the agent is smaller than attackDistance. Agent goes to attack state.
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) < attackDistance && player.gameObject.activeSelf)
        {
            Debug.Log("GOTOATTACK");
            isWalkingToWaypoint = false;
            currentState = State.Attack;
            currentWaypoint = null;
            agent.SetDestination(gameObject.transform.position);
        }

    }

    public override void Attack()
    {
        //base means that we'll be adding to the existing Attack() method.
        base.Attack();
        //rotate towards the player
        var lookPos = player.transform.position - gameObject.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

        //instatiate a bullet every second.
        if(Time.time > counter)
        {
            counter = Time.time + 1;
            Instantiate(bullet, transform.position, rotation);
        }

        //if player is dead then return to idle state
        if (!player.gameObject.activeSelf)
        {
            currentState = State.Idle;
            isWalkingToWaypoint = false;
        }
    }
}
