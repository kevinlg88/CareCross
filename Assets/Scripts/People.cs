using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{

    Animator animator;
    Collider collider;
    Rigidbody rigidbody;
    public GameObject item;

    public GameObject mascara;
    public float speed;

    bool canWalk;
    Vector3 stopPos;
    Quaternion stopRot;

    List<Collider> ragdollParts = new List<Collider>();
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        animator = this.gameObject.GetComponent<Animator>();
        collider = this.gameObject.GetComponent<Collider>();
        GetRigidbodys();
    }

    private void Start() 
    {
        canWalk = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(canWalk)
        {
            animator.SetBool("Walk",true);
            rigidbody.velocity = Vector3.right * speed * Time.deltaTime;
            rigidbody.rotation = Quaternion.LookRotation(Vector3.right);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            TurnOnRagdoll();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("People Colidiu");
        if(other.gameObject.CompareTag("item"))
        {
            item = other.gameObject;
            Destroy(other.gameObject);
            TurnOnRagdoll();
            addRagForce();
            Invoke("Levantar",4f);
            //Vector3 direction = other.transform.position + this.transform.position;
            //rigidbody.AddForce(direction.normalized* -1 * 30000,ForceMode.Impulse);

        }    
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Stop"))
        {
            canWalk = false;
            stopPos = transform.position;
            stopRot = transform.rotation;
            animator.SetBool("Walk",false);
        }    
    }

    void GetRigidbodys()
    {
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
        foreach(Collider c in colliders)
        {
            if(c.gameObject != this.gameObject)
            {
                c.isTrigger = true;
                ragdollParts.Add(c);
            }
        }
    }

    void TurnOnRagdoll()
    {
        mascara.SetActive(true);
        rigidbody.useGravity = false;
        //rigidbody.AddForce(Vector3.up * 2000*-1, ForceMode.Impulse);
        //rigidbody.velocity = Vector3.zero;
        collider.enabled = false;
        animator.enabled = false;

        foreach(Collider c in ragdollParts)
        {
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
        }

    }

    void addRagForce()
    {
        Rigidbody[] rbs = this.gameObject.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody r in rbs)
        {
            if(r.gameObject != this.gameObject)
            {
                Vector3 direction = Vector3.left;
                r.AddForce(direction.normalized*30,ForceMode.Impulse);
            }
        }
    }

    void Levantar()
    {
        //collider.enabled = true;
        animator.enabled = true;
        GetRigidbodys();
        //rigidbody.useGravity = true;
        rigidbody.velocity = Vector3.zero;
        transform.position = stopPos;
        transform.rotation = stopRot;
        Destroy(this.gameObject,4f);

    }
}
