using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //#####Private Variables #########
    Rigidbody rb;
    Animator animator;
    private bool holdItem = false;
    private bool grab = false;
    private bool nearItem = false;

    Vector3 dir;

    //#######Public Variables #########
    [Header("Move")]
    public float speedMovimento;
    public float speedRotation;
    public float force;

    [Header("Prefabs")]
    public GameObject lata;
    public GameObject alcool;
    public GameObject papel;
    public GameObject mascara;
    // Start is called before the first frame update
    void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity = input * speedMovimento *-1 * Time.deltaTime;
        if(rb.velocity != Vector3.zero)
        {
            animator.SetBool("Walk",true);
            dir = input;
            rb.rotation = Quaternion.LookRotation(dir*-1);
        }
        else
        {
            animator.SetBool("Walk",false);
        }
        rb.angularVelocity = Vector3.zero;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(nearItem && !holdItem)
            {
                Debug.Log("Grab True");
                grab = true;
                FindObjectOfType<AudioManager>().Play("Item");
            }
            else if(holdItem)
            {
                //Joga item no chão
                Debug.Log("Drop Item");

                GameObject go = this.gameObject.transform.GetChild(1).GetChild(0).gameObject;

                go.GetComponent<Rigidbody>().isKinematic = false;
                go.GetComponent<Rigidbody>().useGravity = true;
                go.GetComponent<Collider>().enabled = true;

                this.gameObject.transform.GetChild(1).GetChild(0).transform.parent = null;

                go.GetComponent<Rigidbody>().AddForce(dir*-1 * force,ForceMode.Impulse);

                animator.SetBool("Hold",false);
                holdItem = false;
            }
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.CompareTag("item"))
        {
            //Debug.Log("Colidiu Item");
            nearItem = true;
            if(grab)
            {
                grab = false;
                Debug.Log("Pega Item");
                //Pega item
                GameObject go = other.gameObject;

                if(other.gameObject.GetComponent<Rigidbody>() != null)
                {
                    Rigidbody goRb = go.GetComponent<Rigidbody>();
                    goRb.isKinematic = true;
                    goRb.useGravity = false;
                }
                go.GetComponent<Collider>().enabled = false;

                go.transform.SetParent(this.gameObject.transform.GetChild(1));
                go.transform.position = this.gameObject.transform.GetChild(1).position;

                animator.SetBool("Hold",true);
                holdItem = true;
            }
        }    
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.CompareTag("item"))
        {
            //Debug.Log("Colidiu Item");
            nearItem = true;
            if(grab)
            {
                grab = false;
                Debug.Log("Pega Item");
                //Pega item
                GameObject go = other.gameObject;

                if(other.gameObject.GetComponent<Rigidbody>() != null)
                {
                    Rigidbody goRb = go.GetComponent<Rigidbody>();
                    goRb.isKinematic = true;
                    goRb.useGravity = false;
                }
                go.GetComponent<Collider>().enabled = false;

                go.transform.SetParent(this.gameObject.transform.GetChild(1));
                go.transform.position = this.gameObject.transform.GetChild(1).position;

                animator.SetBool("Hold",true);
                holdItem = true;
            }
        }


        else if(other.gameObject.CompareTag("caixaLata"))
        {
            nearItem = true;
            if(grab)
            {
                grab = false;
                GameObject go = Instantiate(lata);

                if(go.gameObject.GetComponent<Rigidbody>() != null)
                {
                    Rigidbody goRb = go.GetComponent<Rigidbody>();
                    goRb.isKinematic = true;
                    goRb.useGravity = false;
                }
                go.GetComponent<Collider>().enabled = false;

                go.transform.SetParent(this.gameObject.transform.GetChild(1));
                go.transform.position = this.gameObject.transform.GetChild(1).position;

                animator.SetBool("Hold",true);
                holdItem = true;

            }
        }



        else if(other.gameObject.CompareTag("caixaAlcool"))
        {
            nearItem = true;
            if(grab)
            {
                grab = false;
                GameObject go = Instantiate(alcool);

                if(go.gameObject.GetComponent<Rigidbody>() != null)
                {
                    Rigidbody goRb = go.GetComponent<Rigidbody>();
                    goRb.isKinematic = true;
                    goRb.useGravity = false;
                }
                go.GetComponent<Collider>().enabled = false;

                go.transform.SetParent(this.gameObject.transform.GetChild(1));
                go.transform.position = this.gameObject.transform.GetChild(1).position;

                animator.SetBool("Hold",true);
                holdItem = true;

            }
        }



        else if(other.gameObject.CompareTag("caixaPapel"))
        {
            nearItem = true;
            if(grab)
            {
                grab = false;
                GameObject go = Instantiate(papel);

                if(go.gameObject.GetComponent<Rigidbody>() != null)
                {
                    Rigidbody goRb = go.GetComponent<Rigidbody>();
                    goRb.isKinematic = true;
                    goRb.useGravity = false;
                }
                go.GetComponent<Collider>().enabled = false;

                go.transform.SetParent(this.gameObject.transform.GetChild(1));
                go.transform.position = this.gameObject.transform.GetChild(1).position;

                animator.SetBool("Hold",true);
                holdItem = true;

            }
        }

        else if(other.gameObject.CompareTag("caixaMask"))
        {
            nearItem = true;
            if(grab)
            {
                grab = false;
                GameObject go = Instantiate(mascara);

                if(go.gameObject.GetComponent<Rigidbody>() != null)
                {
                    Rigidbody goRb = go.GetComponent<Rigidbody>();
                    goRb.isKinematic = true;
                    goRb.useGravity = false;
                }
                go.GetComponent<Collider>().enabled = false;

                go.transform.SetParent(this.gameObject.transform.GetChild(1));
                go.transform.position = this.gameObject.transform.GetChild(1).position;

                animator.SetBool("Hold",true);
                holdItem = true;

            }
        }
    }


    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.CompareTag("item"))
        {
            nearItem = false;
        }

        else if(other.gameObject.CompareTag("caixaLata"))
        {
            nearItem = false;
        }

        else if(other.gameObject.CompareTag("caixaAlcool"))
        {
            nearItem = false;
        }

        else if(other.gameObject.CompareTag("caixaPapel"))
        {
            nearItem = false;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("item"))
        {
            nearItem = false;
        }

        else if(other.gameObject.CompareTag("caixaLata"))
        {
            nearItem = false;
        }

        else if(other.gameObject.CompareTag("caixaAlcool"))
        {
            nearItem = false;
        }
    }
}
