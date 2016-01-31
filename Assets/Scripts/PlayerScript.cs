using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    GameObject projectile;
    public GameObject healthBar;
    public float speed;
    public float currentHealth;
    public float maxHealth;
    Rigidbody rb;
    bool mouseDown;

    Vector3 lookPos;
    Vector3 lookDir;

    private float elapsedTime = 0f;
    public float fireRate = .25f;
   
	// Use this for initialization
	    void Start ()
    {
        currentHealth = maxHealth;
        projectile = Resources.Load("Firework") as GameObject;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
                mouseDown = true;
            
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("Mouse position " + Input.mousePosition + " lookpos " + lookPos);
        lookPos.z = 0;
        lookDir = lookPos - transform.position;
        lookDir.z = 0;
        transform.LookAt(transform.position + lookDir, Vector3.forward);
        
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        rb.AddForce(movement * speed / Time.deltaTime);
        if(mouseDown && elapsedTime > fireRate)
        {
            GameObject instProjectile = Instantiate(projectile) as GameObject;
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
            currentHealth--;
            reduceHealthBar(currentHealth/ maxHealth);
            if (currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void reduceHealthBar(float remainingHealth)
    {
        healthBar.transform.localScale = new Vector3(remainingHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
