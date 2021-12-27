using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    public static GameController instance;
    public Vector2 lastCheckPoint;
    [SerializeField] private AudioController audioController;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        PlayMusicBackGround();
    }
    public void Respawn()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });
    }

    public void Exit()
    {
        Application.Quit();
    }

    void PlayMusicBackGround()
    {
        if(audioController != null)
            if(audioController.sonsBackground.Length > 0)
                if (!audioController.VericarSeAudioEstaTocando())
                    audioController.TocarAudio(audioController.sonsBackground[UnityEngine.Random.Range(0, audioController.sonsBackground.Length)], 0.1f);
                
    }
}
