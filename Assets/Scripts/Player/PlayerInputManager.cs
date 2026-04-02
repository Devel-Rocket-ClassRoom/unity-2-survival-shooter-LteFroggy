using UnityEngine;

public class PlayerInputManager : MonoBehaviour {
    private readonly string _horizontal = "Horizontal";
    private readonly string _vertical = "Vertical";
    private readonly string _fire = "Fire1";
    private readonly string _reload = "Reload";

    public float Horizontal { get;  private set; }
    public float Vertical { get;  private set; }
    public bool Fire { get; private set; }
    public bool Reload { get; private set; }

    private void Update() {
        Horizontal = Input.GetAxis(_horizontal);
        Vertical = Input.GetAxis(_vertical);
        Fire = Input.GetButton(_fire);
        Reload = Input.GetButton(_reload);
    }
}
