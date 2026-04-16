using UnityEngine;

public class Script : MonoBehaviour
{
    public AudioClip[] Clips;
    int seedsCounter = 0;
    int AlcCounter = 0;
    int aniseCounter = 0;
    public GameObject AlcCounterText;
    public GameObject SeedsCounterText;
    public GameObject AniseCounterText;
    public GameObject DestroyText;
    public GameObject CollectText;
    public GameObject IntroText;
    public GameObject MachineText;
    public GameObject MachineMissionText;
    public GameObject MissionCompletedText;
    bool messageShown = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DestroyText.SetActive(false);
        CollectText.SetActive(false);
        MachineText.SetActive(false);
        MachineMissionText.SetActive(false);
        MissionCompletedText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
        {
            GameObject obj = hit.collider.gameObject;

            if (obj.name == "Barrel" ||
                obj.name == "Barrel_2" ||
                obj.name == "Barrel_3" ||
                obj.name == "Machine_6")
            {
                DestroyText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(obj);
                    this.gameObject.GetComponent<AudioSource>().PlayOneShot(Clips[0]);
                }
            }
            else
            {
                DestroyText.SetActive(false);
            }

            if (obj.name == "anise" ||
                obj.name == "anise_2" ||
                obj.name == "anise_3" ||
                obj.name == "Alcohol" ||
                obj.name == "Alcohol_2" ||
                obj.name == "Seeds")
            {
                CollectText.SetActive(true);
            }
            else 
            {
                CollectText.SetActive(false);
            }
            if (obj.name == "Machine" && AlcCounter == 2 && seedsCounter == 1 && aniseCounter == 3)
            {
                MachineMissionText.SetActive(true);
            }
            else 
            {
                MachineMissionText.SetActive(false);
            }

            if (obj.name == "Machine")
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    MissionCompletedText.SetActive(true);
                    this.gameObject.GetComponent<AudioSource>().PlayOneShot(Clips[3]);
                }
            }
        }
        else
        {
            DestroyText.SetActive(false);
            CollectText.SetActive(false);
            MachineMissionText.SetActive(false);
            MissionCompletedText.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            IntroText.SetActive(false);
        }

        if (!messageShown && AlcCounter == 2 && seedsCounter == 1 && aniseCounter == 3)
        {
            MachineText.SetActive(true);
            messageShown = true;
            Invoke("DisableText", 2f);
        }
    }

    void DisableText() 
    {
        MachineText.SetActive(false);
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.name == "Plane")
        {

        }

        if (collision.gameObject.name == "Alcohol" || collision.gameObject.name == "Alcohol_2") 
        {
            Destroy(collision.gameObject);
            AlcCounter++;
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(Clips[2]);
            AlcCounterText.GetComponent<TMPro.TextMeshProUGUI>().text = "Αλκόολ: " + AlcCounter + " / 2";
        }

        if (collision.gameObject.name == "Seeds")
        {
            Destroy(collision.gameObject);
            seedsCounter++;
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(Clips[1]);
            SeedsCounterText.GetComponent<TMPro.TextMeshProUGUI>().text = "Σακούλα: " + seedsCounter + " / 1";
        }

        if (collision.gameObject.name == "anise" || collision.gameObject.name == "anise_2" || collision.gameObject.name == "anise_3")
        {
            Destroy(collision.gameObject);
            aniseCounter++;
            Debug.Log(aniseCounter);
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(Clips[1]);
            AniseCounterText.GetComponent<TMPro.TextMeshProUGUI>().text = "Άνισος: " + aniseCounter + " / 3"
;        }
    }
}
