using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 120f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifier = 5f;
    [SerializeField] ParticleSystem speedupParticleSystem;

    CinemachineCamera cinemachineCamera;

    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }
    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));
        if (speedAmount > 0)
        {
            speedupParticleSystem.Play();
        }
    }

    IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFov = Mathf.Clamp(startFOV+speedAmount * zoomSpeedModifier, minFOV, maxFOV);
        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / zoomDuration; 
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFov, t);
            yield return null;
        }
        cinemachineCamera.Lens.FieldOfView = targetFov;
    }
}
