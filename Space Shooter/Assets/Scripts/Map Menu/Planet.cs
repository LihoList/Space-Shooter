using UnityEngine;
using UnityEngine.SceneManagement;

public class Planet : MonoBehaviour
{
    MapButtons mapButtonsScript;

    float changeSpeed = 2.0f;

    //origin and maximum planet scale value
    float baseScale = 1.0f;
    float maxScale = 1.3f;
    float scaleChangeMultiplyier = 0.03f;

    bool scaleUp = false;
    bool scaleDown = false;

    [Header("Which level will this planet load")]
    [SerializeField] int levelNumber;

    [Header("Planet click audiosource")]
    public AudioSource planetClickAudiosource;

    [Header("Select ring object")]
    public GameObject selectRing;

    [Header("Planet text animation")]
    public Animator planetTextAnimator1;

    [Header("Planet zoom animation")]
    public Animator zoomInAnnimator;
    bool canZoom;

    //dark screen animator controller
    Animator darkScreenAnimator;

    //Level Enter sound
    AudioSource levelStartAudioSource;


    private void Start()
    {
        levelStartAudioSource = GetComponent<AudioSource>();
        mapButtonsScript = FindObjectOfType<MapButtons>().GetComponent<MapButtons>();
        planetClickAudiosource = GameObject.Find("Planet choose sound").GetComponent<AudioSource>();

        selectRing.gameObject.SetActive(false);

        canZoom = true;
    }

    private void Update()
    {
        if(mapButtonsScript.topMenuOpen == false)
        {
            ScalePlus(scaleUp);
            ScaleMinus(scaleDown);
        };
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(levelNumber + 1);
    }

    private void OnMouseDown()
    {
        if (mapButtonsScript.topMenuOpen == false)
        {
            darkScreenAnimator = GameObject.Find("Canvas for dark screen").GetComponent<Animator>();
            darkScreenAnimator.Play("ShowDark");

            planetClickAudiosource.Play();

            switch(levelNumber)
            {
                case 1:
                    zoomInAnnimator.Play("ZoomPlanet1");
                    break;

                case 2:
                    zoomInAnnimator.Play("ZoomPlanet2");
                    break;

                case 3:
                    zoomInAnnimator.Play("ZoomPlanet3");
                    break;
            }
            canZoom = false;
            Invoke("LoadLevel", 2);
            levelStartAudioSource.Play();
        }
    }

    private void OnMouseOver()
    {
        if (mapButtonsScript.topMenuOpen == false && canZoom == true)
        {
            selectRing.gameObject.SetActive(true);
            planetTextAnimator1.Play("MapLevelTextShow");

            scaleUp = true;
            scaleDown = false;
        } 
    }

    private void OnMouseExit()
    {
        if (mapButtonsScript.topMenuOpen == false)
        {
            selectRing.gameObject.SetActive(false);
            planetTextAnimator1.Play("MapLevelTextHide");

            scaleUp = false;
            scaleDown = true;
        }
    }

    void ScalePlus(bool canScale)
    {
        if (!canScale)
            return;

        if (transform.localScale.x <= maxScale)
        {
            transform.localScale = new Vector3
           (
               Mathf.Lerp(transform.localScale.x, transform.localScale.x + scaleChangeMultiplyier, changeSpeed),
               Mathf.Lerp(transform.localScale.y, transform.localScale.y + scaleChangeMultiplyier, changeSpeed),
               Mathf.Lerp(transform.localScale.z, transform.localScale.z + scaleChangeMultiplyier, changeSpeed)
           );
        }
    }

    void ScaleMinus(bool canScale)
    {
        if (!canScale)
            return;

        if (transform.localScale.x >= baseScale)
        {
            transform.localScale = new Vector3
           (
               Mathf.Lerp(transform.localScale.x, transform.localScale.x - scaleChangeMultiplyier, changeSpeed),
               Mathf.Lerp(transform.localScale.y, transform.localScale.y - scaleChangeMultiplyier, changeSpeed),
               Mathf.Lerp(transform.localScale.z, transform.localScale.z - scaleChangeMultiplyier, changeSpeed)
           );
        }
    }







}
