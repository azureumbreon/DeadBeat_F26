using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    public Slider MasterVolume;
    public AudioMixer MainAudioMixer;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene(). buildIndex + 1);
    }
    public void ChangeMasterVolume()
    {
        MainAudioMixer.SetFloat("MasterVolume", MasterVolume.value);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
