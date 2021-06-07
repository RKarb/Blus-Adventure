using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    class CameraRotation
    {
        public float yaw, pitch, roll;

        public void InitializeFromTransform(Transform t)
        {
            pitch = t.eulerAngles.x;
            yaw = t.eulerAngles.y;
            roll = t.eulerAngles.z;
        }

        public void LerpTowards(CameraRotation target, float rotationLerpPct)
        {
            yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
            pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
            roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);
        }

        public void UpdateTransform(Transform t)
        {
            t.eulerAngles = new Vector3(pitch, yaw, roll);
        }
    }

    CameraRotation targetRotation = new CameraRotation();
    CameraRotation currentRotation = new CameraRotation();

    [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
    public float lerpTime = 0.01f;

    public float xSensitivity = 5f;
    public float ySensitivity = 5f;

    void OnDisable() { Cursor.lockState = CursorLockMode.None; }

    void OnEnable()
    {
        targetRotation.InitializeFromTransform(transform);
        currentRotation.InitializeFromTransform(transform);
        // This makes the cursor hidden -- hit Escape to get your cursor back so you can exit play mode
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Update the target rotation based on mouse input
        var mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * -1);
        targetRotation.yaw += mouseInput.x * xSensitivity;
        targetRotation.pitch += mouseInput.y * ySensitivity;

        // Don't allow the camera to flip upside down
        targetRotation.pitch = Mathf.Clamp(targetRotation.pitch, -90, 90);

        // Calculate the new rotation using framerate-independent interpolation
        var lerpPct = 1f - Mathf.Exp(Mathf.Log(0.01f) / lerpTime * Time.deltaTime);
        currentRotation.LerpTowards(targetRotation, lerpPct);

        // Commit the rotation changes to the transform
        currentRotation.UpdateTransform(transform);
    }
}
