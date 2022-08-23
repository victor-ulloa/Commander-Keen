using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeReference] float minXClamp;
    [SerializeReference] float maxXClamp;
    [SerializeReference] float minYClamp;
    [SerializeReference] float maxYClamp;

    private void LateUpdate() {
        if (GameManager.instance.playerInstance) {
            Vector3 cameraPosition = transform.position;
            cameraPosition.x = Mathf.Clamp(GameManager.instance.playerInstance.transform.position.x, minXClamp, maxXClamp);
            cameraPosition.y = Mathf.Clamp(GameManager.instance.playerInstance.transform.position.y, minYClamp, maxYClamp);
            transform.position = cameraPosition;
        }
    }
}
