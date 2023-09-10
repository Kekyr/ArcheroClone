using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityView : MonoBehaviour
{
    [SerializeField] private SFXSO _sfx;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    
    private Ability _ability;
    private Audio _audio;
    private Transform _canvas;

    public Button Key => _button;

    private void Awake()
    {
        _canvas = transform.parent.parent.parent;
        _audio = _canvas.gameObject.GetComponentInChildren<Audio>();
    }

    public void Init(AbilitySO data, Ability ability)
    {
        _title.text = data.Title;
        _icon.sprite = data.Icon;
        _description.text = data.Description;
        _ability = ability;
        _button.onClick.AddListener(_ability.TurnOn);
        _button.onClick.AddListener(Play);
    }

    private void Play()
    {
        _audio.Play(_sfx);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
        Destroy(_ability.gameObject);
    }
}