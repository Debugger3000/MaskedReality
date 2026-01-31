using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;

    //Function to toggle layer visibility depending on player mask
    public void ToggleLayerVisibility(LayerMask addMask, LayerMask removeMask)
    {
        // This "adds" the layer to the camera's view
        mainCamera.cullingMask |= 1 << addMask;
        // This "removes" the layer from the camera's view
        mainCamera.cullingMask &= ~(1 << removeMask);
    }
}
