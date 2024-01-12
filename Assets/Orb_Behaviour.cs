using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.GraphicsBuffer;

public class Orb_Behaviour : MonoBehaviour
{
    public Transform player;
    float t = 0;
    private void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log(distance.ToString());
      
        t += Time.fixedDeltaTime/2;
        if (distance > 1f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, player.transform.position, t);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
