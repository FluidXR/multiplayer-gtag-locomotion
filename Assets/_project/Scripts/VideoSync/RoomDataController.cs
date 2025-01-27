using System;
using Normal.Realtime;
using UnityEngine;

/// <summary>
/// This class is not required its an example to show how to get and set the url for the room.
/// </summary>
public class RoomDataController : Singleton<RoomDataController>
{
    public string debugString;
    
    private Realtime realtime;
    [SerializeField] private LocalAvatarSetup localAvatarSetup;
    [SerializeField] RoomDataSync roomDataSync;

    private void Start()
    {
        realtime = GetComponent<Realtime>();
        realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        // connected to room
    }

    public string GetSharedString()
    {
        return roomDataSync != null ? roomDataSync.URL : "";
    }
}