using Unity.Netcode.Components;
using UnityEngine;

/// <summary>
/// Used for syncing a transform with client side changes. This includes host.
/// </summary>

[DisallowMultipleComponent]
public class ClientNetworkTransform : NetworkTransform
{
    /// <summary>
    /// Used to detirmine who can write to this transform. Owner client only.
    /// This imposes state to the server. This is putting trust on your clients. 
    /// Make shure no security-sensitive features use this transform
    /// </summary>
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
