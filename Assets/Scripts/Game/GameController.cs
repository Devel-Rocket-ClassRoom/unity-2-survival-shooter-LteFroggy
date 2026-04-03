using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private AudioClip _backgroundMusic;

    private AudioSource _audioSource;

    private int _score;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();

        // Menu에 볼륨 조작 넣기
        GameObject.FindWithTag(Tags.Menu).GetComponent<MenuUI>().OnMusicVolumeChange.AddListener(OnVolumeChanged);
    }

    public void AddScore(int amount) {
        _score += amount;

        _scoreText.text = $"Score: {_score}";
    }


    private void OnVolumeChanged(float value) {
        _audioSource.volume = value;
    }
}
