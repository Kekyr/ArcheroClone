using UnityEngine;

public class StartAbility : MonoBehaviour
{
    [SerializeField] private AbilityManager _abilityManager;

    private void Start()
    {
        Time.timeScale = 0;
        _abilityManager.gameObject.SetActive(true);
    }
}
