using System;
using UnityEngine;

public class EarlyStore : Singleton<EarlyStore>
{
    public Action<int> TextureIndexChanged;
    public Action<int> GlassesIndexChanged;
}
