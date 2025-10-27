using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 5.0f;
    public float fuerzaSalto = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;
    public Animator animator;
    private bool enSuelo;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float velocidadX = Input.GetAxis("Horizontal")*Time.deltaTime*velocidad;

        animator.SetFloat("movement", velocidadX*velocidad);

        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Vector3 posicion = transform.position;

        transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);

        enSuelo = hit.collider != null;
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }

        animator.SetBool("ensuelo", enSuelo);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - longitudRaycast, transform.position.z));
    }
   



    
}




