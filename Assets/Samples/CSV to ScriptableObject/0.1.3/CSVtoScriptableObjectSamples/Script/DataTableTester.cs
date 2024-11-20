using UnityEngine;
using UnityEngine.UI;

public class DataTableTester : MonoBehaviour
{
    [SerializeField]
    private DataTableSimpleManager dataTableManager;
    private SampleData sampleData;

    [SerializeField]
    private Button visualizeDatasBtn;
    [SerializeField]
    private Text dataText;

    private void Start()
    {
        sampleData = dataTableManager.GetTable<SampleData>();
        visualizeDatasBtn.onClick.AddListener(VisualizeDatas);
    }

    private void VisualizeDatas()
    {
        dataText.text = string.Empty;
        foreach (BaseDataTableRow row in sampleData.GetTableRows())
        {
            dataText.text += (row as SampleDataRow).IndexInt + " / ";
            dataText.text += (row as SampleDataRow).NameString + " / ";
            dataText.text += (row as SampleDataRow).LengthFloat + " / ";
            dataText.text += (row as SampleDataRow).RandomVector2 + " / ";
            dataText.text += (row as SampleDataRow).RandomVector3 + " / ";
            dataText.text += "\n";
        }

    }
}
