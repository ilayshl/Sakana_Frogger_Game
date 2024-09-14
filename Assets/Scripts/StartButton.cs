
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject fadeOut;
    [SerializeField] GameObject fadeOutMusic;
    bool isPlaying = false;

    void Start()
    {
        fadeOutMusic.GetComponent<Animation>().Stop();
    }

    private void OnMouseDown() {
        if(isPlaying==true){ return; }
        fadeOut.GetComponent<Animation>().Play();
        fadeOutMusic.GetComponent<Animation>().Play();
        Invoke("FishSounds", 1.5f);
        isPlaying=true;
    }

    void FishSounds() {
        gameObject.GetComponent<AudioSource>().Play();
        Invoke("NextLevel", 8);
    }

    void NextLevel() {
        SceneManager.LoadScene(SceneManager.sceneCount);
    }
}
