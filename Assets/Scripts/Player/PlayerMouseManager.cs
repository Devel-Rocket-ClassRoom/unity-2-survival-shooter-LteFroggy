using UnityEngine;

public class PlayerMouseManager : MonoBehaviour
{
    public Vector3 MousePosition { get; private set; }

    private void Update() {
        // 카메라 시작점에서 
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out RaycastHit hit, 50f, Layers.FloorMask)) {
            MousePosition = hit.point;
        }
    }
}
