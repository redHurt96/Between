using RH.Utilities.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GameCamera : MonoBehaviourSingleton<GameCamera>
{
    public static Camera MainCamera
    {
        get
        {
            if (Instance._mainCamera == null)
                Instance._mainCamera = Instance.GetComponent<Camera>();

            return Instance._mainCamera;
        }
    }

    private Camera _mainCamera;

    public static Vector3 ScreenToWorldPoint(Vector3 screenPoint)
    {
        var zDistance = - MainCamera.transform.position.z;
        return MainCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, zDistance));
    }
}
