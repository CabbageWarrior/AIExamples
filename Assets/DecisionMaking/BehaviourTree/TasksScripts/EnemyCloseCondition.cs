﻿using UnityEngine;
using AI.Movement;

namespace AI.BT
{
    public class EnemyCloseCondition : Task
    {
        public float minDistance = 1f;

        public override TaskState Run()
        {
            var allAgents = FindObjectsOfType<Agent>();
            foreach (var agent in allAgents)
            {
                if (agent != myAgent)
                {
                    if (
                        (agent.transform.position - myAgent.transform.position).sqrMagnitude < minDistance * minDistance &&
                        agent.GetComponent<HealthState>().team != myAgent.GetComponent<HealthState>().team
                        )
                    {
                        btdm.currentEnemy = agent;
                        return TaskState.SUCCESS;
                    }
                }
            }
            return TaskState.FAILURE;
        }
    }
}