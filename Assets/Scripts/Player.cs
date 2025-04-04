using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    bool _isFacingRight = true;
    public Animator animator;
    
    [Header("Particles")]
    public ParticleSystem smokeFX;
    public ParticleSystem fartFX;
    
    [Header("PlayerStatus")]
    private bool _isDead = false;
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    float _horizontalMovement;
    
    [Header("Jumping")]
    public float jumpPower = 5f;
    public int maxJumps = 2;
    int _jumpsRemaining;
    
    [Header("Farts")]
    public AudioSource fartAudioSource;
    public AudioClip[] fartClips;
    
    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    private bool _isGrounded;
    
    [Header("SpikeCheck")]
    public Transform spikeCheckPos;
    public Vector2 spikeCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask spikeLayer;
    
    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(_horizontalMovement * moveSpeed, rb.linearVelocity.y);
        GroundCheck();
        SpikeCheck();
        Gravity();
        Flip();
        
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontalMovement = context.ReadValue<Vector2>().x;
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            _isGrounded = true;
            _jumpsRemaining = maxJumps;
        }
        else
        {
            _isGrounded = false;
        }
    }

    private void SpikeCheck()
    {
        if (Physics2D.OverlapBox(spikeCheckPos.position, spikeCheckSize, 0, spikeLayer))
        {
            animator.SetTrigger("die");
        } 
    }

    public void Die()
    {
        _isDead = true;
        Debug.Log("Mort : "+_isDead);
        animator.SetBool("isDead", _isDead);
    }

    private void Gravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_jumpsRemaining > 0)
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                _jumpsRemaining--;
                JumpFX();
            }
            else if (context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                _jumpsRemaining--;
            }
        }
    }

    private void Flip()
    {
        if (_isFacingRight && _horizontalMovement < 0 || !_isFacingRight && _horizontalMovement > 0)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
            smokeFX.Play();
        }
    }

    private void JumpFX()
    {
        animator.SetTrigger("jump");
        if(_isGrounded){
            smokeFX.Play();
        }
        else
        {
            fartFX.Play();
            PlayFartSound();
        }
    }
    
    public void PlayFartSound()
    {
        if (fartClips.Length == 0 || fartAudioSource == null) return;
        
        AudioClip clip = fartClips[Random.Range(0, fartClips.Length)];
        fartAudioSource.pitch = Random.Range(0.8f, 3f);
        fartAudioSource.PlayOneShot(clip);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
