using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject menu;
    public Slider MasterVolume;
    public AudioMixer MainAudioMixer;
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
            GameManager.instance.PauseGame();
        }
    }

    public void ResumeButton()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        GameManager.instance.ResumeGame();
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        GameManager.instance.EndGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void ChangeMasterVolume()
    {
        MainAudioMixer.SetFloat("MasterVolume", MasterVolume.value);
    }
}
