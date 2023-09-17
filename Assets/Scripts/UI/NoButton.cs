using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NoButton : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStats;

    private Button _button;
    private Music _music;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _music = FindObjectOfType<Music>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Destroy(_music.gameObject);
        _playerStats.Restart();
    }
}