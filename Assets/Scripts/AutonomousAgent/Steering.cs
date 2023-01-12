using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering
{
    public static Vector3 Seek(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (target.transform.position - agent.transform.position));
        return force;
    }
    
    public static Vector3 Flee(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (agent.transform.position - target.transform.position));
        return force;
    }

    public static Vector3 Wander(AutonomousAgent agent)
    {
        // randomly adjust angle +/- displacement 
        agent.wanderAngle = agent.wanderAngle + Random.Range(-agent.wanderDisplacement, agent.wanderDisplacement);
        // create rotation quaternion around y-axis (up) 
        Quaternion rotation = Quaternion.AngleAxis(agent.wanderAngle, Vector3.up);
        // calculate point on circle radius 
        Vector3 point = rotation * (Vector3.forward * agent.wanderRadius);
        // set point in front of agent at distance length 
        Vector3 forward = agent.transform.forward * agent.wanderDistance;

        Debug.DrawLine(agent.transform.position, forward + point, Color.red);

        Vector3 force = CalculateSteering(agent, forward + point);

        return force;
    }

    public static Vector3 Cohesion(AutonomousAgent agent, GameObject[] neighbors)
    {
        //get center of neighbors
        Vector3 center = Vector3.zero;
        foreach(GameObject neighbor in neighbors) 
        {
            center += neighbor.transform.position;
        }
        center /= neighbors.Length;

        //steer toward center of position of neighbors
        Vector3 direction = (center - agent.transform.position);

        //steer towards center
        Vector3 force = CalculateSteering(agent, direction);
        
        return force;
    }

    public static Vector3 Separation(AutonomousAgent agent, GameObject[] neighbors, float radius)
    {
        return Vector3.zero;
    }

    public static Vector3 Alignment(Agent agent, GameObject[] neighbors) 
    {
        return Vector3.zero;
    }

    public static Vector3 CalculateSteering(Agent agent, Vector3 direction)
    {
        Vector3 desired = direction.normalized * agent.movement.maxSpeed;
        Vector3 steer = desired - agent.movement.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.movement.maxForce);

        return force;
    }
}
