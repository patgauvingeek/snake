internal class LinkedQueue<T>
{  
  private class LinkedNode
  {
    public T Item { get; set; }
    public LinkedNode Next { get; set; }
  }
  
  private LinkedNode mFirstNode;
  
  public int Count { get; private set; }
  
  public void Enqueue(T item)
  {
    var wNewLinkedNode = new LinkedNode { Item = item };
    Count++;
    if (mFirstNode == null)
    {
      mFirstNode = wNewLinkedNode;
      return;
    }
    var wCurrent = mFirstNode;
    while (wCurrent.Next != null)
    {
      wCurrent = wCurrent.Next;
    }
    wCurrent.Next = new LinkedNode { Item = item };
  }
  
  public T Dequeue()
  {
    var wItem = mFirstNode.Item;
    mFirstNode = mFirstNode.Next;
    Count--;
    return wItem;
  }
      
  public T Peek(int index = 0)
  {
    var wCurrent = mFirstNode;
    for (var i = 0; i < index && wCurrent != null; i++)
    {
      wCurrent = wCurrent.Next;  
    }
    return wCurrent != null ? wCurrent.Item : default(T);
  }
    
}
