using UnityEngine;

namespace AI.BT
{
    public class MyHealthCondition : Task
    {
        public float minHealth = 5f;

        public override TaskState Run()
        {
            var myHealthState = myAgent.GetComponent<HealthState>();
            return (myHealthState.health >= minHealth ? TaskState.SUCCESS : TaskState.FAILURE);
        }
    }

}