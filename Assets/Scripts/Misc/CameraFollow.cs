using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeReference] Transform player;

    [SerializeReference] float minXClamp;
    [SerializeReference] float maxXClamp;
    [SerializeReference] float minYClamp;
    [SerializeReference] float maxYClamp;

    private void LateUpdate() {
        if (player) {
            Vector3 cameraPosition = transform.position;
            cameraPosition.x = Mathf.Clamp(player.transform.position.x, minXClamp, maxXClamp);
            cameraPosition.y = Mathf.Clamp(player.transform.position.y, minYClamp, maxYClamp);
            transform.position = cameraPosition;
        }
    }
}
