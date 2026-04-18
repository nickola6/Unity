using UnityEngine;

public class CubeClicker : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Cube cube = hit.collider.GetComponent<Cube>();

                if (cube != null)
                {
                    cube.OnMouseDown();
                }
            }
        }
    }
}