using UnityEngine;

public class GunRotation : MonoBehaviour
{
    private Camera _camera;

    private async void Start()
    {
        _camera = await CameraHelper.AwaitMainCamera();
    }

    async void FixedUpdate()
    {
        if(_camera == null)
            _camera = await CameraHelper.AwaitMainCamera();

        var trackingPosition = Input.mousePosition;

        Ray ray = _camera.ScreenPointToRay(trackingPosition);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out var enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);

        Vector3 speed = (mouseInWorld - transform.position);
        transform.rotation = Quaternion.LookRotation(speed);
    }
}
