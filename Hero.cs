using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    EventBinding<TestEvent> testEvent;
    EventBinding<PlayerEvent> playerEvent;


    private void OnEnable()
    {
        testEvent = new EventBinding<TestEvent>(HandleTestEvent);
        EventBus<TestEvent>.Register(testEvent);
        playerEvent = new EventBinding<PlayerEvent>(handlePlayerEvent);
        EventBus<PlayerEvent>.Register(playerEvent);

    }

    private void OnDisable()
    {
        EventBus<TestEvent>.DeRegister(testEvent);
        EventBus<PlayerEvent>.DeRegister(playerEvent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventBus<TestEvent>.Raise(new TestEvent()
            {
            });
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EventBus<PlayerEvent>.Raise(new PlayerEvent()
            {
                health = 100,
                mana = 100
            });

        }
    }

    public void HandleTestEvent()
    {
        Debug.Log($"<color=green>Test Event Recieved</color>");
    }
    public void handlePlayerEvent(PlayerEvent playerEvent)
    {
        Debug.Log($"<color=green>Player Event Recieved. Health {playerEvent.health} : Mana {playerEvent.mana}</color>");
    }
}
