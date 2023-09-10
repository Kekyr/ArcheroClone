using UnityEngine;

[CreateAssetMenu(fileName = "new JoystickSO", menuName = "JoystickSO/Create new JoystickSO")]
public class JoystickSO : ScriptableObject
{
    [SerializeField] private bool _isActive;

    public bool IsActive => _isActive;

    public void ChangeStatus()
    {
        _isActive = !_isActive;
    }
}
