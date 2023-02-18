using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidView : MonoBehaviour, IDamagable
{
    private int rotatingSpeed;
    private int movingSpeed;
    private float healthCount;
    private float damage;
    
    private Vector3 currentEulerAngles;
    public Rigidbody2D rb;
    public Transform bulletSpawner;
    private AsteroidController controller;

    public Slider slider;
    private AsteroidModel model;
    private AsteroidType asteroidType;
    private Vector3 initPos;
    private Quaternion initRot;
    private int splitNumber;
    private int listIndex;
    private int splitAsteroidForceMultiplier = 3;
    private int asteroidForceMultiplier = 5;
    int xRnd = 0;
    int yRnd = 0;

    public void InitializeController(AsteroidController asteroidController)
    {
        controller = asteroidController;
    }
    public AsteroidController GetController()
    {
        return controller;
    }

    private void Start()
    {
        EventService.Instance.GameStart += OnGameStart;

        initPos = controller.SpawnerPos;
        initRot = controller.SpawnerRotation;
       

        if (asteroidType == AsteroidType.Asteroid)
        {
            xRnd = Random.Range(-2, 2);
            yRnd = Random.Range(-2, 2);

            rb.AddForce(-new Vector2(xRnd, yRnd) * asteroidForceMultiplier, ForceMode2D.Impulse);

            StartCoroutine(EnableScreenWrapper());

        }
        else
        {
            xRnd = Random.Range(0, 0);
            yRnd = Random.Range(1, 2);
            rb.AddForce(new Vector2(xRnd, yRnd) * movingSpeed, ForceMode2D.Force);
            StartCoroutine(EnableScreenWrapperOfCutSceneAsteroid());
        }
    }

    IEnumerator EnableScreenWrapper()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<ScreenWrapper>().enabled = true;
    }
    IEnumerator EnableScreenWrapperOfCutSceneAsteroid()
    {
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<ScreenWrapper>().enabled = true;
    }
    private void OnGameStart()
    {

    }

    public void SetViewDetails()
    {
        model = controller.AsteroidModel;
        asteroidType = model.AsteroidType;
        splitNumber = 1;

        SetAsteroidSpeed(model.EnemySpeed, model.EnemyRotation);
        SetAsteroidHealth(model.EnemyHealth);
        SetAsteroidDamage(model.EnemyDamage);
    }

    public void SetSplitViewDetails(int _splitCounter, int _listIndex)
    {
        model = controller.AsteroidModel;
        splitNumber = _splitCounter;
        listIndex = _listIndex;
        SetAsteroidSpeed(model.EnemySpeed, model.EnemyRotation);
        SetAsteroidHealth(model.EnemyHealth);
        SetAsteroidDamage(model.EnemyDamage);

        xRnd = Random.Range(-2, 2);
        yRnd = Random.Range(-2, 2);

        Vector3 spawnDir = Random.insideUnitCircle.normalized * 2;
        rb.AddForce(-new Vector2(xRnd, yRnd) * splitAsteroidForceMultiplier, ForceMode2D.Impulse);

    }

    public int GetListIndex()
    {
        return listIndex;
    }

    public void SetSplitCount(int splitCount)
    {
        splitNumber = splitCount;
    }

    private void ResetPosition()
    {
        transform.position = initPos;
        transform.rotation = initRot;
    }

    public void SetAsteroidSpeed(int EnemySpeed, int EnemyRotation)
    {
        movingSpeed = EnemySpeed;
        rotatingSpeed = EnemyRotation;
    }

    public void SetAsteroidHealth(float EnemyHealth)
    {
        healthCount = EnemyHealth;
    }

    private void SetHealthBar()
    {
        slider.value = healthCount;
    }

    public void SetAsteroidDamage(float EnemyDamage)
    {
        damage = EnemyDamage;
    }

   
    public void TakeDamage(float damage)
    {

        controller.ApplyDamage(damage, splitNumber, transform.position, transform.rotation, this);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        ResetPosition();

    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Destroy()
    {
        Destroy(gameObject, 0.1f);
    }

    public void moveAsteroid()
    {
        currentEulerAngles += new Vector3(0, 0, 1) * Time.deltaTime * rotatingSpeed;
        transform.eulerAngles = currentEulerAngles;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
            }
            controller.ApplyDamage(damage, splitNumber, transform.position, transform.rotation, this);
        }
    }
}