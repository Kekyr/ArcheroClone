using UnityEngine;

[CreateAssetMenu(fileName = "new AbilitySO", menuName = "AbilitySO/Create new AbilitySO")]
public class AbilitySO : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;
    [SerializeField] private Ability _skill;

    public string Title => _title;
    public Sprite Icon => _icon;
    public string Description => _description;
    public Ability Skill => _skill;
}