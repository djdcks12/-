using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class BallAgent : Agent
{
    private Rigidbody ballrigidbody;

    public Transform target;

    public float moveForce = 10f;

    private bool targetEaten = false;
    private bool dead = false;

    void Awake()
    {
        ballrigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dead"))
        {
            dead = true;
        }
        else if (other.CompareTag("goal"))
        {
            targetEaten = true;
        }
    }

    void ResetTarget()
    {
        targetEaten = false;
        Vector3 randompos = new Vector3(Random.Range(-4.5f, 4.5f),0.5f, Random.Range(-4.5f, 4.5f));
        target.localPosition = randompos;
    }

    public override void AgentReset()
    {
        Vector3 randompos = new Vector3(Random.Range(-4.5f, 4.5f), 0.5f, Random.Range(-4.5f, 4.5f));
        transform.localPosition = randompos;

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

        if (targetEaten)
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
        Vector3 distanceToTarget = target.position - transform.position;

        //-5 ~ +5 -> - 1 ~ + 1
        AddVectorObs(Mathf.Clamp(distanceToTarget.x / 5f,-1f,1f));
        AddVectorObs(Mathf.Clamp(distanceToTarget.z / 5f, -1f, 1f));

        Vector3 relativePos = transform.localPosition;

        //-5 ~ +5
        AddVectorObs(Mathf.Clamp(relativePos.x / 5f, -1f, 1f));
        AddVectorObs(Mathf.Clamp(relativePos.z / 5f, -1f, 1f));

        //-10 ~ +10 -> -1 ~ +1 (정규화)
        // if -3 이면 -0.3 으로
        AddVectorObs(Mathf.Clamp(ballrigidbody.velocity.x / 10f, -1f, 1f));
        AddVectorObs(Mathf.Clamp(ballrigidbody.velocity.z / 10f, -1f, 1f));
    }

}
