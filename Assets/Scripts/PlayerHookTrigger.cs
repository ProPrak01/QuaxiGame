using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D c) {
        // Detect Hook and It need to be Enemy Hook
        if (c.gameObject.layer == LayerMask.NameToLayer ("Hook")) {
            // Hit the hook
            PlayerController.instance.HitHook (c.transform);

            //apply Camera Shake Effect
            CameraController.Instance.CallCameraShakeEffect();            
        }

        // Detect Lava
        if (c.tag == "lava") {
            Debug.Log ("Player Died !! Game Over ...");
            GameSceneScript.Instance.GameOver();
        }
    }
}
