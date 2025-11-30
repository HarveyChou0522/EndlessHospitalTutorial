using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSanity : MonoBehaviour
{
    public Image sanitySprite;
    public float sanity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        sanitySprite.color = new Color(0.41f, 0.01f, 0.01f, 1-sanity);
        InvokeRepeating("LostSanity", 1f, 1f);
    }

    private void Update()
    {
        sanitySprite.color = new Color(0.41f, 0.01f, 0.01f, 1 - sanity);
    }

    public void LostSanity()
    {
        sanity = Mathf.Clamp(sanity - 0.03f, 0, 1);

        if(sanity <= 0) GetComponent<PlayerMover>().PlayerDead();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pills"))
        {
            sanity = Mathf.Clamp(sanity + 0.03f , 0 , 1);
            
            Destroy(other.gameObject);
        }

    }
}
