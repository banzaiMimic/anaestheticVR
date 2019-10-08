using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Drag onto a camera, gives developer right-click + drag functionality to move camera around.
 */
namespace AnaestheticVR {
  public class CameraDragger : MonoBehaviour {
#if UNITY_EDITOR
    public bool invertMouseY = true;
    private new Camera camera;
    private bool isDragging = false;
    private float mouseXStart;
    private float mouseYStart;

    private void Awake() {
      camera = GetComponent<Camera>();
    }

    private void Update() {
      if (Input.GetMouseButtonDown(1) && !isDragging) {
        isDragging = true;
        mouseXStart = Input.mousePosition.x;
        mouseYStart = Input.mousePosition.y;
      } else if (Input.GetMouseButtonUp(1) && isDragging) {
        isDragging = false;
      }
    }

    private void LateUpdate() {
      if (isDragging) {
        // get current mouse pos
        Vector3 mousePos = Input.mousePosition;
        float endMouseX = mousePos.x;
        float endMouseY = (invertMouseY) ? -mousePos.y : mousePos.y;

        // difference
        float diffX = endMouseX - mouseXStart;
        float diffY = endMouseY - mouseYStart;

        // only update screen if there is difference in x and y - fixes screen auto moving downward
        if (diffX != 0 && diffY != 0) {
          // updated screen center
          float centerXUpdate = Screen.width / 2 + diffX;
          float centerYUpdate = Screen.height / 2 + diffY;

          Vector3 updatedPosition = camera.ScreenToWorldPoint(new Vector3(centerXUpdate, centerYUpdate, camera.nearClipPlane));

          transform.LookAt(updatedPosition);
        }

        mouseXStart = endMouseX;
        mouseYStart = endMouseY;
      }
    }

#endif
  }
}