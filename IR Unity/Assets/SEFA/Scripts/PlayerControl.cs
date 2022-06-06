using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public GameObject mainCamera ;

    private void FixedUpdate() {

        float direction = Input.GetAxis("Horizontal");
        transform.Translate(direction * moveSpeed , 0f,0f);
    }

    private void LateUpdate() {

        Vector3 camPos = new Vector3(transform.position.x+5f, mainCamera.transform.position.y,mainCamera.transform.position.z);
        mainCamera.transform.SetPositionAndRotation(camPos,Quaternion.identity);
    }
}
