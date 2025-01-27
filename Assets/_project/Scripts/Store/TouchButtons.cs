using UnityEngine;

public class TouchButtons : MonoBehaviour
{
    public ButtonType buttonType;
    public int Index;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Hands")) return;

        if (buttonType == ButtonType.glasses)
        {
            Debug.Log($"On Trigger Enter {other.gameObject.name}");
            EarlyStore.Instance.GlassesIndexChanged?.Invoke(Index);
        }
        else if (buttonType == ButtonType.texture)
        {
            Debug.Log($"On Collider Enter {other.gameObject.name}");
            EarlyStore.Instance.TextureIndexChanged?.Invoke(Index);
        }
    }
}

[System.Serializable]
public enum ButtonType
{
    glasses,
    texture
}