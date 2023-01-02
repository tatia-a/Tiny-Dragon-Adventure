using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack properties")]
    [SerializeField] private GameObject fireballStartPoint;
    [SerializeField] private GameObject fireballPrefab;

    [SerializeField] private float attackCooldown = 2;
    private float attackCooldownTimer;

    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        ThrowFireball();
    }
    private void ThrowFireball()
    {
        attackCooldownTimer -= Time.deltaTime;

        bool isKeyPressed = CrossPlatformInputManager.GetButtonDown("Fire1");
        bool isCooldownEnded = attackCooldownTimer <= 0;

        if (isKeyPressed && isCooldownEnded)
        {
            // сброс кулдауна
            attackCooldownTimer = attackCooldown;

            animator.SetTrigger("attackStarted");

            InstantiateFireball();
        }
    }

    private void InstantiateFireball()
    {
        // повернуть прицел к точке
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distanse = 50f;
        Vector3 lookPoint = ray.GetPoint(distanse);
        fireballStartPoint.transform.LookAt(lookPoint);

        Instantiate(fireballPrefab, fireballStartPoint.transform.position, fireballStartPoint.transform.rotation);
    }
}
