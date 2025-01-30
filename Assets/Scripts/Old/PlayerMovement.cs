
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Sprite[] playerSprites;
    [SerializeField] AudioSource playerSound;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip[] mySounds;
    [SerializeField] GameObject fadeIn;
    [SerializeField] GameObject moveTutorial;

    AudioClip activeSound;

    [SerializeField] float jumpDistance = 3f;
    bool isAlive=true;
    bool isFlying=false;
    bool isGrounded = true;
    bool canMove = true;
    [SerializeField] float sizeIncreament = 1.5f;
    int deathCounter;
    int winCounter;

    Vector3 startingPos;

    void Start()
    {
        canMove=false;
        fadeIn.GetComponent<Animation>().Play();
        startingPos=transform.position;
        Invoke("CanMove", 1f);
    }

    void CanMove() {
        canMove=true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) { Application.Quit();}
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
                if(!moveTutorial.IsDestroyed()){ Destroy(moveTutorial); }
            }}
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

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Fan")) {
            playerSound.PlayOneShot(mySounds[3]);
            if(isFlying==false) {
                isFlying=true;
                transform.localScale*=sizeIncreament;
                return;
            }
        }
        if(!collision.gameObject.CompareTag("Fan")) {
            if(isFlying==true) {
                transform.localScale/=sizeIncreament;
                isFlying=false;
            }
        }
            if(collision.gameObject.CompareTag("Death") || collision.gameObject.CompareTag("Plate")) {
            if(isAlive==true&&isFlying==false){
                isAlive=false;
                Invoke("ReloadLevel", 2);
                gameObject.GetComponent<SpriteRenderer>().sprite=playerSprites[1];
                gameObject.GetComponent<ParticleSystem>().Play();
                playerSound.PlayOneShot(mySounds[4]);
            deathCounter++;
            if(deathCounter==1){ Debug.Log("You've defied death for the first time. You can try again, but don't flop!"); }
            else{Debug.Log($"You've defied death {deathCounter} times. Stop flopping around!"); }
        } }
        if(collision.gameObject.CompareTag("Finish")) {
            if(isAlive==true&&isFlying==false) {
                isAlive=false;
                Invoke("ReloadLevel", 5);
                gameObject.GetComponent<SpriteRenderer>().sprite=playerSprites[0];
                gameObject.GetComponent<ParticleSystem>().Play();
                winCounter++;
                if(winCounter==1) { Debug.Log("You won, but that doesn't mean you escaped!"); }
                else{ Debug.Log($"You've tried to escape {winCounter} times... keep trying!"); }
            }
        }
    }

    void PlayerLand() {
        gameObject.GetComponent<SpriteRenderer>().sprite=playerSprites[0];
        isGrounded=true;
    }


    void ReloadLevel() {
        gameObject.GetComponent<TrailRenderer>().time=0;
        gameObject.GetComponent<TrailRenderer>().enabled=false;
        transform.position=startingPos;
        PlayerLand();
        gameObject.GetComponent<TrailRenderer>().enabled=true;
        gameObject.GetComponent<TrailRenderer>().time=1;
        transform.rotation=new Quaternion(0, 0, 0, 0);
        isAlive=true;
        isFlying=false;
    }
}
