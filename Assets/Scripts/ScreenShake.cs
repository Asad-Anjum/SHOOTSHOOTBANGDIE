using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] public Vector3 positionStrength;
    [SerializeField] public Vector3 rotationStrength;

    private static event Action Shake;
    public static void Invoke(){
        Shake?.Invoke();
    }

    private void OnEnable() => Shake += CameraShake;
    private void OnDisable() => Shake -= CameraShake;

    private void CameraShake()
    {
        camera.DOComplete();
        camera.DOShakePosition(0.3f, positionStrength);
        camera.DOShakeRotation(0.3f, rotationStrength);
    }
}
