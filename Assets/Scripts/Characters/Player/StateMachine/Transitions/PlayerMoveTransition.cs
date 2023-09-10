using UnityEngine;

public class PlayerMoveTransition : PlayerTransition
{
    private void Update()
    {
        if (InputManager.Direction != Vector3.zero)
        {
            NeedTransit = true;
        }
    }
}