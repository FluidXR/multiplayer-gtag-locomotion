using System;
using GorillaLocomotion;
using Normal.Realtime;
using UnityEngine;

public class LocalAvatarSetup : MonoBehaviour
{
    private RealtimeAvatarManager _manager;
    private RealtimeAvatar _localAvatar;
    private Player localPlayer;

    public Action LocalAvatarCreated;

    private void Awake() {
        _manager = GetComponent<RealtimeAvatarManager>();
        _manager.avatarCreated += AvatarCreated;
        _manager.avatarDestroyed += AvatarDestroyed;
    }

    private void Start()
    {
        localPlayer = Player.Instance;
    }

    private void AvatarCreated(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar) {
        if (isLocalAvatar)
        {
            _localAvatar = avatar;
            LocalAvatarCreated?.Invoke();
        }
    }

    private void AvatarDestroyed(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar) {
        // Avatar destroyed!
    }

    private void LateUpdate()
    {
        if (_localAvatar != null)
        {
            _localAvatar.transform.position = localPlayer.transform.position;
            _localAvatar.transform.rotation = localPlayer.transform.rotation;
        }
    }
}
