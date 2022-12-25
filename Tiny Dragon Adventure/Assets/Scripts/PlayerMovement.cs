using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("В метрах в секунду")][SerializeField] float Speed = 4f;

    [SerializeField] float xRange = 0.5f;
    [SerializeField] float yUpRange = 0.5f;
    [SerializeField] float yDownRange = 0.5f;

    float xThrow, yThrow;

    [Header("Pitch/Yaw controls")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 25;
    [SerializeField] float controlYawFactor = 0;


    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }
    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = Speed * xThrow * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = Speed * yThrow * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawNewYPos, yDownRange, yUpRange);

        transform.localPosition = new Vector3(clampedXPos,
            transform.localPosition.y,
            transform.localPosition.z);

        transform.localPosition = new Vector3(transform.localPosition.x,
            clampedYPos,
            transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor; // данный питч влияет на восприятие игрока в пространстве,
                                                                                    // немного меняет перспективу
        float pitchDueControlThrow = yThrow * controlPitchFactor; // такой питч заставляет чувствовать большую отдачу,
                                                                  // потому что буквально связан с тем,
                                                                  // что игрок зажимает клавишу дольше
        float pitch = pitchDueToPosition + pitchDueControlThrow;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueControlThrow = xThrow * controlYawFactor;
        float yaw = yawDueToPosition + yawDueControlThrow;

        float roll = 0f;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
