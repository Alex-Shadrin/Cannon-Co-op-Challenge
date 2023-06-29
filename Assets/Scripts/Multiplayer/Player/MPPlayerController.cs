using Unity.Netcode;
using UnityEngine;

public class MPPlayerController : NetworkBehaviour
{

    public void Update()
    {
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsOwner)
            TryTurnOffControls();

        Debug.Log("Network spawn");
        Debug.Log("Network spawn is owner: " + IsOwner);

        if(IsOwner)
        {
            var camera = Camera.main.GetComponent<PlayerTrackingCamera>();
            camera.Player = this.gameObject;
        }
    }

    private bool _controlsAreTurnedOf = false;
    private void TryTurnOffControls()
    {
        if (_controlsAreTurnedOf)
            return;

        var gun = GetComponentInChildren<Gun>();
        var gunRotation = GetComponentInChildren<GunRotation>();

        this.tag = "Untagged";
        // disable gun
        gun.enabled = false;
        gunRotation.enabled = false;

        _controlsAreTurnedOf = true;
    }
}
