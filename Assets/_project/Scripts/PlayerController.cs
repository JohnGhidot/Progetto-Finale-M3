using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerAnimation playerAnimation;

    [Header("Movimento")]
    [SerializeField] private float speed = 5f;

    private float h; 
    private float v; 
    private Vector2 currentMoveDirection; 
    
    [SerializeField] private Transform _gunEquipPoint; 
    private Gun _currentGun; 
    
    public bool _isFacingRight = true;

    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (playerAnimation == null)
        {
            playerAnimation = GetComponent<PlayerAnimation>();
        }
                
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        currentMoveDirection = new Vector2(h, v);

        bool isMoving = currentMoveDirection.magnitude > 0.1f;

       playerAnimation.UpdateAnimations(isMoving, currentMoveDirection);

        if (h > 0.01f)
        {
            _isFacingRight = true;
        }
        else if (h < -0.01f)
        {
            _isFacingRight = false;
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + currentMoveDirection.normalized * (speed * Time.fixedDeltaTime));
    }

    public void EquipGun(GameObject _gunPrefab)
    {
        if (_currentGun != null && _currentGun.gameObject != null)
        {
            Destroy(_currentGun.gameObject);
            _currentGun = null;
        }

        GameObject newGun = Instantiate(_gunPrefab);
        newGun.transform.parent = _gunEquipPoint;
        newGun.transform.localPosition = Vector3.zero;

        _currentGun = newGun.GetComponent<Gun>();
        if (_currentGun != null)

        {
            _currentGun.SetEquippedStatus(true);
            Debug.Log("Arma equipaggiata: " + newGun.name);
        }
        else
        {
            Destroy(newGun);
        }
    }
}
