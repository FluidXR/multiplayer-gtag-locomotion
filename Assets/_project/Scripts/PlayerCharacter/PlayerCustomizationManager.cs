using System;
using System.Security.Cryptography;
using Normal.Realtime;
using UnityEngine;

public class PlayerCustomizationManager : RealtimeComponent<PlayerCustomizationModel>
{
    [SerializeField] private Texture2D[] textureOptions;
    [SerializeField] private GameObject[] glassesOptions;
    [SerializeField] private Renderer avatarRenderer;
    [SerializeField] private RealtimeAvatar avatar;
    [SerializeField] private GameObject Head;

    private const string TEXTURE_PLAYER_PREFS_KEY = "SelectedTextureIndex";
    private const string GLASSES_PLAYER_PREFS_KEY = "SelectedGlassesIndex";

    private void Start()
    {
        LoadSavedTexture();
        LoadSavedGlasses();

        EarlyStore.Instance.TextureIndexChanged += OnTextureIndexChanged;
        EarlyStore.Instance.GlassesIndexChanged += GlassesIndexChanged;
    }

    private void GlassesIndexChanged(int index)
    {
        OnGlassesIndexChanged(index);
    }

    private void OnDestroy()
    {
        if (EarlyStore.Instance != null)
        {
            EarlyStore.Instance.TextureIndexChanged -= OnTextureIndexChanged;
        }
    }

    protected override void OnRealtimeModelReplaced(PlayerCustomizationModel previousModel,
        PlayerCustomizationModel currentModel)
    {
        if (previousModel != null)
        {
            // Unsubscribe from events on the previous model
            previousModel.selectedTextureIndexDidChange -= SelectedTextureIndexDidChange;
            previousModel.selectedGlassesIndexDidChange -= SelectedGlassesChangedOnModel;
        }

        if (currentModel != null)
        {
            // Subscribe to events on the new model
            currentModel.selectedTextureIndexDidChange += SelectedTextureIndexDidChange;
            currentModel.selectedGlassesIndexDidChange += SelectedGlassesChangedOnModel;

            // If we're the owner, set the initial state
            if (currentModel.isOwnedLocallySelf)
            {
                currentModel.selectedTextureIndex = PlayerPrefs.GetInt(TEXTURE_PLAYER_PREFS_KEY, 0);
                currentModel.selectedGlassesIndex = PlayerPrefs.GetInt(GLASSES_PLAYER_PREFS_KEY, 0);
            }

            // Update the avatar with the current texture
            UpdateAvatarTexture(currentModel.selectedTextureIndex);
        }
    }

    private void SelectedGlassesChangedOnModel(PlayerCustomizationModel playerCustomizationModel, int value)
    {
        // OnGlassesIndexChanged(value);
        UpdateAvatarGlasses(value);
    }

    private void OnGlassesIndexChanged(int index)
    {
        if (!avatar.isOwnedLocallyInHierarchy)
        {
            Debug.Log("avatar is not local");
            return;
        }

        if (index >= 0 && index < glassesOptions.Length)
        {
            model.selectedGlassesIndex = index;
            SaveGlasses(index);
        }
    }

    private GameObject currentAvatarGlasses;
    private void UpdateAvatarGlasses(int index)
    {
        if (currentAvatarGlasses != null)
        {
            Destroy(currentAvatarGlasses);
        }
        
        if (index >= 0 && index < glassesOptions.Length)
        {
            currentAvatarGlasses = Instantiate(glassesOptions[index]);
            currentAvatarGlasses.transform.SetParent(Head.transform, false);
            // currentAvatarGlasses.GetComponent<MeshCollider>().enabled = false;
            currentAvatarGlasses.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f) ;
            currentAvatarGlasses.transform.localPosition = new Vector3(0.0f, 0.018f, -0.023f) ;
        }
    }

    private void SaveGlasses(int index)
    {
        PlayerPrefs.SetInt(GLASSES_PLAYER_PREFS_KEY, index);
        PlayerPrefs.Save();
    }

    private void LoadSavedGlasses()
    {
        int savedIndex = PlayerPrefs.GetInt(GLASSES_PLAYER_PREFS_KEY, 0);
        OnGlassesIndexChanged(savedIndex);
    }

    public void OnTextureIndexChanged(int index)
    {
        if (!avatar.isOwnedLocallyInHierarchy)
        {
            Debug.Log("avatar is not local");
            return;
        }

        if (index >= 0 && index < textureOptions.Length)
        {
            model.selectedTextureIndex = index;
            SaveTexture(index);
        }
    }

    private void SelectedTextureIndexDidChange(PlayerCustomizationModel model, int value)
    {
        UpdateAvatarTexture(value);
    }

    private void UpdateAvatarTexture(int index)
    {
        if (avatarRenderer != null && index >= 0 && index < textureOptions.Length)
        {
            avatarRenderer.material.mainTexture = textureOptions[index];
        }
    }

    private void SaveTexture(int index)
    {
        PlayerPrefs.SetInt(TEXTURE_PLAYER_PREFS_KEY, index);
        PlayerPrefs.Save();
    }

    private void LoadSavedTexture()
    {
        int savedIndex = PlayerPrefs.GetInt(TEXTURE_PLAYER_PREFS_KEY, 0);
        OnTextureIndexChanged(savedIndex);
    }
}