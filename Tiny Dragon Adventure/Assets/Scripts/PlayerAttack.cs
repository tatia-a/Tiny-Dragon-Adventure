using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack properties")]
    [SerializeField] GameObject fireballStartPoint;
    [SerializeField] GameObject fireballPrefab;

    Animator animator;
    [SerializeField] float attackCooldown = 2;
    bool isThrowing = false;
    float attackCooldownTimer;

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
            attackCooldownTimer = attackCooldown; // ����� ��������

            // ����� ����������� ��������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ����� ��������������
            Physics.Raycast(ray, out RaycastHit raycastHit);
            fireballStartPoint.transform.LookAt(raycastHit.point);
            //TODO: ���� ��� �� ���������, ��������� ������ ������
            // �������� ��������
            animator.SetTrigger("attackStarted");
            Instantiate(fireballPrefab, fireballStartPoint.transform.position, fireballStartPoint.transform.rotation);
        }
    }
}
