using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 positionStrength;
    public Vector3 rotationalStrength;

    public static event Action OnCameraShake;

    public static void Invoke()
    {
        OnCameraShake?.Invoke();
    }

    private void OnEnable() => OnCameraShake += CameraShake;
    private void OnDisable() => OnCameraShake -= CameraShake;

    private void CameraShake()
    {
        Debug.Log("Hi squidward");
        cameraTransform.DOComplete();
        cameraTransform.DOShakePosition(0.4f,positionStrength);
        cameraTransform.DOShakeRotation(0.4f, rotationalStrength);
    }
}
