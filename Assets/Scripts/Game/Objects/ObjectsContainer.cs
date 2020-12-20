using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjectsContainer<T> where T : FieldElementObject
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public void Remove(T item)
    {
        items.Remove(item);
    }

    public T Find(FieldElement source)
    {
        return items.FirstOrDefault(c => c.SourceElement == source);
    }

}
