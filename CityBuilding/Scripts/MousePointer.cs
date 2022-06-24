using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
public class MousePointer : MonoBehaviour
{
    public Camera cam;

    RaycastHit hit;
    Ray ray;
    [SerializeField] GameObject selectedGameObject;
    GameObject tmp;
    public GameObject[] imgs;
    [SerializeField] private Image imgSelecao;
    int img = 0;
    public GameObject currentSpawn;
    VerificaPosicao verifica;
    bool EstouSelecionado = false;
    Building building;
    int priceMoney, priceWood;
    private void Awake()
    {
        building = GetComponent<Building>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit)) 
        {

        }
        if (Input.GetMouseButtonDown(0)) 
        {
            if (selectedGameObject != null && verifica.colidiu == false && UiRecursos.abrir == true && building.Money > priceMoney && building.Wood > priceWood) 
            {
                tmp = Instantiate(selectedGameObject);
                building.Money -= priceMoney;
                building.Wood -= priceWood;
                tmp.GetComponent<Collider>().enabled = true;
                tmp.GetComponent<VerificandoIdentidade>().enabled = true;
                tmp.GetComponent<Rigidbody>().useGravity = true;
                Vector3 nearestPoint = GameObject.Find("Terrain").GetComponent<Terrain>().GridPoint(hit.point);
                tmp.transform.position = hit.point;
                tmp.transform.SetPositionAndRotation(new Vector3(nearestPoint.x, 1.2f, nearestPoint.z), tmp.transform.rotation);
                tmp.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            }

        }
        else if (Input.GetMouseButtonDown(1) || UiRecursos.abrir == false) 
        {
            Destroy(selectedGameObject);
            EstouSelecionado = false;
        }
        else 
        {
            if(selectedGameObject!= null) 
            {
                Vector3 nearestPoint = GameObject.Find("Terrain").GetComponent<Terrain>().GridPoint(hit.point);
                selectedGameObject.transform.SetPositionAndRotation(new Vector3(nearestPoint.x, 1.2f, nearestPoint.z), selectedGameObject.transform.rotation);
                verifica = selectedGameObject.GetComponent<VerificaPosicao>();
                //price = tmp.GetComponent<VerificandoIdentidade>().price;
                Debug.Log(priceMoney);
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (selectedGameObject != null) 
            {
                selectedGameObject.transform.Rotate(0, 90, 0);
            }
        }
        if(selectedGameObject == null) 
        {
            imgSelecao.enabled = false;
            verifica = null;
        }
        else 
        {
            imgSelecao.enabled = true;
        }
        if (verifica.colidiu == true)
        {
            selectedGameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }
        else if(verifica.colidiu == false)
        {
            selectedGameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        }
    }
    public void Objeto01() 
    {
        if (currentSpawn == null || currentSpawn.name != "SM_HouseC" && EstouSelecionado == false) 
        {
            EstouSelecionado = true;
            if (selectedGameObject = null) 
            {
                Destroy(selectedGameObject);
            }
            currentSpawn = GameObject.Find("SM_HouseC");
            selectedGameObject = Instantiate(currentSpawn);
            img = 0;
            imgSelecao.transform.localPosition = imgs[img].transform.localPosition;
            priceMoney = 500;
            priceWood = 50;
        }
    }

    public void Objeto02()
    {
        if (currentSpawn == null || currentSpawn.name != "SM_LightStand" && EstouSelecionado == false)
        {
            EstouSelecionado = true;
            if (selectedGameObject = null)
            {
                Destroy(selectedGameObject);
            }
            currentSpawn = GameObject.Find("SM_LightStand");
            selectedGameObject = Instantiate(currentSpawn);
            img = 1;
            imgSelecao.transform.localPosition = imgs[img].transform.localPosition;
            priceMoney = 200;
            priceWood = 10;
        }
    }

    public void Objeto03()
    {
        if (currentSpawn == null || currentSpawn.name != "SM_Mill" && EstouSelecionado == false)
        {
            EstouSelecionado = true;
            if (selectedGameObject = null)
            {
                Destroy(selectedGameObject);
            }
            currentSpawn = GameObject.Find("SM_Mill");
            selectedGameObject = Instantiate(currentSpawn);

            img = 2;
            imgSelecao.transform.localPosition = imgs[img].transform.localPosition;
            priceMoney = 1000;
            priceWood = 75;
        }
    }
    public void Objeto04()
    {

        if (currentSpawn == null || currentSpawn.name != "SM_Well" && EstouSelecionado == false)
        {
            EstouSelecionado = true;
            if (selectedGameObject = null)
            {
                Destroy(selectedGameObject);
            }
            currentSpawn = GameObject.Find("SM_Well");
            selectedGameObject = Instantiate(currentSpawn);

            img = 3;
            imgSelecao.transform.localPosition = imgs[img].transform.localPosition;
            priceMoney = 600;
            priceWood = 50;
        }
    }
    public void Objeto05()
    {
        if (currentSpawn == null || currentSpawn.name != "SM_HouseB" && EstouSelecionado == false)
        {
            EstouSelecionado = true;
            if (selectedGameObject = null)
            {
                Destroy(selectedGameObject);
            }
            currentSpawn = GameObject.Find("SM_HouseB");
            selectedGameObject = Instantiate(currentSpawn);

            img = 4;
            imgSelecao.transform.localPosition = imgs[img].transform.localPosition;
            priceMoney = 400;
            priceWood = 40;
        }
    }
}
