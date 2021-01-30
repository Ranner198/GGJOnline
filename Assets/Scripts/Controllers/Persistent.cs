using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Persistent : MonoBehaviour {

    public static Persistent instance;
    public float loadingStepDelay = 0.15f;
    public enum loadSteps { DONE  };
    public loadSteps loadStep = loadSteps.DONE;
    private bool loaded = false;
    public string statusString;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
        // load everything immediately if I didn't start on splash.
        /*
        if (SceneManager.GetActiveScene().name != "splash")
        {
            loadingStepDelay = 0f;
            Debug.Log("starting load... not on splash screen");
            StartCoroutine(ProcessLoadStep(0f));
        } 
        */       
    }

    public void StartLoading()
    {
        Debug.Log("Persistent Start Load called");
        StartCoroutine(ProcessLoadStep(1f));
    }

    void LoadComplete()
    {
        Debug.Log("Load complete");
       // if (LoginManager.instance) LoginManager.instance.AssetsLoaded();
        loaded = true;
        loadingStepDelay = 0f;
    }

    IEnumerator ProcessLoadStep(float delay)
    {        
        yield return new WaitForSeconds(delay);
        Debug.Log("Processing load step = " + loadStep);
        if (loaded) LoadComplete();
        else
        {
            /*
            switch (loadStep)
            {
                case loadSteps.FACTORY:
                    statusString = "Loading factory...";
                    Factory.instance.Setup();
                    break;
                case loadSteps.HEROMANAGER:
                    statusString = "Loading heroes...";
                    HeroManager.instance.Setup();
                    break;
                case loadSteps.QUICKBATTLEMANAGER:
                    statusString = "Loading opponents...";
                    QuickbattleManager.instance.Setup();
                    break;
                    */
            //if (PersistLoadingPanel.instance) PersistLoadingPanel.instance.UpdateStep(statusString);
        }
    }

    public void LoadStepFinished()
    {
        Debug.Log("Finished step " + loadStep);
        loadStep = (loadSteps)((int)loadStep + 1);
        if (loadStep == loadSteps.DONE) LoadComplete();
        else StartCoroutine(ProcessLoadStep(loadingStepDelay));
    }

}
