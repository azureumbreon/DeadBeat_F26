/* Juliet Kingsley
Date: 11/10/2026
*/

using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public Scroller scroller;

    public GameObject jumpBoxPrefab;
    public GameObject slideBoxPrefab;
    public GameObject tutorialBox;

    private trackPosition left = trackPosition.LEFT;
    private trackPosition middle = trackPosition.MIDDLE;
    private trackPosition right = trackPosition.RIGHT;
    [Header("Intro Story Panels")]
    public GameObject enemyPanel;
    public GameObject enemyPanel2;

    public GameObject herFacePanel;
    public GameObject herFacePanel2;

    public GameObject textTMP;
    public GameObject jumpText;
    public GameObject moveLeft;
    public GameObject moveRight;
    public GameObject slideText;
    public GameObject dontHitText;
    public GameObject runText;

    public GameObject blackBackground;

    [SerializeField] private GameObject tutorialTextCanvas;

    void Start()
    {
        StartCoroutine(PlayIntroSequence());
        StartCoroutine(RunTutorialSequence());
    }

    IEnumerator PlayIntroSequence()
    {
       //scroller.AddObstacle(tutorialBox, 1f, left);

        blackBackground.SetActive(true);
        enemyPanel2.SetActive(false);
        enemyPanel.SetActive(true);
        herFacePanel.SetActive(false);
        textTMP.SetActive(false);

        yield return new WaitForSeconds(.75f);
        enemyPanel2.SetActive(true);
        blackBackground.SetActive(true);
        enemyPanel.SetActive(false);
        herFacePanel.SetActive(false);
        textTMP.SetActive(false);
        yield return new WaitForSeconds(.75f);



        enemyPanel2.SetActive(false);
        enemyPanel.SetActive(false);
        herFacePanel2.SetActive(true);
        textTMP.SetActive(true);

        yield return new WaitForSeconds(.75f);
        herFacePanel2.SetActive(false);
        herFacePanel.SetActive(true);
        textTMP.SetActive(true);

        yield return new WaitForSeconds(.75f);
        blackBackground.SetActive(false); 
        herFacePanel.SetActive(false);
        textTMP.SetActive(false);

    }

    IEnumerator RunTutorialSequence()
    {
        jumpText.SetActive(false);
        moveLeft.SetActive(false);
        moveRight.SetActive(false);
        slideText.SetActive(false);
        dontHitText.SetActive(false);
        runText.SetActive(false);
        yield return new WaitForSeconds(4f);
        jumpText.SetActive(true);
        moveLeft.SetActive(false);
        moveRight.SetActive(false);
        slideText.SetActive(false);
        dontHitText.SetActive(false);
        runText.SetActive(false);
        yield return new WaitForSeconds(3f);
        jumpText.SetActive(false);
        moveLeft.SetActive(true);
        yield return new WaitForSeconds(3f);
        moveLeft.SetActive(false);
        moveRight.SetActive(true);
        yield return new WaitForSeconds(3f);
        moveRight.SetActive(false);
        slideText.SetActive(true);
        yield return new WaitForSeconds(3f);
        slideText.SetActive(false);
        dontHitText.SetActive(true);
        yield return new WaitForSeconds(3f);
        dontHitText.SetActive(false);
        runText.SetActive(true);
        yield return new WaitForSeconds(10f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }
}
