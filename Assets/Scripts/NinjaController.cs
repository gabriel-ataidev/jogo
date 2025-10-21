using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class NinjaController : MonoBehaviour
{
    Rigidbody2D _rb;
    float xDir;
    [SerializeField] float xSpeed;
    Animator animator;
    
    void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void OnMove(InputValue inputValue)
    {
        xDir = inputValue.Get<Vector2>().x;
    }
    void Movimentar()
    {
        _rb.linearVelocityX = xDir * xSpeed * Time.deltaTime;
    }
    
    void FixedUpdate()
    {
        Movimentar();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Fruit 1"))
        {
            GameManager.Fruit1Count++;
            Debug.Log("Fruit 1 coletada! Total: " + GameManager.Fruit1Count);

            if (GameManager.Fruit1Count >= 10)
            {
                Debug.Log("Você coletou 10 Fruit 1! Fim de jogo.");
                StartCoroutine(close());
            }
        }
        else
        {
            animator.enabled = true;
            StartCoroutine(close());
        }
        
        Destroy(other.gameObject);
    }

    IEnumerator close()
    {
        yield return new WaitForSeconds(3f);
        // Encerrar jogo aqui
        Application.Quit();
    }
}
