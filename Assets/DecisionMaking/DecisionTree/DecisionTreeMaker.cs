namespace AI.DecisionTree
{
    public class DecisionTreeMaker : DecisionMaker
    {
        Decision root;

        private void Awake()
        {
            // Create the decision tree
            root = gameObject.AddComponent<MyHealthCondition>();
            root.trueNode = gameObject.AddComponent<EnemyCloseCondition>();
            root.falseNode = gameObject.AddComponent<SeekPickup>();
            Decision decision2 = (Decision)root.trueNode;
            decision2.trueNode = gameObject.AddComponent<SeekShoot>();
            decision2.falseNode = gameObject.AddComponent<SeekBase>();

            // ... add parameters
            SeekPickup seekPickupNode = (SeekPickup)root.falseNode;
            seekPickupNode.seekBe = GetComponent<AI.Movement.SeekBehaviour>();
            seekPickupNode.fleeBe = GetComponent<AI.Movement.FleeBehaviour>();

            SeekShoot seekShootNode = (SeekShoot)decision2.trueNode;
            seekShootNode.seekBe = GetComponent<AI.Movement.SeekBehaviour>();
            seekShootNode.fleeBe = GetComponent<AI.Movement.FleeBehaviour>();
            seekShootNode.shootAction = GetComponent<ShootAction>();

            SeekBase seekBaseNode = (SeekBase)decision2.falseNode;
            seekBaseNode.seekBe = GetComponent<AI.Movement.SeekBehaviour>();
            seekBaseNode.fleeBe = GetComponent<AI.Movement.FleeBehaviour>();
        }

        public override void MakeDecision()
        {
            // Ask for an action
            root.MakeDecision();
        }
    }
}
