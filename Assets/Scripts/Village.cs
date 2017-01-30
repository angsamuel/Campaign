using UnityEngine;
using System.Collections;

public class Village : Environment {
    public bool isIndependant;
    public int prosperity;
	public int tax = 0;
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
	public int ProduceFood(){
		return (population*(3+prosperity));
	}
	public int ProduceMun(){
		return ((population * tax) / 100);
	}
}
