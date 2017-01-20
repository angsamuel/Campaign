using UnityEngine;
using System.Collections;

public class Village : Environment {
    public bool isIndependant;
    public int prosperity;
    void Awake()
    {
        base.Awake();
        base.Start();
        prosperity = 0;
        type = "village";
        NameWizard nameWizard = GameObject.Find("NameWizard").GetComponent<NameWizard>();
        name = nameWizard.GenerateCityName();
        population = Random.Range(300, 600);
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
