using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpGain : MonoBehaviour
{
    [SerializeField] int frmPerTp;
    [SerializeField] PlayerTp p;
    bool inTrigger;
    Collider2D other;

    // Start is called before the first frame update
    void Start()
    {
        if (frmPerTp <= 0) frmPerTp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (inTrigger)
        {
            p.AddTp(Vector2.Distance(p.transform.position, other.transform.position)) ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            inTrigger = true;
            other = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            inTrigger = false;
            p.TpRadius(false);
        }
    }


}
