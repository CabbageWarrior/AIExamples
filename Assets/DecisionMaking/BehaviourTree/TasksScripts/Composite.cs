using System.Collections.Generic;
using UnityEngine;

namespace AI.BT
{
    public abstract class Composite : Task
    {
        [HideInInspector]
        public List<Task> children;
    }
}