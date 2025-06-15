using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{

    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth = 50;

    void Awake() //Imposto la salute iniziale
    {
        _currentHealth = _maxHealth;
    }

    
    public void TakeDamage(int dmg)
    {
        _currentHealth -= dmg;
        Debug.Log($"Danno subito: {dmg}. Salute restante: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Il giocatore è morto!");
            
        }
    }
}
