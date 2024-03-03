using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private float adjustmentAmount = .1f;
    EventBinding<PlayerEvent> playerEventBinding;

    private Slider slider;

    private void OnEnable()
    {
        playerEventBinding = new EventBinding<PlayerEvent>(HandlePlayerHealthUI);
        EventBus<PlayerEvent>.Register(playerEventBinding);
    }

    private void OnDisable()
    {
        EventBus<PlayerEvent>.DeRegister(playerEventBinding);
    }
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void HandlePlayerHealthUI(PlayerEvent playerEvent)
    {
        slider.value += adjustmentAmount;
    }
}
