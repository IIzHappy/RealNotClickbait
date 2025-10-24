using UnityEngine;

public class ScreenCameraShader : MonoBehaviour
{
    public Material lutMaterial;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("renders");
        if (lutMaterial != null)
        {
            Graphics.Blit(source, destination, lutMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}

