using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class BallAgents : Agent
{
    private Rigidbody ballrigidbody;
    public Transform target;
    public float moveForce = 10f;
    private bool targetEaton = false;
    private bool dead = false;

    private void Awake()
    {
        ballrigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dead"))
        {
            dead = true;
        }
        else if (other.CompareTag("goal"))
        {
            targetEaton = true;
        }
    }

    void ResetTarget()
    {
        targetEaton = false;
        Vector3 randompos = new Vector3(Random.Range(-4.5f, 4.5f), 0.5f, Random.Range(-4.5f, 4.5f));
        target.localPosition = randompos;
    }
    public override void AgentReset()
    {
        Vector3 randompos = new Vector3(Random.Range(-4.5f, 4.5f), 0.5f, Random.Range(-4.5f, 4.5f));
        this.transform.localPosition = randompos;


        dead = false;
        ballrigidbody.velocity = Vector3.zero;
        ResetTarget();
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        AddReward(-0.001f);

        float horizontalInput = vectorAction[0];
        float verticalInput = vectorAction[1];

        ballrigidbody.AddForce(horizontalInput * moveForce, 0f, verticalInput * moveForce);

        if (targetEaton)
        {
            AddReward(1f);
            ResetTarget();
        }
        else if (dead)
        {
            AddReward(-1f);
            Done();
        }
        Monitor.Log(name, GetCumulativeReward(), transform);
    }
    public override void CollectObservations()
    {
        Vector3 distanceToTarget = target.position - this.transform.position;

        AddVectorObs(Mathf.Clamp(distanceToTarget.x / 5f, -1f, 1f));
        AddVectorObs(Mathf.Clamp(distanceToTarget.z / 5f, -1f, 1f));

        Vector3 relatvePos = this.transform.localPosition;

        AddVectorObs(Mathf.Clamp(relatvePos.x / 5f, -1f, 1f));
        AddVectorObs(Mathf.Clamp(relatvePos.z / 5f, -1f, 1f));

        AddVectorObs(Mathf.Clamp(ballrigidbody.velocity.x / 10f, -1f, 1f));
        AddVectorObs(Mathf.Clamp(ballrigidbody.velocity.z / 10f, -1f, 1f));
    }
   
}
