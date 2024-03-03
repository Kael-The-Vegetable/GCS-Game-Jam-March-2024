using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Transform cameraTransform;

    public static event Action<Vector3,Vector3> OnCameraShake;

    public static void Invoke(Vector3 positionStrength, Vector3 rotationalStrength)
    {
        OnCameraShake?.Invoke(positionStrength,rotationalStrength);
    }

    private void OnEnable() => OnCameraShake += CameraShake;
    private void OnDisable() => OnCameraShake -= CameraShake;

    private void CameraShake(Vector3 positionStrength, Vector3 rotationalStrength)
    {
        Debug.Log("Hi squidward");
        cameraTransform.DOComplete();
        cameraTransform.DOShakePosition(0.4f,positionStrength);
        cameraTransform.DOShakeRotation(0.4f, rotationalStrength);
    }
}
