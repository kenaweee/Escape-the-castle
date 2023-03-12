using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class Throwing : MonoBehaviour
{
    public Text thetext;

    public Transform cam;

    public Transform attackPoint;

    public GameObject objectToThrow;

    public float throwCooldown;


    public GameObject gameover;

    float throwForce = 5.0f;

    float throwUpwardForce = 5.0f;

    bool readyToThrow = false;
    bool readytostop = false;

    public GameObject door;

   bool Throwedthebomb = false;

    public GameObject rotatingfloor;
    public GameObject rotatingfloor2;

    bool rotate1 = true;
    bool rotate2 = true;

    bool doorfound = false;

    private GameObject projectile;

    public GameObject HUD;

    public GameObject explosion;

    private void Start()
    {
        
    }

    private void Update()
    {
        

        if(rotate1)
        rotatingfloor.transform.Rotate(300 * Time.deltaTime, 0, 0);
        if (rotate2)
            rotatingfloor2.transform.Rotate(300 * Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.Alpha1)&& !pausing.ispaused)
        {
            thetext.text = "Remote Bomb";
            readyToThrow = true;
            readytostop = false;
            rotate1 = true;
            rotate2 = true;
            CancelInvoke("normal");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)&& !pausing.ispaused)
        {
            thetext.text = "Stasis";
            readyToThrow = false;
            readytostop = true;
            Throwedthebomb = false;
            if(GameObject.FindWithTag("bomb")!=null)
            Destroy(GameObject.FindWithTag("bomb"));
        }


        if (Input.GetKeyDown(KeyCode.Q) && readyToThrow &&!readytostop && Throwedthebomb==false && !pausing.ispaused)
        {
            readyToThrow = false;
            this.GetComponent<Animator>().SetTrigger("throwingbomb");

            Invoke(nameof(Throw), 0.5f);
            
        }
        if (Input.GetKeyDown(KeyCode.Q) && Throwedthebomb == true && !pausing.ispaused)
        {
            if (projectile != null)
            {
                Collider[] hitColliders = Physics.OverlapSphere(projectile.transform.position, 2.5f);
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].CompareTag("door"))
                    {
                        doorfound = true;
                        break;
                    }
                }
            }
            Throwedthebomb = false;
            if (doorfound == true)
            {
                Destroy(GameObject.FindWithTag("door"));
            }

            //door.SetActive(false);
            if(projectile!=null)
            Instantiate(explosion, projectile.transform.position, cam.rotation);
            Destroy(GameObject.FindWithTag("bomb"));
        }
        if (Input.GetKeyDown(KeyCode.Q) && readytostop == true && rotate1 && rotate2 && !pausing.ispaused)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].CompareTag("rotatingfloor"))
                {
                    rotate1 = false;

                    Invoke("normal", 10);
                    break;
                }
                else if (hitColliders[i].CompareTag("rotatingfloor2"))
                {
                    rotate2 = false;

                    Invoke("normal", 10);
                    break;
                }
            }

           
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("death"))
        {
            this.GetComponent<Animator>().SetTrigger("dead");
            HUD.SetActive(false);
            Invoke("dying", 3);
        }
        if (other.CompareTag("goal"))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void dying()
    {
        Time.timeScale = 0f;
        gameover.SetActive(true);
    }
   
    private void normal()
    {
        rotate1 = true;
        rotate2 = true;
    }

    private void Throw()
    {
       

        

        
         projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

       
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

       
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

       



        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
        Throwedthebomb = true;
    }
}