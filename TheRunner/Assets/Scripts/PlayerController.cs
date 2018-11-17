using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float jumpSpeed = 50;
    public float initialJumpSpeed;
    public float forwardSpeed = 5;
    public float initialForwardSpeed;
    public bool isDead;
    public GameObject groundChecker;
    public LayerMask layerMask;
    public ParticleSystem jetpack;
    public AudioClip collectCoinSound;
    public AudioClip laserZapSound;
    public AudioSource mainAudioSrc;
    public AudioSource footStepAudio;
    public AudioSource jetpackAudio;
    public Text textCoin;

    private Animator playeranim;
    private Rigidbody2D rb;
    private bool isJumping;
    private bool groundCollision;
    private bool isGrounded;
    private UIManager ui;

    void Start()
    {
        this.ui = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<UIManager>();
        this.initialJumpSpeed = this.jumpSpeed;
        this.initialForwardSpeed = forwardSpeed;
        this.footStepAudio = this.GetComponent<AudioSource>();
        this.playeranim = this.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            this.isJumping = true;            
        }

        this.footStepAudio.enabled = this.isGrounded && !isDead && !UIManager.isPaused;

        this.playeranim.SetBool("IsGrounded", this.isGrounded);

        this.playeranim.SetBool("IsDead", isDead);

        AdjustJetpack();
    }

    private void AdjustJetpack()
    {
        this.jetpack.enableEmission = this.isGrounded ? false : true;
        this.jetpack.emissionRate = this.isJumping ? 300 : 150;
        this.jetpackAudio.enabled = this.isJumping && !isDead && !UIManager.isPaused;
    }

    void FixedUpdate()
    {
        this.CheckIsGrounded();

        if (!isDead)
        {
            var currentVelocity = this.rb.velocity;
            currentVelocity.x = forwardSpeed;
            this.rb.velocity = currentVelocity;

            if (this.isJumping)
            {
                this.isJumping = false;
                this.rb.AddForce(new Vector2(0, this.jumpSpeed));
            }
            forwardSpeed += 0.001f;
        }
        else
        {
            forwardSpeed = 0;
            this.jumpSpeed = 0;
        }

    }

    private void CheckIsGrounded()
    {
        var collision = Physics2D.OverlapCircle(this.groundChecker.transform.position, 0.1f, this.layerMask);
        this.isGrounded = collision == null ? false : true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Laser"))
        {
            if (!isDead)
            {
                this.mainAudioSrc.PlayOneShot(this.laserZapSound);
            }
            isDead = true;
            ValuesManager.SaveCoins();
            ValuesManager.SaveScore();
        }
        else if (collider.CompareTag("Coin"))
        {
            Destroy(collider.gameObject);
            this.mainAudioSrc.PlayOneShot(this.collectCoinSound);
            ValuesManager.AddCoin();
            this.ui.UpdateCoinsText();
        }
    }
}
