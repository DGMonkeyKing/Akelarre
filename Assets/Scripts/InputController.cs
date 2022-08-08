using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Sprite mouseSprite;
    private SpriteRenderer _spriteRenderer;

    Vector3 mousePosition;
    Vector3 mouseWorldPosition;

    private Camera camera = null;

    
    public GameObject cube;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        mousePosition = Mouse.current.position.ReadValue();

        _spriteRenderer.sprite = mouseSprite;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();

        Debug.Log(mousePosition);
/*  
        Ray ray = camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {

        }

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            
        }
*/
    }
    
}
