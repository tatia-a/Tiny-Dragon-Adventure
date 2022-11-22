using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("В метрах в секунду")][SerializeField] float xSpeed = 4f;
    [SerializeField] float border = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xSpeed * xThrow * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;

        float clampedXPos = Mathf.Clamp(rawNewXPos, -border, border);

        transform.localPosition = new Vector3(clampedXPos, 
            transform.localPosition.y, 
            transform.localPosition.z);
    }
}
