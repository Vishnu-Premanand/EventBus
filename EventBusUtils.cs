using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Searcher.Searcher.AnalyticsEvent;

public static class EventBusUtils
{

    public static IReadOnlyList<Type> EventTypes { get; set; }
    public static IReadOnlyList<Type> EventBusTypes { get; set; }


#if UNITY_EDITOR

    public static PlayModeStateChange PlayModeStateChange { get; set; }

    [InitializeOnLoadMethod]
    public static void InitializeEditor()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange change)
    {
        PlayModeStateChange = change;
        if (change == PlayModeStateChange.ExitingPlayMode)
        {
            ClearAllBuses();
        }
    }
#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        Debug.Log("InitializeCalled");
        EventTypes = PreDefinedAssemblyUtils.GetTypes(typeof(IEvent));
        EventBusTypes = GetAllEventBusTypes();

    }

    private static List<Type> GetAllEventBusTypes()
    {
        List<Type> types = new List<Type>();

        var typeOf= typeof(EventBus<>);
        foreach (var type in EventTypes)
        {
            Debug.Log($"Type : {type}");
            var busType = typeOf.MakeGenericType(type);
            types.Add(busType);
            Debug.Log($"Initialized EventBus<{type.Name}>");
        }
        return types;
    }
    public static void ClearAllBuses()
    {
        Debug.Log("Clearing All Busses......");

        for(int i = 0;i<EventTypes.Count;i++)
        {
            var busType= EventBusTypes[i];
            var clearMethod= busType.GetMethod("Clear",BindingFlags.Static | BindingFlags.NonPublic);
            clearMethod?.Invoke(null, null);
        }
    }
}

