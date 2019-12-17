using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballscript : MonoBehaviour
{

    public bool dead = false;
    public bool targetEaten = false;
    public int combo_bonus = 1;
    public int breaking_num;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemey"))
        {
            collision.gameObject.SetActive(false);
            targetEaten = true;
            breaking_num += 1;
            combo_bonus = 1;
        }
        else if (collision.gameObject.CompareTag("dead"))
        {
            dead = true;
            combo_bonus = 1;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            combo_bonus = 1;
        }
    }
}
