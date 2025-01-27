using Normal.Realtime;
using UnityEngine;

[RealtimeModel]
public partial class PlayerCustomizationModel
{
    [RealtimeProperty(1, true, true)] private int _selectedTextureIndex;
    [RealtimeProperty(2, true, true)] private int _selectedGlassesIndex;
}