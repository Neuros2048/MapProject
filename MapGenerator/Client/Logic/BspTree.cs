namespace Client.Logic;

public interface ICompareDouble<in T>
{
    public int Compare(T w, double x);
    public int Compare( double x,T w);
}
public class BspTree<T>(IComparer<T?> comparer,ICompareDouble<T?> compareDouble)
{
    private Vertex? _root ;
    public class Vertex
    {
        public Vertex? Left ;
        public Vertex? Right ;
        public Vertex Next;
        public Vertex Last;
        public Vertex? Parent;
        public T? Data;

        public Vertex(T? data)
        {
            Data = data;
            Next = this;
            Last = this;
        }
        public Vertex()
        {
            Next = this;
            Last = this;
        }
    }

    private readonly Vertex _end = new Vertex();
    
    private Vertex Add(T? data, Vertex? w)
    {
        if (comparer.Compare(data, w!.Data)>0)
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
                w.Right.Parent = w;
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
                w.Left.Parent = w;
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
        if (comparer.Compare(x.Data, w.Data)>0)
        {
            if (w.Right == null)
            {
                w.Right = x;
                w.Right.Parent = w;
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
                w.Left.Parent = w;
            }
            else
            {
                Add(x,w.Left);
            }
        }
    }

    private void Remove(T? data, Vertex w)
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

        Remove(data, comparer.Compare(data, w.Data) > 0 ? w.Right! : w.Left!);
    }
    
    private void Remove(Vertex data, Vertex w)
    {

        Console.WriteLine("remove in");
        if (data == w)
        {
            Console.WriteLine("com jest");
            if (w.Parent.Right!.Equals(w))
            {
                w.Parent.Right = null;
            }
            else
            {
                w.Parent.Left = null;
            }

            w.Next.Last = w.Last;
            w.Last.Next = w.Next;
            if (w.Left != null) Add(w.Left, w.Parent);
            if (w.Right != null) Add(w.Right, w.Parent);
            return;
        }
        Remove(data, comparer.Compare(data.Data, w.Data) > 0 ? w.Right! : w.Left!);
       

        
    }

    private Vertex Get(T? data, Vertex w)
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
    private Vertex FindFirst(T? data, Vertex w)
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

    private Vertex? LowerBound(T? data,Vertex? w)
    {
        if ( comparer.Compare(data,w!.Data) <= 0 )
        {
            
            if (w.Left == null) return w;
            Vertex? res = LowerBound(data, w.Left);
            if (res == null ) return w;
            return res;
        }
        Console.WriteLine("niew adsad");
        if (w.Right == null) return null;
        return LowerBound(data, w.Right);
    }
    private Vertex? LowerBound(double data,Vertex? w)
    {
        Console.WriteLine("hus");
        if ( compareDouble.Compare(data,w!.Data) <= 0 )
        {
            
            if (w.Left == null) return w;
            Vertex? res = LowerBound(data, w.Left);
            if (res == null ) return w;
            return res;
        }
        Console.WriteLine("niew adsad");
        if (w.Right == null) return null;
        return LowerBound(data, w.Right);
    }
    private bool Exist(T? data,Vertex w)
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
    

    public Vertex Add(T? data)
    {
        if (_root == null)
        {
            _root = new Vertex(data);
            _root.Next = _end;
            _root.Last = _end;
            _end.Next = _root;
            _end.Last = _root;
            _root.Parent = null;
            return _root;
        }
        return Add(data, _root);
    }

    public Vertex Get(T? data)
    {
        return Get(data, _root!);
    }

    public void Remove(Vertex w)
    {
        if (w.Equals(_root))
        {
            w.Next.Last = w.Last;
            w.Last.Next = w.Next;
            if (w.Right != null)
            {
                _root = w.Right;
                _root.Parent = null;
            }else if (w.Left != null)
            {
                _root = w.Last;
                _root.Parent = null;
            }
            else
            {
                _root = null;
            }
            return;
        }
        Console.WriteLine("remive rest");
        if (w.Equals(w.Parent!.Right) )
        {
            w.Parent.Right = null;
        }
        else
        {
            w.Parent.Left = null;
        }

        w.Next.Last = w.Last;
        w.Last.Next = w.Next;
        Console.WriteLine("cos tam");
        if (w.Left != null) Add(w.Left, w.Parent);
        if (w.Right != null) Add(w.Right, w.Parent);
       
    }

    public void Remove(T? data)
    {
        if (_root!.Data!.Equals(data))
        {
            _root.Next.Last = _root.Last;
            _root.Last.Next = _root.Next;
            Console.WriteLine("huje");
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
    
    public Vertex FindFirst(T? data)
    {
        return FindFirst(data, _root!);
    }

    public Vertex LowerBound(T? data)
    {
        return LowerBound(data, _root)!;
    }
    public Vertex LowerBound(double data)
    {
        return  LowerBound(data, _root)!;
    }

    public bool Exist(T? data)
    {
        return _root != null && Exist(data, _root);
    }

    public Vertex GetEnd()
    {
        return _end;
    }
    public Vertex GetFirst()
    {
        return _end.Next;
    }

    public Vertex GetRoot()
    {
        return _root!;
    }
    
}