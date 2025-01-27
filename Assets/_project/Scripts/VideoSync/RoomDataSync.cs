using System;
using UnityEngine;
using Normal.Realtime;

public class RoomDataSync : RealtimeComponent<RoomDataModel>
{
    /// <summary>
    /// we were syncing a string for all clients but this is just an example that can be used for anything
    /// </summary>
    private string exampleValue;
    public string URL
    {
        get => exampleValue;
        set
        {
            if (exampleValue != value)
            {
                exampleValue = value;
                if (model != null)
                {
                    model.exampleString = exampleValue;
                }
            }
        }
    }

    public Action<string> ExampleValueChanged;

    protected override void OnRealtimeModelReplaced(RoomDataModel previousModel, RoomDataModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.exampleStringDidChange -= ExampleValueDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                currentModel.exampleString = exampleValue;
            }
            exampleValue = currentModel.exampleString;

            currentModel.exampleStringDidChange += ExampleValueDidChange;
        }
    }

    private void ExampleValueDidChange(RoomDataModel model, string value)
    {
        exampleValue = value;
        ExampleValueChanged?.Invoke(value);
        // You can add any additional logic here that should happen when the string changes
        Debug.Log($"Shared string changed to: {exampleValue}");
    }
}
