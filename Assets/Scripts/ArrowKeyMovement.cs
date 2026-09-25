using UnityEngine;
using System.Collections;

public class ArrowKeyMovement : MonoBehaviour
{
    Rigidbody rb;
    public float movement_speed = 4;
    private Animator _animator;
    SpriteRenderer sprite;
    [SerializeField]
    Sprite attackSpriteDown;
    [SerializeField]
    Sprite attackSpriteUp;
    [SerializeField]
    Sprite attackSpriteRight;
    [SerializeField]
    Sprite attackSpriteLeft;
    [SerializeField]
    GameObject swordDown;
    [SerializeField]
    GameObject swordUp;
    [SerializeField]
    GameObject swordRight;
    [SerializeField]
    GameObject swordLeft;

    private bool playLock = false;
    private bool beamShot = false;

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
        if (playLock == false)
        {
            Vector2 current_input = GetInput();
            rb.linearVelocity = current_input * movement_speed;

            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(playerAttack(current_input));
            }
        }

    }

    Vector2 GetInput()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");

        float vertical_input = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horizontal_input) > 0.0f)
        {
            vertical_input = 0.0f;
        }
        animationPlayer(horizontal_input, vertical_input);

        return new Vector2(horizontal_input, vertical_input);
    }

    public void animationPlayer(float horizontal, float vertical)
    {
        _animator.speed = 1;
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
        else
        {
            _animator.speed = 0;
        }
    }

    IEnumerator playerAttack(Vector2 direction)
    {
        rb.linearVelocity = Vector2.zero;
        _animator.enabled = false;
        Vector3 currPosition = transform.position;
        GameObject sword;
        
        playLock = true;

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkDown"))
        {
            //Changes link's sprite to attack sprite
            sprite.sprite = attackSpriteDown;
            //Creates a visible raycast in debug to show sword length
            Debug.DrawRay(this.transform.position, this.transform.up * -1.3f, Color.red, .2f);
            //Creates sword sprite
            sword = Object.Instantiate(swordDown, currPosition + new Vector3(0, -.8f, 0), Quaternion.identity);
            //Creates actual raycast to destroy objects
            if (Physics.Raycast(this.transform.position, -Vector3.up, out RaycastHit hit, 1.3f) && hit.collider.gameObject.CompareTag("enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
            //Deletes sword sprite
            Destroy(sword, .2f);
            if(beamShot == false)
            {
                StartCoroutine(shootSwordBeam(swordDown));
            }
            beamShot = true;

        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkUp"))
        {
            sprite.sprite = attackSpriteUp;

            Debug.DrawRay(this.transform.position, this.transform.up * 1.3f, Color.red, .2f);

            sword = Object.Instantiate(swordUp, currPosition + new Vector3(0, .8f, 0), Quaternion.identity);

            if (Physics.Raycast(this.transform.position, Vector3.up, out RaycastHit hit, 1.3f) && hit.collider.gameObject.CompareTag("enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
            Destroy(sword, .2f);
            if (beamShot == false)
            {
                StartCoroutine(shootSwordBeam(swordUp));
            }
            beamShot = true;
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkRight"))
        {
            sprite.sprite = attackSpriteRight;

            Debug.DrawRay(this.transform.position, this.transform.right * 1.3f, Color.red, .2f);

            sword = Object.Instantiate(swordRight, currPosition + new Vector3(.8f, 0, 0), Quaternion.identity);

            if (Physics.Raycast(this.transform.position, Vector3.right, out RaycastHit hit, 1.3f) && hit.collider.gameObject.CompareTag("enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
            Destroy(sword, .2f);
            if (beamShot == false)
            {
                StartCoroutine(shootSwordBeam(swordRight));
            }
            beamShot = true;

        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkLeft"))
        {
            sprite.sprite = attackSpriteLeft;

            Debug.DrawRay(this.transform.position, this.transform.right * -1.3f, Color.red, .2f);

            sword = Object.Instantiate(swordLeft, currPosition + new Vector3(-.8f, 0, 0), Quaternion.identity);

            if (Physics.Raycast(this.transform.position, -Vector3.right, out RaycastHit hit, 1.3f) && hit.collider.gameObject.CompareTag("enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
            Destroy(sword, .2f);
            if (beamShot == false)
            {
                StartCoroutine(shootSwordBeam(swordLeft));
            }
            beamShot = true;
        }

        yield return new WaitForSeconds(0.2f);
        playLock = false;
        _animator.enabled = true;
    }

    IEnumerator shootSwordBeam(GameObject swordBeam)
    {
        Vector3 currPosition = transform.position;
        bool noHit = true;
        //GameObject swordBeam;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkUp"))
        {
            swordBeam = Object.Instantiate(swordBeam, currPosition + new Vector3(0, .8f, 0), Quaternion.identity);
            
            while (Camera.main.WorldToViewportPoint(swordBeam.transform.position).y < 0.8 )
            {
                if (Physics.Raycast(swordBeam.transform.position, -Vector3.up, out RaycastHit hit, 1.3f) && hit.collider.gameObject.CompareTag("enemy"))
                {
                    hit.transform.position += new Vector3(0, 0.2f, 0);
                    Destroy(hit.collider.gameObject);
                    noHit = false;
                    yield return null;
                    Destroy(swordBeam, .2f);
                }
                swordBeam.transform.position += new Vector3(0, 0.2f, 0);
                yield return null;
                if(swordBeam != null)
                {
                    Destroy(swordBeam, .2f);
                }
            }
            //Destroy(swordBeam, .2f);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkDown"))
        {
            swordBeam = Object.Instantiate(swordBeam, currPosition + new Vector3(0, -.8f, 0), Quaternion.identity);
            while (Camera.main.WorldToViewportPoint(swordBeam.transform.position).y > 0)
            {
                if (Physics.Raycast(swordBeam.transform.position, Vector3.up, out RaycastHit hit, 1.3f) && hit.collider.gameObject.CompareTag("enemy"))
                {
                    hit.transform.position += new Vector3(0, -0.2f, 0);
                    Destroy(hit.collider.gameObject);
                    yield return null;
                    Destroy(swordBeam, .2f);
                }
                swordBeam.transform.position += new Vector3(0, -0.2f, 0);
                yield return null;
            }
            Destroy(swordBeam, .2f);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkRight"))
        {
            swordBeam = Object.Instantiate(swordBeam, currPosition + new Vector3(.8f, 0, 0), Quaternion.identity);
            while (Camera.main.WorldToViewportPoint(swordBeam.transform.position).x < 1)
            {
                if (Physics.Raycast(swordBeam.transform.position, Vector3.right, out RaycastHit hit, 1.3f) && hit.collider.gameObject.CompareTag("enemy"))
                {
                    hit.transform.position += new Vector3(0.2f, 0, 0);
                    Destroy(hit.collider.gameObject);
                    yield return null;
                    Destroy(swordBeam, .2f);
                }

                swordBeam.transform.position += new Vector3(0.2f, 0, 0);
                yield return null;
            }
            Destroy(swordBeam, .2f);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkLeft"))
        {
            swordBeam = Object.Instantiate(swordBeam, currPosition + new Vector3(-.8f, 0, 0), Quaternion.identity);
            while (Camera.main.WorldToViewportPoint(swordBeam.transform.position).x > 0)
            {
                if (Physics.Raycast(swordBeam.transform.position, Vector3.right, out RaycastHit hit, 1.3f) && hit.collider.gameObject.CompareTag("enemy"))
                {
                    hit.transform.position += new Vector3(-0.2f, 0, 0);
                    Destroy(hit.collider.gameObject);
                    yield return null;
                    Destroy(swordBeam, .2f);
                }
                swordBeam.transform.position += new Vector3(-0.2f, 0, 0);
                yield return null;
            }
            Destroy(swordBeam, .2f);
        }

        yield return null;
        beamShot = false;
    }
}
