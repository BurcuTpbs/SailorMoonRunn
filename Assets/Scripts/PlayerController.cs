using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float speed = 3f;
    [SerializeField] UIManager uiManager;
    public float swerveSpeed = 20f;
    private Vector2 _deltaTouchStart, _deltaTouchEnd, _dragDelta;
    private float _slideFactor = 0.1f;
    [SerializeField] private Vector2 boundaries;


    private void Start()
    {
        StartCoroutine(nameof(MoveForwardRoutine));
    }



    IEnumerator MoveForwardRoutine()
    {
        while (true)
        {
            playerTransform.position = Vector3.Lerp(playerTransform.position, playerTransform.position + Vector3.forward,
               speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("i will get the weapon");
        if (other.CompareTag("Collectable"))
        {
            Debug.Log("Collected");
            other.gameObject.SetActive(false);
            uiManager.score += 10;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTouchStart();
        }


        if (Input.GetMouseButton(0))
        {
            DragDeltaInput();
        }
    }

    void SetTouchStart()
    {
        _deltaTouchStart = Input.mousePosition;
        _deltaTouchEnd = Input.mousePosition;
    }

 
    void DragDeltaInput()
    {
        Debug.Log("deltaDragInput");
        _deltaTouchStart = Input.mousePosition;
        _dragDelta.x = (_deltaTouchEnd.x - _deltaTouchStart.x) * _slideFactor;
        _deltaTouchEnd = _deltaTouchStart;
        Swerve();
    }

   
    private float _positionOnX = 0;
    void Swerve()
    {
        Debug.Log("swerve");
        _positionOnX = transform.position.x;
        _positionOnX = Mathf.Lerp(_positionOnX, _positionOnX - _dragDelta.x,
           Time.deltaTime * swerveSpeed);
        _positionOnX = Mathf.Clamp(_positionOnX, boundaries.x, boundaries.y); //boundrileri tek yerden belirleyebilmek için v2
        Vector3 newPosOnX = new Vector3(_positionOnX, transform.position.y, transform.position.z);
        transform.position = newPosOnX;
    }



}//class
