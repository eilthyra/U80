using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OUA.Movement
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 3f;
        [SerializeField] private float sprintSpeed = 6f;
        [SerializeField] private float speedSmoothTime = 0.1f;
        [SerializeField] private float maxStamina = 100f;
        [SerializeField] private float staminaDecreaseRate = 10f;
        [SerializeField] private float staminaRecoveryRate = 5f;
        [SerializeField] private float sprintCooldownDuration = 5f;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private Slider staminaSlider;
        [SerializeField] private LayerMask groundLayer; // Zemini belirlemek için bir katman maskesi

        private CharacterController controller = null;
        private Animator animator = null;
        private Transform mainCameraTransform = null;

        private float velocityY = 0f;
        private float speedSmoothVelocity = 0f;
        private float currentSpeed = 0f;
        private float currentStamina = 0f;
        private float sprintCooldownTimer = 0f;
        private bool isJumping = false;

        private static readonly int hashSpeedPercentage = Animator.StringToHash("SpeedPercentage");
        private static readonly int hashJump = Animator.StringToHash("Jump");
        private static readonly int hashIsGrounded = Animator.StringToHash("isGrounded");

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            mainCameraTransform = Camera.main.transform;
            currentStamina = maxStamina;
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }

        private void Update()
        {
            Move();
            HandleSprintCooldown();
        }

        private void Move()
        {
            Vector2 movementInput = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            ).normalized;

            Vector3 forward = mainCameraTransform.forward;
            Vector3 right = mainCameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 desiredMoveDirection = (forward * movementInput.y + right * movementInput.x).normalized;

            if (desiredMoveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 0.1f);
            }

            bool isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && sprintCooldownTimer <= 0;
            float targetSpeed = (isSprinting ? sprintSpeed : movementSpeed) * movementInput.magnitude;

            if (isSprinting)
            {
                currentStamina -= staminaDecreaseRate * Time.deltaTime;
                if (currentStamina < 0)
                {
                    currentStamina = 0;
                    sprintCooldownTimer = sprintCooldownDuration;
                }
            }
            else
            {
                currentStamina += staminaRecoveryRate * Time.deltaTime;
                if (currentStamina > maxStamina)
                {
                    currentStamina = maxStamina;
                }
            }

            bool isGrounded = CheckIfGrounded();
            animator.SetBool(hashIsGrounded, isGrounded);

            if (isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    velocityY = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
                    animator.SetTrigger(hashJump);
                    isJumping = true;
                }
                else
                {
                    isJumping = false;
                }
            }

            velocityY += Physics.gravity.y * Time.deltaTime;
            Vector3 velocity = desiredMoveDirection * currentSpeed + Vector3.up * velocityY;

            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
            controller.Move(velocity * Time.deltaTime);

            if (isGrounded && !isJumping)
            {
                velocityY = 0;
            }

            animator.SetFloat(hashSpeedPercentage, 0.5f * movementInput.magnitude, speedSmoothTime, Time.deltaTime);
            staminaSlider.value = currentStamina;
        }

        private void HandleSprintCooldown()
        {
            if (sprintCooldownTimer > 0)
            {
                sprintCooldownTimer -= Time.deltaTime;
            }
        }

        private bool CheckIfGrounded()
        {
            // CharacterController'ın altındaki bir noktayla zemini kontrol et
            return Physics.CheckSphere(controller.bounds.center - new Vector3(0, controller.bounds.extents.y, 0), 0.1f, groundLayer);
        }
    }
}