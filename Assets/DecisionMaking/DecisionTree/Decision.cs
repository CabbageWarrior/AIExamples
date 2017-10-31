namespace AI.DecisionTree
{
    public abstract class Decision : Node
    {
        public abstract bool CheckCondition();
        public Node trueNode;
        public Node falseNode;

        public override void MakeDecision()
        {
            if (CheckCondition())
            {
                trueNode.MakeDecision();
            }
            else
            {
                falseNode.MakeDecision();
            }
        }
    }
}