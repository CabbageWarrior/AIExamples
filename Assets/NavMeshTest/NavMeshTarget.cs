using UnityEngine;

namespace AI.NavMesh
{
    public class NavMeshTarget : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    transform.position = hit.point;
                }
            }
        }
    }
}
