using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;

    void Awake()
    {
        //hide red and green environments at start
        mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("RedGround"));
        mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("GreenGround"));
        mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("RedProjectile"));
        mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("GreenProjectile"));
    }

    //Function to toggle layer visibility depending on player mask
    public void ToggleLayerVisibility(LayerMask addMask, LayerMask removeMask)
    {
        // This "adds" the layer to the camera's view
        mainCamera.cullingMask |= 1 << addMask;
        // This "removes" the layer from the camera's view
        mainCamera.cullingMask &= ~(1 << removeMask);
    }

    public LayerMask getCurrentMask()
    {
        //find and return current visible ground layer
        if ((mainCamera.cullingMask & (1 << LayerMask.NameToLayer("RedGround"))) != 0)
        {
            return LayerMask.NameToLayer("RedGround");
        }
        else if ((mainCamera.cullingMask & (1 << LayerMask.NameToLayer("GreenGround"))) != 0)
        {
            return LayerMask.NameToLayer("GreenGround");
        }
        else
        {
            return LayerMask.NameToLayer("TrueGround");
        }
    }
}