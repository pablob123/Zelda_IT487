using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour, Weapons
{
    Rigidbody rb;
    public int damage = 1;
    public int ammoCount;
    public bool hasAmmo;
    public bool hasWeapon = false;
    //0 = sword, 1 = bow, 2 = bomb, 3 = boomerang
    public string weaponType;
    private bool playLock = false;
    //private bool beamShot = false;
    public int currHealth;
    public Inventory inventory;
    private Animator _animator;
    SpriteRenderer sprite;
    [SerializeField]
    GameObject bowDown;
    [SerializeField]
    GameObject bowUp;
    [SerializeField]
    GameObject bowRight;
    [SerializeField]
    GameObject bowLeft;

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
        hasWeapon = inventory.GetBow();
        if (playLock == false)
        {
            Vector2 current_input = direction.GetInput();
            ammoCount = inventory.GetRupees();
            if (Input.GetKeyDown(KeyCode.Z) && hasWeapon == true && ammoCount > 0)
            {
                inventory.AddRupees(-1);
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkUp"))
                {
                    StartCoroutine(shootArrow(bowUp));
                }
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkDown"))
                {
                    StartCoroutine(shootArrow(bowDown));
                }
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkRight"))
                {
                    StartCoroutine(shootArrow(bowRight));
                }
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkLeft"))
                {
                    StartCoroutine(shootArrow(bowLeft));
                }
            }
        }
    }

    

    IEnumerator shootArrow(GameObject arrow)
    {
        Vector3 currPosition = transform.position;
        //GameObject swordBeam;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkUp"))
        {
            arrow = Object.Instantiate(arrow, currPosition + new Vector3(0, 1.0f, 0), Quaternion.identity);

            while (Camera.main.WorldToViewportPoint(arrow.transform.position).y < 0.8)
            {
                arrow.transform.position += new Vector3(0, 0.2f, 0);
                if (Physics.Raycast(arrow.transform.position, -Vector3.up, out RaycastHit hit, .7f))
                {
                    if (hit.collider.gameObject.CompareTag("enemy"))
                    {
                        hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                        if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        Destroy(arrow);
                        break;
                    }
                    if (hit.collider.gameObject.CompareTag("wall"))
                    {
                        Destroy(arrow);
                        break;
                    }
                    //break;
                }
                
                    yield return null;
            }
            Destroy(arrow);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkDown"))
        {
            arrow = Object.Instantiate(arrow, currPosition + new Vector3(0, -1.0f, 0), Quaternion.identity);
            while (Camera.main.WorldToViewportPoint(arrow.transform.position).y > 0)
            {
                arrow.transform.position += new Vector3(0, -0.2f, 0);
                if (Physics.Raycast(arrow.transform.position, Vector3.up, out RaycastHit hit, .7f))
                {
                    if (hit.collider.gameObject.CompareTag("enemy"))
                    {
                        hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                        if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        Destroy(arrow);
                        break;
                    }
                    if (hit.collider.gameObject.CompareTag("wall"))
                    {
                        Destroy(arrow);
                        break;
                    }
                }
                yield return null;
            }
            Destroy(arrow, .2f);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkRight"))
        {
            arrow = Object.Instantiate(arrow, currPosition + new Vector3(1.0f, 0, 0), Quaternion.identity);
            while (Camera.main.WorldToViewportPoint(arrow.transform.position).x < 1)
            {
                arrow.transform.position += new Vector3(0.2f, 0, 0);
                if (Physics.Raycast(arrow.transform.position, Vector3.right, out RaycastHit hit, .7f))
                {
                    if (hit.collider.gameObject.CompareTag("enemy"))
                    {
                        hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                        if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        Destroy(arrow);
                        break;
                    }
                    if (hit.collider.gameObject.CompareTag("wall"))
                    {
                        Destroy(arrow);
                        break;
                    }
                }
                yield return null;
            }
            Destroy(arrow, .2f);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkLeft"))
        {
            arrow = Object.Instantiate(arrow, currPosition + new Vector3(-1.0f, 0, 0), Quaternion.identity);
            while (Camera.main.WorldToViewportPoint(arrow.transform.position).x > 0)
            {
                arrow.transform.position += new Vector3(-0.2f, 0, 0);
                if (Physics.Raycast(arrow.transform.position, -Vector3.right, out RaycastHit hit, .7f))
                {
                    if (hit.collider.gameObject.CompareTag("enemy"))
                    {
                        hit.collider.GetComponent<HasHealth>().TakeDamage(damage);
                        if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() <= 0)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        Destroy(arrow);
                        break;
                    }
                    if (hit.collider.gameObject.CompareTag("wall"))
                    {
                        Destroy(arrow);
                        break;
                    }
                }
                yield return null;
            }
            Destroy(arrow, .2f);
        }

        yield return null;
    }

}
