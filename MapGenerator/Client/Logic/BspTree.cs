namespace Client.Logic;

public class BspTree<T>(IComparer<T> comparer)
{
    private Vertex? _root ;
    public class Vertex
    {
        public Vertex? Left ;
        public Vertex? Right ;
        public Vertex Next;
        public Vertex Last;
        public T Data;

        public Vertex(T data)
        {
            Data = data;
            Next = this;
            Last = this;
        }
    }

    private Vertex Add(T data, Vertex? w)
    {
        if (comparer.Compare(data, w!.Data)>=0)
        {
            if (w.Right == null)
            {
                w.Right = new Vertex(data)
                {
                    Last = w,
                    Next = w.Next
                };
                w.Next.Last = w.Right;
                w.Next = w.Right;
                return w.Right;
            }
            else
            {
                return Add(data,w.Right);
            }
        }
        else
        {
            if (w.Left == null)
            {
                w.Left = new Vertex(data)
                {
                    Next = w,
                    Last = w.Last
                };
                w.Last.Next = w.Left;
                w.Last = w.Left;
                return w.Left;
            }
            else
            {
                return Add(data,w.Left);
            }
        }
    }
    private void Add(Vertex x, Vertex w)
    {
        if (comparer.Compare(x.Data, w.Data)>=0)
        {
            if (w.Right == null)
            {
                w.Right = x;
            }
            else
            {
                Add(x,w.Right);
            }
        }
        else
        {
            if (w.Left == null)
            {
                w.Left = x;
            }
            else
            {
                Add(x,w.Left);
            }
        }
    }

    private void Remove(T data, Vertex w)
    {
        if (w.Left != null && w.Left.Data!.Equals(data))
        {
            w.Left.Next.Last = w.Left.Last;
            w.Left.Last.Next = w.Left.Next;
            Vertex? vertex = w.Left.Right;
            w.Left = w.Left.Left;
            if(vertex!= null) Add(vertex,w);
            return;
        }
        if (w.Right != null && w.Right.Data!.Equals(data))
        {
            w.Right.Next.Last = w.Right.Last;
            w.Right.Last.Next = w.Right.Next;
            Vertex? vertex = w.Right.Left;
            w.Right = w.Right.Right;
            if(vertex != null) Add(vertex,w);
            return;
        }

        Remove(data, comparer.Compare(data, w.Data) >= 0 ? w.Right! : w.Left!);
    }

    private Vertex Get(T data, Vertex w)
    {
        if (w.Data!.Equals(data))
        {
            return w;
        }

        if (comparer.Compare(data, w.Data) >= 0)
        {
            return Get(data, w.Right!);
        }

        return Get(data, w.Left!);
    }
    private Vertex FindFirst(T data, Vertex w)
    {
        Console.WriteLine($"find first ");
        int compResult = comparer.Compare(data, w.Data);
        if (compResult == 0)
        {
            Console.WriteLine($"find first rowny");
            return w;
        }
        Console.WriteLine($"find first nie  rowny");
        if (compResult > 0)
        {
            return FindFirst(data, w.Right!);
        }
        return FindFirst(data, w.Left!);
    }

    private bool Exist(T data,Vertex w)
    {
        if (w.Data!.Equals(data))
        {
            return true;
        }

        if (comparer.Compare(data, w.Data) >= 0)
        {
            return w.Right != null && Exist(data, w.Right);
        }

        return w.Left != null && Exist(data, w.Left);
    }
    

    public Vertex Add(T data)
    {
        if (_root == null)
        {
            _root = new Vertex(data);
            return _root;
        }
        return Add(data, _root);
    }

    public Vertex Get(T data)
    {
        return Get(data, _root!);
    }

    public void Remove(Vertex w)
    {
        Remove(w.Data);
    }

    public void Remove(T data)
    {
        if (_root!.Data!.Equals(data))
        {
            if (_root.Right != null)
            {
                Vertex? w = _root.Right.Left;
                _root.Right.Left = _root.Left;
                _root = _root.Right;
                if (w != null)
                {
                    Add(w, _root);
                }
            }
            else if (_root.Left != null)
            {
                Vertex? w = _root.Left.Right;
                _root.Left.Right = _root.Right;
                _root = _root.Left;
                if (w != null)
                {
                    Add(w, _root);
                }
            }
            else
            {
                _root = null;
            }
            return;
        }
        Remove(data,_root);
    }
    
    public Vertex FindFirst(T data)
    {
        return FindFirst(data, _root!);
    }

    public bool Exist(T data)
    {
        return _root != null && Exist(data, _root);
    }

    public Vertex GetRoot()
    {
        return _root!;
    }
    
}