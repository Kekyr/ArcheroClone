using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    [SerializeField] private Input _input;
    [SerializeField] private JoystickSO _joystickSO;

    private string _pcDevice = "Keyboard";
    private List<JoystickPart> _joystickParts = new List<JoystickPart>();

    private void Awake()
    {
        _joystickParts.AddRange(GetComponentsInChildren<JoystickPart>());
        SetJoystick();
    }

    private void OnEnable()
    {
        _input.InputData.Player.Move.performed += ctx => OnPress();
    }

    private void OnDisable()
    {
        _input.InputData.Player.Move.performed -= ctx => OnPress();
    }

    private void OnPress()
    {
        string deviceName = _input.InputData.Player.Move.activeControl.device.displayName;

        if (deviceName == _pcDevice && _joystickSO.IsActive == true)
        {
            _joystickSO.ChangeStatus();
            SetJoystick();
        }
    }

    private void SetJoystick()
    {
        foreach (JoystickPart joystickPart in _joystickParts)
        {
            joystickPart.gameObject.SetActive(_joystickSO.IsActive);
        }
    }
}
