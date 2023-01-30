using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthScript : MonoBehaviour
{
    public int health = 3;
    int maxHealth;
    gameObject Player;

    void start(){
        Player = GameObject.FindGameObjectWithTag("Player");
        maxHealth == health;
    }

    void update(){
    
    }

    public void takeDamage(){
        health--;
    }

    public void IncreaseHealth(int value){
        if(health < maxHealth){
            health += value;
        }
    }

    public void die(){
        if(health <= 0 && Player){
            debug.log("DEAD!!!!!!!!!!!!!!!!!!!!!!!!!!!")
        }
    }

    public int returnHealth(){
        return health;
    }
}
