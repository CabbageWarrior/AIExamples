using AI.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class FSMDecisionMaker : DecisionMaker
    {
        public Animator aiAnimator;

        Agent agent;

        // Use this for initialization
        void Start()
        {
            agent = GetComponent<Agent>();

            var seekBe = agent.GetComponent<SeekBehaviour>();
            var fleeBe = agent.GetComponent<FleeBehaviour>();
            var shootAction = agent.GetComponent<ShootAction>();

            var goToBaseState = aiAnimator.GetBehaviour<GoToBaseState>();
            goToBaseState.agent = agent;
            goToBaseState.seekBe = seekBe;
            goToBaseState.fleeBe = fleeBe;

            var attackEnemyState = aiAnimator.GetBehaviour<AttackEnemyState>();
            attackEnemyState.agent = agent;
            attackEnemyState.seekBe = seekBe;
            attackEnemyState.fleeBe = fleeBe;
            attackEnemyState.shootAction = shootAction;

            var getHealthState = aiAnimator.GetBehaviour<GetHealthState>();
            getHealthState.agent = agent;
            getHealthState.seekBe = seekBe;
            getHealthState.fleeBe = fleeBe;

            var RunAwayState = aiAnimator.GetBehaviour<RunAwayState>();
            RunAwayState.agent = agent;
            RunAwayState.seekBe = seekBe;
            RunAwayState.fleeBe = fleeBe;

            // Animator runs before Update, so we do the first check now
            MakeDecision();
        }

        public override void MakeDecision()
        {
            // Check State Switching
            // ... We just need to update inputs to the AC

            float closestDistanceSqr = float.MaxValue;

            var allAgents = FindObjectsOfType<Agent>();
            foreach (var otherAgent in allAgents)
            {
                if (otherAgent != agent)
                {
                    if (
                        (otherAgent.transform.position - transform.position).sqrMagnitude < closestDistanceSqr &&
                        otherAgent.GetComponent<HealthState>().team != GetComponent<HealthState>().team
                        )
                    {
                        closestDistanceSqr = (otherAgent.transform.position - transform.position).sqrMagnitude;
                    }
                }
            }
            aiAnimator.SetFloat("ClosestEnemyDistance", closestDistanceSqr);

            aiAnimator.SetInteger("MyHealth", (int)GetComponent<HealthState>().health);

            aiAnimator.SetInteger("HealthPickups", FindObjectsOfType<HealthPickup>().Length);

            // Execute current state
            // ... The AC will do that for us
        }
    }
}