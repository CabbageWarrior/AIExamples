using UnityEngine;

namespace AI.Movement
{
    public abstract class SteeringBehaviour : MonoBehaviour
    {
        public abstract SteeringOutput GetSteering();
    }
}