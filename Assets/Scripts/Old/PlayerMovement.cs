using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] MovementCollider movementCollider;
    private MovementCollider activeMovementCollider;

    [SerializeField] Sprite[] playerSprites;
    [SerializeField] AudioSource playerSound;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip[] mySounds;
    [SerializeField] GameObject fadeIn;

    AudioClip activeSound;

    [SerializeField] float jumpDistance = 2.2f;
    bool isAlive=true;
    bool isFlying=false;
    bool isGrounded = true;
    bool canMove = true;

    private const float GRID = 2.2f;

    void Start()
    {
        fadeIn.GetComponent<Animation>().Play();
    }

    void CanMove() {
        canMove=true;
    }

    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Escape)) { Application.Quit();}
            if(canMove==false){ return; }
        if (isAlive==true){
            if(Input.GetKeyDown(KeyCode.W)) {
            PlayerJump(KeyCode.W);
            isGrounded=false;
            }else if(Input.GetKeyDown(KeyCode.A)) {
            PlayerJump(KeyCode.A);
            isGrounded=false;
            }else if(Input.GetKeyDown(KeyCode.S)) {
            PlayerJump(KeyCode.S);
            isGrounded=false;
            }else if(Input.GetKeyDown(KeyCode.D)) {
            PlayerJump(KeyCode.D);
            isGrounded=false;
            }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) ||
           Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)) {
            PlayerLand();
            }}
            */
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || 
        Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)){
            Vector3 offset = new Vector3 (0, 90, 0);
            activeMovementCollider = Instantiate(movementCollider, transform.position + offset, Quaternion.identity);
        }

    //When pressing WASD, a movementCollider instantiates in the direction chosen.
    //When key is held, the movementCollider keeps going in the same direction.
    //When released, the movementCollider locks in the nearest Position Checker and the fish jumps to it.
    //When fish arrives, destroy MovementCollider.
    }

    void PlayerJump(KeyCode k) {
        if(isGrounded==false) { return; }
        gameObject.GetComponent<SpriteRenderer>().sprite=playerSprites[1];
        activeSound=mySounds[Random.Range(0, 2)];
        playerSound.PlayOneShot(activeSound);
        if(k==KeyCode.W) {
            transform.position+=new Vector3(0, jumpDistance, 0);
            transform.Rotate(Vector3.forward*90);
        } else if(k==KeyCode.S) {
            transform.position+=new Vector3(0, -jumpDistance, 0);
            transform.Rotate(Vector3.forward*-90);
        } else if(k==KeyCode.A) {
            transform.position+=new Vector3(-jumpDistance, 0, 0);
            transform.Rotate(Vector3.forward*90);
        } else if(k==KeyCode.D) {
            transform.position+=new Vector3(jumpDistance, 0, 0);
            transform.Rotate(Vector3.forward*-90);
            
        }
    }

    void PlayerLand() {
        gameObject.GetComponent<SpriteRenderer>().sprite=playerSprites[0];
        isGrounded=true;
    }


    void ReloadLevel() {
        gameObject.GetComponent<TrailRenderer>().time=0;
        gameObject.GetComponent<TrailRenderer>().enabled=false;
        PlayerLand();
        gameObject.GetComponent<TrailRenderer>().enabled=true;
        gameObject.GetComponent<TrailRenderer>().time=1;
        transform.rotation=new Quaternion(0, 0, 0, 0);
        isAlive=true;
        isFlying=false;
    }
}
