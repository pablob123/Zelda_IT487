using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour, Weapons
{
    Rigidbody rb;
    public int damage = 1;
    private float distance = 4f;
    private float time = 1.5f;
    public bool hasWeapon = false;
    public bool isThrown = false;
    //0 = sword, 1 = bow, 2 = bomb, 3 = boomerang
    public string weaponType;
    private bool playLock = false;
    //private bool beamShot = false;
    public int currHealth;
    public Inventory inventory;
    private Animator _animator;
    SpriteRenderer sprite;
    [SerializeField]
    GameObject boomerangLeft;
    [SerializeField]
    GameObject boomerangDiagonal;
    [SerializeField]
    GameObject boomerangDown;

    ArrowKeyMovement direction;

    public Transform player;

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
        hasWeapon = inventory.GetBoomerang();
        if (playLock == false)
        {
            Vector2 current_input = direction.GetInput();
            if (Input.GetKeyDown(KeyCode.Z) && hasWeapon == true)
            {
                StartCoroutine(throwBoomerang(boomerangLeft));
            }
        }
    }


    IEnumerator throwBoomerang(GameObject boomerang)
    {
        Vector3 currPosition = transform.position;

        float elapsedTime = 0.0f;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkUp") && isThrown == false)
        {
            isThrown = true;
            boomerang = Object.Instantiate(boomerang, currPosition + new Vector3(0, 1.0f, 0), Quaternion.identity);
            float newPositionY = boomerang.transform.position.y + distance;
            Vector3 newPosition = new Vector3(boomerang.transform.position.x, newPositionY);

            while (Vector3.Distance(boomerang.transform.position, newPosition) > .2f)
            {
                boomerang.transform.position = Vector3.Lerp(boomerang.transform.position, newPosition, elapsedTime / time);
                if (Physics.Raycast(boomerang.transform.position, -Vector3.up, out RaycastHit hit, .7f))
                {
                    if (hit.collider.gameObject.CompareTag("enemy"))
                    {
                        if (hit.collider.GetComponent<frozenByBoomerang>() != null)
                        {
                            hit.collider.GetComponent<frozenByBoomerang>().freeze();
                        }

                        if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() == 0)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        break;
                    }
                    if (hit.collider.gameObject.CompareTag("wall"))
                    {
                        break;
                    }
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            StartCoroutine(boomerangReturn(boomerang));
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkDown") && isThrown == false)
        {
            isThrown = true;
            boomerang = Object.Instantiate(boomerang, currPosition + new Vector3(0, -1.0f, 0), Quaternion.identity);
            float newPositionY = boomerang.transform.position.y - distance;
            Vector3 newPosition = new Vector3(boomerang.transform.position.x, newPositionY);

            while (Vector3.Distance(boomerang.transform.position, newPosition) > .2f)
            {
                boomerang.transform.position = Vector3.Lerp(boomerang.transform.position, newPosition, elapsedTime / time);
                if (Physics.Raycast(boomerang.transform.position, Vector3.up, out RaycastHit hit, .7f))
                {
                    if (hit.collider.gameObject.CompareTag("enemy"))
                    {

                        if (hit.collider.GetComponent<frozenByBoomerang>() != null)
                        {
                            hit.collider.GetComponent<frozenByBoomerang>().freeze();
                        }

                        if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() == 1)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        break;
                    }
                    if (hit.collider.gameObject.CompareTag("wall"))
                    {
                        break;
                    }
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            StartCoroutine(boomerangReturn(boomerang));
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkRight") && isThrown == false)
        {
            isThrown = true;
            boomerang = Object.Instantiate(boomerang, currPosition + new Vector3(1.0f, 0, 0), Quaternion.identity);
            float newPositionX = boomerang.transform.position.x + distance;
            Vector3 newPosition = new Vector3(newPositionX, boomerang.transform.position.y);

            while (Vector3.Distance(boomerang.transform.position, newPosition) > .2f)
            {
                boomerang.transform.position = Vector3.Lerp(boomerang.transform.position, newPosition, elapsedTime / time);
                if (Physics.Raycast(boomerang.transform.position, Vector3.right, out RaycastHit hit, .7f))
                {
                    if (hit.collider.gameObject.CompareTag("enemy"))
                    {
                        //hit.collider.GetComponent<HasHealth>().TakeDamage(damage);

                        if (hit.collider.GetComponent<frozenByBoomerang>() != null)
                        {
                            hit.collider.GetComponent<frozenByBoomerang>().freeze();
                        }

                        if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() == 1)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        break;
                    }
                    if(hit.collider.gameObject.CompareTag("wall"))
                    {
                        break;
                    }
                    break;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            StartCoroutine(boomerangReturn(boomerang));
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkLeft") && isThrown == false)
        {
            isThrown = true;
            boomerang = Object.Instantiate(boomerang, currPosition + new Vector3(-1.0f, 0, 0), Quaternion.identity);
            float newPositionX = boomerang.transform.position.x - distance;
            Vector3 newPosition = new Vector3(newPositionX, boomerang.transform.position.y);

            while (Vector3.Distance(boomerang.transform.position, newPosition) > .2f)
            {
                boomerang.transform.position = Vector3.Lerp(boomerang.transform.position, newPosition, elapsedTime / time);
                if (Physics.Raycast(boomerang.transform.position, -Vector3.right, out RaycastHit hit, .7f))
                {
                    if (hit.collider.gameObject.CompareTag("enemy"))
                    {

                        if (hit.collider.GetComponent<frozenByBoomerang>() != null)
                        {
                            hit.collider.GetComponent<frozenByBoomerang>().freeze();
                        }

                        if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() == 1)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        break;
                    }
                    if (hit.collider.gameObject.CompareTag("wall"))
                    {
                        break;
                    }
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            StartCoroutine(boomerangReturn(boomerang));
        }

    }
    IEnumerator boomerangReturn(GameObject boomerang)
    {
        while (Vector3.Distance(boomerang.transform.position, player.transform.position) > 0.2f)
        {
            boomerang.transform.position += (player.transform.position - boomerang.transform.position).normalized * .2f;
            if (Physics.Raycast(boomerang.transform.position, -Vector3.right, out RaycastHit hit, .7f))
            {
                if (hit.collider.gameObject.CompareTag("enemy"))
                {

                    hit.collider.GetComponent<frozenByBoomerang>().freeze();

                    if (hit.collider.GetComponent<HasHealth>().GetCurrentHealth() == 1)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    break;
                }
            }

            yield return null;
        }
        Destroy(boomerang);

        isThrown = false;
        yield return null;

    }
}

