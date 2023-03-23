using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    #region Fields
    [SerializeField]
    float jumpForce;                            //how much force applied to jump in Y Axis
    [SerializeField]
    float crouchheghit;                          //how much heigh while crouch
    [SerializeField]
    Material playerMaterials;                   //This is player material so we can change player color

    Animator animator;                              //Animator
    Rigidbody myRigidbody;                      //Rigidbody
    CapsuleCollider capsuleCollider;            //CapsuleCollider
    bool isGrounded;                            //Is player on Ground
    bool isCrouching;                           //Is player crouch
    float inputHoldTime;                       //Time calculating How long user pressing the button
    float capsuleHeight;                       //Capsule Height
    #endregion

    #region Properties
    public bool Stop { get; set; }
    public Material PayerMaterials
    { 
        get { return playerMaterials; }
        set { playerMaterials = value; }
    }
    #endregion

    #region Events
    public static event Action OnJump;
    #endregion

    #region Methods
    private void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleHeight= capsuleCollider.height;
    }
    private void Update()
    {
        if (Stop)
        {
            return;
        }
        HandleInput();
        Crouch();
    }
    /// <summary>
    /// Calculate how long User pressing the button and if it's too long player crouch 
    /// if it's short he jump
    /// </summary>
    void HandleInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            inputHoldTime += Time.deltaTime;
            if (inputHoldTime > 0.25f)
            {
                isCrouching = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isCrouching = false;
            Jump();
            inputHoldTime = 0;
        }
    }
    /// <summary>
    /// Decrease the capsuel height to crouch heghit when isCrouching and increase it when not
    /// </summary>
    void Crouch()
    {

        if (isCrouching)
        {
            animator.SetFloat("Crouch", Mathf.MoveTowards(animator.GetFloat("Crouch"), 1, 20 * Time.deltaTime));

            capsuleCollider.height = Mathf.MoveTowards(capsuleCollider.height, crouchheghit, 20 * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Crouch", Mathf.MoveTowards(animator.GetFloat("Crouch"), 0, 20 * Time.deltaTime));

            capsuleCollider.height = Mathf.MoveTowards(capsuleCollider.height, capsuleHeight, 20 * Time.deltaTime);
        }
    }
    /// <summary>
    /// If the player isGrounded you can jump
    /// </summary>
    void Jump()
    {
        if (!isGrounded)
        {
            return;
        }
        if (inputHoldTime <= 0.25f) 
        {
            myRigidbody.AddForce(Vector3.up* jumpForce,ForceMode.Impulse);
            #region Observer
            OnJump?.Invoke();
            #endregion
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = false;
        }
    }
    #endregion
}
