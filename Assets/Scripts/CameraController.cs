using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform inGameCameraPos;
    private void Awake()
    {
        GameManager.onGameStarted += MoveCameraToInGamePos;
    }
    private void OnDisable()
    {
        GameManager.onGameStarted -= MoveCameraToInGamePos;
    }
    private void MoveCameraToInGamePos()
    {
        transform.DOMove(inGameCameraPos.position, 2f);
        transform.DORotateQuaternion(inGameCameraPos.rotation, 2f);
    }
}
