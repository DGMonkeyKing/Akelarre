using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public static class MousePointer
{
    public static bool useNewSystem = true;
    public static float RaycastLength = 6f;

    public static bool GetLeftButtonClickedThisFrame()
    {
        if(useNewSystem)
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    public static float2 GetScreenPosition()
    {
        if(useNewSystem)
        {
            return Mouse.current.position.ReadValue();
        }
        else
        {
            Vector3 posn = Input.mousePosition;
            return new float2(posn.x, posn.y);
        }
    }

    public static float2 GetBoundedSreenPosition()
    {
        float2 raw = GetScreenPosition();
        return math.clamp(raw, new float2(0,0), new float2(Screen.width - 1, Screen.height -1));
    }

    //Resolution Independet function.
    public static float2 GetViewportPosition()
    {
        float2 screenPos = GetScreenPosition();
        return screenPos / new float2(Screen.width, Screen.height);
    }

    public static float2 GetViewportPosition(Camera camera)
    {
        float2 screenPos = GetScreenPosition();
        float3 viewportPos = camera.ScreenToViewportPoint(new float3(screenPos, 0));
        return viewportPos.xy;
    }

    public static float3 GetWorldPosition(Camera camera)
    {
        return GetWorldPosition(camera, camera.nearClipPlane);
    }

    public static float3 GetWorldPosition(Camera camera, float worldDepth)
    {
        float2 screenPos = GetBoundedSreenPosition();
        float3 screenPosWithDepth = new float3(screenPos, worldDepth);
        return camera.ScreenToWorldPoint(screenPosWithDepth);
    }

    public static Ray GetWorldRay(Camera camera)
    {
        float2 screenPos = GetBoundedSreenPosition();
        float3 screenPosWithDepth = new float3(screenPos, camera.nearClipPlane);
        return camera.ScreenPointToRay(screenPosWithDepth);
    }
}
