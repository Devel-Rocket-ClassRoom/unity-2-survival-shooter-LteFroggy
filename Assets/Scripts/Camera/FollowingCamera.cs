using UnityEngine;

public class FollowingCamera : MonoBehaviour {
    private const float k_XOffset = 0.19f;
    private const float k_YOffset = 8.81f;
    private const float k_ZOffset = -11.9f;

    private GameObject _followingTarget;

    private void Awake() {
        _followingTarget = GameObject.FindWithTag(Tags.Player);
    }

    private void FixedUpdate() {
        transform.position = _followingTarget.transform.position + new Vector3(k_XOffset, k_YOffset, k_ZOffset);
    }
}
