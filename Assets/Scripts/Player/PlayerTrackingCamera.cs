using UnityEngine;

[RequireComponent (typeof(Camera))]

public class PlayerTrackingCamera : MonoBehaviour
{
    //[SerializeField] private GameObject Player;
    private Camera _camera;
    public GameObject Player;

    public Vector3 Offset;
    public float CameraSpeed = 5f;
    public float ZoomSpeed;

    private void Start()
    {
        _camera = GetComponent<Camera> ();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (Player == null) return;

        var oldCameraPostion = _camera.transform.position;
        var newCameraPosition = Player.transform.position + Offset;

        _camera.transform.position = Vector3.Lerp(
            oldCameraPostion, 
            newCameraPosition, 
            CameraSpeed * Time.deltaTime
        );  
    }

        //if (Input.mouseScrollDelta.y != 0)
        //    Offset.z += 1 * ZoomSpeed;
}
