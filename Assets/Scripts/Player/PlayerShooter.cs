using UnityEngine;

class PlayerShooter : MonoBehaviour{
    private PlayerInputManager _playerInputManager;
    private Gun _playerGun;

    private void Awake() {
        _playerGun = GetComponentInChildren<Gun>();
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void Update() {
        if (_playerInputManager.Fire) {
            _playerGun.Fire();
        }
    }
}