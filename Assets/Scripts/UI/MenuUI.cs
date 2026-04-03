using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour {
    private readonly string Cancel = "Cancel";
    private Image _panelImage;

    [SerializeField]
    private Slider _musicVolumeSlider;
    [SerializeField]
    private Slider _effectVolumeSlider;

    private bool _isEnabled = false;

    public UnityEvent<float> OnEffectVolumeChange;
    public UnityEvent<float> OnMusicVolumeChange;

    private void Awake() {
        _panelImage = GetComponentInChildren<Image>();

        // 시작 시에는 비활성화
        _panelImage.gameObject.SetActive(false);
    }

    private void Start() {
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        _effectVolumeSlider.onValueChanged.AddListener(OnEffectVolumeChanged);
    }

    private void Update() {
        if (Input.GetButtonDown(Cancel)) {
            if (!_isEnabled) {
                _panelImage.gameObject.SetActive(true);
                Time.timeScale = 0f;
                _isEnabled = true;
            } else {
                _panelImage.gameObject.SetActive(false);
                Time.timeScale = 1f;
                _isEnabled = false;
            }
        }
    }

    private void OnMusicVolumeChanged(float value) {
        OnMusicVolumeChange?.Invoke(value);
    }

    private void OnEffectVolumeChanged(float value) {
        OnEffectVolumeChange?.Invoke(value);
    }


}
