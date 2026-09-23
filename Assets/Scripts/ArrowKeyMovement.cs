using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour
{
    Rigidbody rb;
    public float movement_speed = 4;
    private Animator _animator;
    SpriteRenderer sprite;
    public Sprite attackSpriteDown; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 current_input = GetInput();
        rb.linearVelocity = current_input * movement_speed;

        if (Input.GetKeyDown(KeyCode.X))
        {
            playerAttack();
        }
    }

    Vector2 GetInput()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");

        float vertical_input = Input.GetAxisRaw("Vertical");
        if(Mathf.Abs(horizontal_input) > 0.0f)
        {
            vertical_input = 0.0f;
        }
        animationPlayer(horizontal_input, vertical_input);

        return new Vector2(horizontal_input, vertical_input);
    }

    public void animationPlayer(float horizontal, float vertical)
    {
        if (horizontal > 0)
        {
            _animator.Play("WalkRight");
            
        }
        else if (horizontal < 0)
        {
            _animator.Play("WalkLeft");
        }
        else if (vertical > 0)
        {
            _animator.Play("WalkUp");
        }
        else if (vertical < 0)
        {
            _animator.Play("WalkDown");
        }
    }

    public void playerAttack()
    {
        Debug.Log("e");
        _animator.enabled = false;
        sprite.sprite = attackSpriteDown;

    }
}
