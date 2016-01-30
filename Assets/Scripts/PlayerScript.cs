using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    GameObject projectile;
    public float speed;
    Rigidbody rb;
    bool mouseDown;

    Vector3 lookPos;
    Vector3 lookDir;

    private float elapsedTime = 0f;
    public float fireRate = .25f;
   
	// Use this for initialization
	    void Start ()
    {
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }
        lookDir = lookPos - transform.position;
        lookDir.y = 0;
        transform.LookAt(transform.position + lookDir, Vector3.up);
        
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
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.AddForce(lookDir * speed / Time.deltaTime);
            Debug.Log("Projectile: " + instProjectile.transform.position);
            Debug.Log("Player: " +transform.position);
            Vector3 negativeDir = new Vector3(-lookDir.x, 0, -lookDir.z);
            rb.AddForce(negativeDir * speed / Time.deltaTime);
            elapsedTime = 0;
        }
    }
}
