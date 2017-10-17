using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Movement
{
    public class Agent : MonoBehaviour
    {
        Vector2 linearVelocity;
        float angularVelocity;

        public SteeringBehaviour steeringBehaviour;

        void Start()
        {
            //linearVelocity = new Vector2(1, 0);
            //angularVelocity = 20;
        }

        void Update()
        {
            // Get the steering output
            SteeringOutput steering = steeringBehaviour.GetSteering();

            // Velocity Update
            linearVelocity = steering.targetLinearVelocity;

            // Position/Rotation Update
            transform.position += (Vector3)(linearVelocity * Time.deltaTime);
            transform.localEulerAngles += Vector3.forward * angularVelocity * Time.deltaTime;


        }
    }
}