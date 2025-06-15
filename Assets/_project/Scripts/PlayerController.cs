using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    private float h;
    private float v;
    Vector2 Direction;

    [SerializeField] Transform _gunEquipPoint;

    private Gun _currentGun;

    private bool _isFacingRight = true;

    [SerializeField] private PlayerAnimation playerAnimation;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();// prendo il PlayerAnimation del player                 
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector2 moveInput = new Vector2(h, v);
        bool isMoving = moveInput.magnitude > 0.1f; 

        playerAnimation.UpdateAnimations(isMoving, moveInput);

        if (h > 0.01f) //controllo se player va a destra o sinistra
        {
            _isFacingRight = true;
        }
        else if (h < -0.01f) 
        {
            _isFacingRight = false;
        }
        

        //ApplyFlipping(); //gira arma
    }

    private void FixedUpdate()
    {
        Direction = new Vector2(h, v);
        rb.MovePosition(rb.position + Direction *(speed * Time.deltaTime));
    }

    public void EquipGun(GameObject _gunPrefab)
    {
        if (_currentGun != null && _currentGun.gameObject != null) //se c'è un'arma equipaggiata, la distruggo
        {
            Destroy(_currentGun.gameObject); 
            _currentGun = null;
        }

        GameObject newGun = Instantiate(_gunPrefab);

        newGun.transform.parent = _gunEquipPoint;
        newGun.transform.localPosition = Vector3.zero;

        if (_currentGun != null)
        {
            _currentGun.SetEquippedStatus(true);
        }

        _currentGun = newGun.GetComponent<Gun>();
        Debug.Log("Arma equipaggiata: " + newGun.name);

        if (_currentGun == null)
        {
            
            Destroy(newGun);
        }
        else
        {
            _currentGun.SetPlayerReference(this);
            _currentGun.SetEquippedStatus(true);            
        }
    }

    
    public bool IsFacingRight //potrò accederci in lettura dalle altre classi
    {
        get { return _isFacingRight; }
    }

}
