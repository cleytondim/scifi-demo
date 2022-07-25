using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    private CharacterController _controller;
    private NavMeshAgent _navmesh;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = 9.81f;

    [SerializeField]
    private GameObject _flash;

    [SerializeField]
    private GameObject _hitMarkerPrefab;

    [SerializeField]
    private AudioSource _shotAudioSource;

    [SerializeField]
    private int _currentAmmo;
    [SerializeField]
    private int _maxAmmo = 50;
    [SerializeField]
    private int _currentLoads;
    [SerializeField]
    private int _maxLoads = 5;

    [SerializeField]
    private bool _reloading = false;

    private UIManager _uiManager;

    public bool haveCoin = false;

    [SerializeField]
    private GameObject _weapon;


    // Start is called before the first frame update
    void Start()
    {
        //_controller = GetComponent<CharacterController>();
        _navmesh = GetComponent<NavMeshAgent>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //_flash = GameObject.Find("Muzzle_Flash");

        //_shotAudioSource = GetComponent<AudioSource>();
        _currentAmmo = _maxAmmo;
        _currentLoads = _maxLoads;
        _reloading = false;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //Physics.SyncTransforms();
        Ray();
        CheckEsc();
        CalculateMovement();
        //Jump();
        Reload();
        //Shoot();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jumpou");
            _navmesh.enabled = true;
            Vector3 direction = new Vector3(0, 20, 0);
            transform.Translate(direction);
            //_navmesh.Move(direction);
            _navmesh.enabled = false;
            
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        velocity = transform.TransformDirection(velocity);
        _navmesh.Move(velocity * Time.deltaTime);
        //_controller.Move(velocity * Time.deltaTime);

    }

    /*void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _flash.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            _flash.SetActive(false);
        }
    }*/


    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && _currentLoads>0 && !_reloading)
        {
            StartCoroutine(DoReload());
        }
    }

    void Ray()
    {
        if (Input.GetMouseButton(0) && _currentAmmo>0 && !_reloading && _weapon.activeInHierarchy)
        {
            //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            _currentAmmo--;
            _uiManager.UpdateAmmo(_currentAmmo);

            if (!_shotAudioSource.isPlaying)
            {
                _shotAudioSource.Play();
            }

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                //Debug.Log("Raycast Hit Something: "+ hitInfo.transform.name);
                GameObject hitmark = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                
                Destroy(hitmark, 1);

                Destructable crate = hitInfo.transform.GetComponent<Destructable>();
                if(crate != null)
                {
                    crate.DestroyCrate();
                }

            }
            _flash.SetActive(true);
        }
        else
        {
            _flash.SetActive(false);
            _shotAudioSource.Stop();
        }
    }

    void CheckEsc()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    IEnumerator ClearInSecs(GameObject obj, float secs)
    {
        yield return new WaitForSeconds(secs);
        Destroy(obj);
    }

    IEnumerator DoReload()
    {
        _reloading = true;
        _currentLoads--;
        
        _currentAmmo = _maxAmmo;
        
        yield return new WaitForSeconds(1.5f);
        _uiManager.UpdateLoads(_currentLoads);
        _uiManager.UpdateAmmo(_currentAmmo);
        _reloading = false;
    }

    public void ActiveWeapon()
    {
        _weapon.SetActive(true);
    }
}
