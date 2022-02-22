using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float h;
    float v;
    Vector3 moveDirection;
    public float speed = 3;
    [SerializeField]Transform aim;
    [SerializeField]Camera camara;
    Vector2 facingDirection;
    [SerializeField]Transform bulletPrefab;
    bool gunLoaded = true;
    [SerializeField]float fireRate = 1;
    public int health = 2;
    bool powerShotEnabled = false;
    [SerializeField] float invulnerableTime = 3;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;
    public bool invulnerable = false;

    public int Health {
        get => health;
        set {
            health = value;
            UIManager.Instance.UpdateUIHealth(health);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.UpdateUIHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
       h = Input.GetAxis("Horizontal");
       v = Input.GetAxis("Vertical");
        moveDirection.x = h;
        moveDirection.y = v;
        transform.position += moveDirection * Time.deltaTime *speed;
        //Movimiento de la mira
        facingDirection = camara.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized;
        //0 es el click izquierdo del mouse
        if (Input.GetMouseButton(0)&&gunLoaded) {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y,facingDirection.x)* Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Transform bulletClone = Instantiate(bulletPrefab , transform.position, targetRotation);
            if (powerShotEnabled) {
                bulletClone.GetComponent<Bullet>().powerShot = true;
            }
            StartCoroutine(ReloadGun());
        }
        anim.SetFloat("Speed", moveDirection.magnitude);
        if (aim.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else {
            spriteRenderer.flipX = false;
        }
    }
    public IEnumerator ReloadGun() {
        yield return new WaitForSeconds(1/fireRate);
        gunLoaded = true;
    }
    public IEnumerator MakeVulnerableAgain()
    {
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = true;
    }
    public void TakeDamage()
    {

        if(!invulnerable)
          Health--;
        if (Health <= 0)
        {
            //Destroy(gameObject);
            GameManager.Instance.gameOver = true;
            UIManager.Instance.ShowGameOverScreen();
            

        }
        //le pegan una vez le quitan un punto y se hace invulnerable por 3 segundos
        invulnerable = false;
       // StartCoroutine(MakeVulnerableAgain());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            switch (collision.GetComponent<PowerUp>().powerUpType) {
                case PowerUp.PowerUpType.FireRateIncrease:
                    fireRate++;
                    break;
                case PowerUp.PowerUpType.PowerShot:
                    powerShotEnabled = true;
                    break;

            }
            Destroy(collision.gameObject, 0.1f);

        }
    }
    
}
