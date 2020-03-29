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
    public GameObject placa;
    public float speed;

    bool canWalk;
    Vector3 stopPos;
    Quaternion stopRot;

    public GameObject avatar;
    public Color[] colors;

    [Header("Pedidos")]
    public Texture[] pedidos;
    public string[] namePedidos;

    List<Collider> ragdollParts = new List<Collider>();

    public string pedido;
    //List<string> pedidos = new List<string>();
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
        avatar.GetComponent<Renderer>().material.color = colors[Random.Range(0,colors.Length)];
        int random = Random.Range(0,pedidos.Length);
        this.gameObject.transform.GetChild(2).GetComponent<Renderer>().material.mainTexture = pedidos[random];
        placa.SetActive(false);
        pedido = namePedidos[random];
        canWalk = true;
        collider.isTrigger = true;
        rigidbody.useGravity = false;
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
            //transform.name = transform.name.Replace("(clone)","").Trim();
            Debug.Log(other.gameObject.name.Replace("(Clone)",""));
            if(other.gameObject.name.Replace("(Clone)","") == pedido)
            {
                FindObjectOfType<AudioManager>().Play("Impacto");
                FindObjectOfType<AudioManager>().Play("Acertou");
                Destroy(placa);
                item = other.gameObject;
                Destroy(other.gameObject);
                TurnOnRagdoll();
                addRagForce();
                Invoke("Levantar",3f);
            }

            else
            {
                FindObjectOfType<AudioManager>().Play("Errou");
                Destroy(other.gameObject);
                rigidbody.velocity = Vector3.zero;
                transform.position = stopPos;
                transform.rotation = stopRot;
            }
            //Vector3 direction = other.transform.position + this.transform.position;
            //rigidbody.AddForce(direction.normalized* -1 * 30000,ForceMode.Impulse);
        }

        if(other.gameObject.CompareTag("npc"))
        {
            canWalk = false;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.CompareTag("npc"))
        {
            canWalk = true;
        }   
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.CompareTag("Stop"))
        {
            placa.SetActive(true);
            canWalk = false;
            collider.isTrigger = false;
            rigidbody.useGravity = true;
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
            if(r.gameObject != this.gameObject && r.gameObject.name == "mixamorig_Hips")
            {
                Vector3 direction = Vector3.up;
                //r.AddForce(direction.normalized*30,ForceMode.Force);
                //r.velocity = direction;
            }
        }
    }

    void Levantar()
    {
        //collider.enabled = true;
        //animator.enabled = true;
        //GetRigidbodys();
        //rigidbody.useGravity = true;
        //rigidbody.velocity = Vector3.zero;
        //transform.position = stopPos;
        //transform.rotation = stopRot;
        Destroy(this.gameObject);

    }
}
