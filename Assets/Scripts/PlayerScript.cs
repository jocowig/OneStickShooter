using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    GameObject projectile;
    public GameObject healthBar;
    public float speed;
    public float currentHealth;
    public float maxHealth;
    Rigidbody rb;
    bool mouseDown;
    bool joystickMoving;
    bool usingJoystick;

    Vector3 lookPos;
    Vector3 lookDir;

    private float elapsedTime = 0f;
    public float fireRate = .25f;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        projectile = Resources.Load("Firework") as GameObject;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            usingJoystick = true;
            Debug.Log("Joystick moving");
            joystickMoving = true;
            transform.LookAt(transform.position + new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0), Vector3.forward);
        }
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            joystickMoving = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            usingJoystick = false;
            mouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }
        if (!usingJoystick) { 
            lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("Mouse position " + Input.mousePosition + " lookpos " + lookPos);
        lookPos.z = 0;
        lookDir = lookPos - transform.position;
        lookDir.z = 0;
        transform.LookAt(transform.position + lookDir, Vector3.forward);
    }
    }

    void FixedUpdate()
    {
        if(joystickMoving && elapsedTime > fireRate)
        {
            GameObject instProjectile = Instantiate(projectile) as GameObject;
            instProjectile.transform.rotation = Quaternion.LookRotation(transform.forward.normalized);
            instProjectile.transform.position = transform.Find("Projectile Shooter").transform.position;
            instProjectile.transform.GetComponent<FireworkScript>().dir = transform.forward.normalized;
            Vector3 negativeDir = new Vector3(-transform.forward.x, -transform.forward.y, 0);
            rb.velocity = (negativeDir * speed);
            elapsedTime = 0;
        }
        if (mouseDown && elapsedTime > fireRate)
        {
            GameObject instProjectile = Instantiate(projectile) as GameObject;
            instProjectile.transform.LookAt(transform.forward.normalized);
            instProjectile.transform.rotation = Quaternion.LookRotation(transform.forward.normalized);
            instProjectile.transform.position = transform.Find("Projectile Shooter").transform.position;
            instProjectile.transform.GetComponent<FireworkScript>().dir = lookDir.normalized;
            //Debug.Log("Projectile: " + instProjectile.transform.position);
            //Debug.Log("Player: " +transform.position);
            Vector3 negativeDir = new Vector3(-transform.forward.x, -transform.forward.y, 0);
            rb.velocity = (negativeDir * speed);
            elapsedTime = 0;
        }
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag.Equals("Enemy"))
        {
            reduceHealthBar();
        }
    }

    public void reduceHealthBar()
    {
        currentHealth--;
        healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("StartScreen");

        }

    }
}