using UnityEngine;

public class DataTableSimpleManager : MonoBehaviour
{
    [SerializeField]
    private BaseDataTable[] dataTables;

    public T GetTable<T>() where T : BaseDataTable
    {
        foreach (var elem in dataTables)
        {
            if (elem.GetType() == typeof(T))
            {
                return (T)elem;
            }
        }
        return null;
    }
}
