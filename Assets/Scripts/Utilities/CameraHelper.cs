using System.Threading.Tasks;
using UnityEngine;

public static class CameraHelper
{
    public static async Task<Camera> AwaitMainCamera()
    {
        var camera = Camera.main;
        while (camera == null)
        {
            camera = Camera.main;
            await Task.Yield();

        }

        return camera;
    }
}
