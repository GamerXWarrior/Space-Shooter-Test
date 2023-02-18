using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipView : MonoBehaviour, IDamagable
{
    private float horizontalInput;
    private float verticalInput;
    private int rotatingSpeed;
    private int movingSpeed;
    private float healthCount;
    private float bulletDamage;
    public Renderer[] rend;
    private Vector3 currentEulerAngles;
    private Rigidbody2D rb;
    public Transform bulletSpawner;
    private ShipController controller;
    private ShipModel model;
    private bool isGameStarted = false;
    private bool isCutSceneOver = false;
    public GameObject barrier;

    private void Start()
    {
        EventService.Instance.OnPlayerSpawn();
        EventService.Instance.CameraSet += OnCutScenOver;
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitialiseController(ShipController shipController)
    {
        controller = shipController;
    }

    private void OnCutScenOver()
    {
        isCutSceneOver = true;
    }

    public void SetViewDetails()
    {
        model = controller.ShipModel;
        SetShipSpeed(model.MovingSpeed, model.RotatingSpeed);
        SetShipHealth(model.Health);
    }

    public void SetShipSpeed(int Speed, int Rotation)
    {
        movingSpeed = Speed;
        rotatingSpeed = Rotation;
    }

    public void SetShipHealth(float health)
    {
        healthCount = health;
        SetHealthBar();
    }

    private void SetHealthBar()
    {
        controller.SetShipHealth(healthCount);
    }

    public ShipController GetController()
    {
        return controller;
    }

    public void EnableBarrierPowerUp()
    {
        barrier.SetActive(true);
    }
    public void TakeDamage(float damage)
    {
        controller.ApplyDamage(damage);
    }

    public void Destroy()
    {
        Destroy(gameObject, 0.1f);

    }

    public ShipView GetView()
    {
        return this;
    }

    private void FixedUpdate()
    {
        if (!isCutSceneOver)
            return;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((horizontalInput > 0 && !isGameStarted) || (verticalInput > 0 && !isGameStarted) || (Input.GetButtonDown("Jump") && !isGameStarted))
        {
            isGameStarted = true;
            EventService.Instance.OnGameStart();
        }
   
        if (isGameStarted)
        {
            MoveShip();
            StopShip();
        }
    }

    private void Update()
    {
        if (!isCutSceneOver)
            return;
        if (Input.GetButtonDown("Jump"))
        {
            controller.fire(bulletSpawner, bulletDamage);

        }
    }

    private void MoveShip()
    {
        currentEulerAngles -= new Vector3(0, 0, horizontalInput) * Time.deltaTime * rotatingSpeed;
        transform.eulerAngles = currentEulerAngles;

        rb.AddForce(transform.up * verticalInput * movingSpeed, ForceMode2D.Force);
    }

    private void StopShip()
    {
        if (verticalInput == 0 && rb.velocity.magnitude != 0)
        {
            //rb.AddForce(transform. * verticalInput * movingSpeed, ForceMode2D.Force);
            rb.velocity -= new Vector2(rb.velocity.x * 0.05f, rb.velocity.y * 0.05f);

            if (rb.velocity.magnitude < 0.1 && rb.velocity.magnitude > 0)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.GetComponent<BulletView>() && !collision.transform.GetComponent<ShipView>())
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(0);
                barrier.SetActive(false);
            }
        }
    }
}