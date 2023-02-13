using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    public int switchType = 0;
    public GameObject Door;
    public GameObject Platform;
    public GameObject Lava;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        MovingPlatform mp = Platform.GetComponent<MovingPlatform>();
        lavaBehaviour lb = Lava.GetComponent<lavaBehaviour>();
        playerMovementBehaviour pb = player.GetComponent<playerMovementBehaviour>();
        if (collision.gameObject.Equals(player))
        {
            if (switchType == 0)
            {
                Door.SetActive(false);
            }
            if (switchType == 1)
            {
                mp.active = true;
            }
            if (switchType == 2)
            {
                lb.lavaSpeed = 0;
                lb.Invoke("LavaReset", 5f);
            }
        }
    }
}
