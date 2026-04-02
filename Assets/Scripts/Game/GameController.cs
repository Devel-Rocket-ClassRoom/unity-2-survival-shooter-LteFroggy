using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private int _score;

    public void AddScore(int amount) {
        _score += amount;

        _scoreText.text = $"Score: {_score}";
    }
}
