using UnityEngine;
using System.Collections;

public class Village : Environment {
    public bool isIndependant;
    public int prosperity;
    void Awake()
    {

    }

	// Use this for initialization
	void Start () {
        base.Start();
        prosperity = 0;
        type = "village";
        NameWizard nameWizard = GameObject.Find("NameWizard").GetComponent<NameWizard>();
        name = nameWizard.GenerateCityName();
        population = Random.Range(300, 600);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
