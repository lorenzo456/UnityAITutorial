using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    /*
     * Below we have the actual state machine of the base class of the enemy
     */
    public enum State { Idle, Attack } //These are the states the enemy has.     
    protected State currentState; //The current state the enemy is in, it is protected because we don't want any other script other than the inherited members of this class to access this variable.
    
    /*
     * Below we make variables for the player and the agent.
     */
    protected NavMeshAgent agent; // A NavMeshAgent Component allows us to make use the Unity AI features. 
    public GameObject player; // We're getting the player because we want our agent to know some information about the player

    /*
     * Below we make varibles about some information of the environment
     */
    public GameObject area; // This is the area our agent will be in, every area has x amount of waypoints as children. We are filling this with the Waypontholder gameobject that is a child of the area the agent is in. 
    public List<GameObject> waypoints = new List<GameObject>(); // here we make a list of waypoint the agent knows about.
    public GameObject currentWaypoint; //This is the current waypoint of the agent.
    public float offsetToWaypoint; //The offset an agent can have when comparing the agent position and waypoint position.
    protected bool isWalkingToWaypoint; //Shows if the agent is currently walking the a waypoint or not.
    
    /*
     * Below are some stats of the Enemy
     */
    public float speed = 5; //This is the speed the agent walks
    public int maxhealth; //This is the maximum health the agent may have
    public int damageGiven; //This is the damage the agent gives to another GameObject when attacking
    protected int currentHealth; //This is the current health of the agent.
    public float attackDistance; //This is the attack range the agent has.


    /* 
     * Here we are initializing some variables
     * virtual Functions can be overridden by the classes that inherit them, this is handy 
     * if a class wants to either add more to this "base" function or completely change the 
     * the function altogether.
     */
    public virtual void Start ()
    {
        currentHealth = maxhealth; //here we set current health to max health.
        currentState = State.Idle; // Here we are setting the current state of our agent as idle state
        agent = gameObject.GetComponent<NavMeshAgent>(); // here we are setting the agent to the NavMeshAgent component of Unity (Which we've added in the inspector)
        agent.speed = speed; // here we are setting the agent speed (unity built in variable) equal to the speed we set earlier.

        /*Here we're looping through the children of the area our agent is in and adding all
         * of the child objects as waypoint the agent can go to.
         */
        for(int i = 0; i < area.transform.childCount; i++)
        {
            waypoints.Add(area.transform.GetChild(i).gameObject);
        }
	}

    //The update happens ~60 times a second
    public virtual void  Update()
    {
        //Here we constantly check what state our agent is in.
        switch (currentState)
        {
            //if the agent is in Idle state call the following method(s)
            case State.Idle:
                print("WALk");
                Move();
                break;
            //if the agent is in the attack state call the following method(s)
            case State.Attack:
                print("Attack");
                Attack();
                break;
        }
    }

    //If the agent is hit call this method, the parameter we're getting is the damage our agent is recieving
    public virtual void OnHit(int damageTaken)
    {
        //When this function is called 
        currentHealth -= damageTaken;

        //If the current health is equal or lower than 0 then call the Death() function
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    /*
     * The following methods are empty, but they are virtual meaning that the classes that inherit from this automatically have them and can override them
     */
    public virtual void Attack()
    {

    }

    public virtual void Death()
    {

    }

    public virtual void Move()
    {

    }
}
