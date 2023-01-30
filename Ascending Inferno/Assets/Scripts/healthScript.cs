using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthScript : MonoBehaviour
{
    public int health = 3;

    void start(){

    }

    public void takeDamage(){
        health--;
    }

    public int returnHealth(){
        return health;
    }
}
