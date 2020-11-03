using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTableController : MonoBehaviour
{
    public GameObject prefabRowTable;
    public List<GameObject> listRows;
    public GameObject header;
    public float spaceDY;
    bool flagVisible;

    // Start is called before the first frame update
    void Start()
    {
        flagVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddNewRow(TownController data){
        GameObject row = Instantiate(prefabRowTable, transform);
        if (!flagVisible) row.SetActive(false);
        listRows.Insert(0, row);
        row.GetComponent<RowTableController>().SetData(data);
        UpdatePositionRows();
    }
    void UpdatePositionRows(){
        for (int i=0; i<listRows.Count; i++){
            listRows[i].transform.localPosition = new Vector3(0, -spaceDY*i);
        }
    }
    public void SetVisible(bool flag){
        flagVisible = flag;
        header.SetActive(flagVisible);
        foreach (GameObject row in listRows) row.SetActive(flagVisible);
    }
    public void ClearRows(){
        foreach (GameObject row in listRows) Destroy(row);
        listRows = new List<GameObject>();
    }
}
