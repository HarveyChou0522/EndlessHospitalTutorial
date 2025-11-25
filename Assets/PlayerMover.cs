using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public BoxCollider col;
    //public MapSpawner spawner;

    public GameObject playerVisual;
    public Animator visualAnim;

    public LayerMask blockedLayer;

    int line;
    bool isJumping;
    bool isCrouching;

    Vector3 targetPos;

    private void Start()
    {
        targetPos = playerVisual.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        HandleHorizontalMove();
        HandleJump();
        HandleCrouch();
    }

    void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            //StopAllCoroutines();
            if(!isJumping) StartCoroutine(nameof(Jump));
        }
    }

    IEnumerator Jump()
    {
        StopCoroutine(nameof(Crouch));
        isCrouching = false;

        visualAnim.SetTrigger("Jump");

        isJumping = true;
        
        col.center = new Vector3(col.center.x, 1, col.center.z);
        yield return new WaitForSeconds(.7f);
        //yield return new WaitUntil(() => spawner.mapMoveSpeed * Time.de);
        col.center = new Vector3(col.center.x, 0, col.center.z);
        isJumping = false;
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopAllCoroutines();
            isJumping=false;

            StartCoroutine(nameof(Crouch));
        }
    }

    IEnumerator Crouch()
    {
        visualAnim.SetTrigger("Crouch");

        col.center = new Vector3(col.center.x, -1, col.center.z);
        yield return new WaitForSeconds(.7f);
        
        col.center = new Vector3(col.center.x, 0, col.center.z);
        
    }

    void HandleHorizontalMove()
    {
        //Debug.Log(Physics.BoxCast(transform.position + col.center, new Vector3(.2f, .3f, .1f), this.transform.forward * -1f, Quaternion.identity, .5f, blockedLayer));

        if (Input.GetKeyDown(KeyCode.D))
        {

            
            if(Physics.BoxCast(transform.position + col.center, new Vector3(.2f,.3f,.2f) , this.transform.forward * -1f , Quaternion.identity , .5f , blockedLayer))
            {
                
                return;
            }

            if(line < 1)
            {
                targetPos.z -= 1f;

                line++;
                col.center -= new Vector3(0, 0, 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (Physics.BoxCast(transform.position + col.center, new Vector3(.2f, .3f, .2f), this.transform.forward * 1f, Quaternion.identity, .5f, blockedLayer))
            {

                return;
            }

            if (line > -1)
            {
                targetPos.z += 1f;

                line--;
                col.center += new Vector3(0, 0, 1);
            }
        }

        playerVisual.transform.position = Vector3.Lerp(playerVisual.transform.position, targetPos , Time.deltaTime * 50f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pills"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Hit");

            //var mover = FindObjectsByType<MapMover>(FindObjectsSortMode.None);
            MapSpawner.instance.StopMoveMap();
            visualAnim.Play("Dead");

        }
    }


}
