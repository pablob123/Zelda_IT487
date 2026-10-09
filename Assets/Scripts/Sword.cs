using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour, Weapons
{
    Rigidbody rb;
    public int damage = 1;
    public bool hasAmmo;
    public bool hasWeapon = true;
    //0 = sword, 1 = bow, 2 = bomb, 3 = boomerang
    public string weaponType;
    private bool playLock = false;
    private bool beamShot = false;
    public int currHealth;
    public HasHealth health;
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

    ArrowKeyMovement direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = GetComponent<ArrowKeyMovement>();
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playLock == false)
        {
            Vector2 current_input = direction.GetInput();
            currHealth = health.GetCurrentHealth();
            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(playerAttack(current_input));
            }
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
                hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                if(hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                {
                    Destroy(hit.collider.gameObject);
                }

            }
            //Deletes sword sprite
            Destroy(sword, .2f);
            if (beamShot == false && currHealth == 6)
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
                hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            Destroy(sword, .2f);
            if (beamShot == false && currHealth == 6)
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
                hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            Destroy(sword, .2f);
            if (beamShot == false && currHealth == 6)
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
                hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            Destroy(sword, .2f);
            if (beamShot == false && currHealth == 6)
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
        //GameObject swordBeam;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkUp"))
        {
            swordBeam = Object.Instantiate(swordBeam, currPosition + new Vector3(0, .8f, 0), Quaternion.identity);

            while (Camera.main.WorldToViewportPoint(swordBeam.transform.position).y < 0.8)
            {
                swordBeam.transform.position += new Vector3(0, 0.2f, 0);
                if (Physics.Raycast(swordBeam.transform.position, -Vector3.up, out RaycastHit hit, .7f) && hit.collider.gameObject.CompareTag("enemy"))
                {
                    hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                    if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    Destroy(swordBeam);
                    break;
                }
                yield return null;
            }
            Destroy(swordBeam, .2f);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkDown"))
        {
            swordBeam = Object.Instantiate(swordBeam, currPosition + new Vector3(0, -.8f, 0), Quaternion.identity);
            while (Camera.main.WorldToViewportPoint(swordBeam.transform.position).y > 0)
            {
                swordBeam.transform.position += new Vector3(0, -0.2f, 0);
                if (Physics.Raycast(swordBeam.transform.position, Vector3.up, out RaycastHit hit, .7f) && hit.collider.gameObject.CompareTag("enemy"))
                {
                    hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                    if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    Destroy(swordBeam);
                    break;
                }
                yield return null;
            }
            Destroy(swordBeam, .2f);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkRight"))
        {
            swordBeam = Object.Instantiate(swordBeam, currPosition + new Vector3(.8f, 0, 0), Quaternion.identity);
            while (Camera.main.WorldToViewportPoint(swordBeam.transform.position).x < 1)
            {
                swordBeam.transform.position += new Vector3(0.2f, 0, 0);
                if (Physics.Raycast(swordBeam.transform.position, Vector3.right, out RaycastHit hit, .7f) && hit.collider.gameObject.CompareTag("enemy"))
                {
                    hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                    if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    Destroy(swordBeam);
                    break;
                }
                yield return null;
            }
            Destroy(swordBeam, .2f);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkLeft"))
        {
            swordBeam = Object.Instantiate(swordBeam, currPosition + new Vector3(-.8f, 0, 0), Quaternion.identity);
            while (Camera.main.WorldToViewportPoint(swordBeam.transform.position).x > 0)
            {
                swordBeam.transform.position += new Vector3(-0.2f, 0, 0);
                if (Physics.Raycast(swordBeam.transform.position, -Vector3.right, out RaycastHit hit, .7f) && hit.collider.gameObject.CompareTag("enemy"))
                {
                    hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                    if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    Destroy(swordBeam);
                    break;
                }
                yield return null;
            }
            Destroy(swordBeam, .2f);
        }

        yield return null;
        beamShot = false;
    }


    //public override string returnWeaponType()
    //{
    //    return "Sword";
    //}
}
