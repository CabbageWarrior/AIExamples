using System.Collections;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{
    public abstract class Task : MonoBehaviour
    {
        [HideInInspector]
        public Agent myAgent;
        [HideInInspector]
        public BehaviourTreeDecisionMaker btdm; // for Blackboard access

        public abstract TaskState Run();
    }
}