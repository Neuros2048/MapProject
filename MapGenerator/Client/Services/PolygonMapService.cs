using System.Runtime.InteropServices.ComTypes;
using Client.Logic;

namespace Client.Services;

public class PolygonMapService
{
    public PolygonMapService()
    {
        
    }
   

    private class _comparerPoint : IComparer<Point>
    {
       public int Compare(Point x, Point y)
       {
          var xComparison = x.X.CompareTo(y.X);
          if (xComparison != 0) return xComparison;
          return x.Y.CompareTo(y.Y);
       }
    }
    
    private struct Position(Point p,int id, Point left,int idLeft ,Point right,int idRight)
    {
       public static double L = 0;
       public static double Lmin = 0;
       public static double Lmax = 800;
       public Point P = p;
       public Point Left = left;
       public Point Right = right;
       public int Id = id;
       public int IdLeft = idLeft;
       public int IdRight = idRight;
    }
    private static readonly double TOLERANCE = 0.0000000001;
    private class ComparerPosition : IComparer<Position>
    {
       public int Compare(Position x, Position y)
       {
          double a = Przeciecie(x.Left, x.P);
          double b = Przeciecie(x.P, x.Right);
          double c = Przeciecie(y.Left, y.P);
          double d = Przeciecie(y.P, y.Right);
          Console.WriteLine($"({x.P.X} {x.P.Y}) {x.Id} ({x.Left.X} {x.Left.Y}) {x.IdLeft} ({x.Right.X} {x.Right.Y}) {x.IdRight}");
          Console.WriteLine($"({y.P.X} {y.P.Y}) {y.Id} ({y.Left.X} {y.Left.Y}) {y.IdLeft} ({y.Right.X} {y.Right.Y}) {y.IdRight} ");
          Console.WriteLine($"prownanie {a} {b} {c} {d} dla l {Position.L}");

          if ( (x.Id == x.IdRight && x.Id == x.IdLeft) || (y.Id == y.IdRight && y.Id == y.IdLeft))
          {
             if ((a <= c && d <= b) || (c <= a && b <= d)) return 0;
          }
          
          /*if (x.Id == y.IdRight && x.Id ==  y.IdRight )
          {
             if (x.IdRight == y.Id) return -1;
             return 1;
         
          }

          if (y.Id == x.IdRight && y.Id ==  x.IdRight)
          {
             if (y.IdRight == x.Id) return 1;
             return -1;
          }*/
          
         
         
          if ((a+b)/2 < (c+d)/2) return -1;
          return 1;

       }
       
       private double Przeciecie( Point p1 , Point p2)
       {
          
          if (p1.Y <= Position.Lmin) return Position.Lmin;
          if (p2.Y >= Position.Lmax) return Position.Lmax;
          if (Math.Abs(p2.X - Position.L) < TOLERANCE) return p2.Y;
          if (Math.Abs(p1.X - Position.L) < TOLERANCE) return p1.Y;
          double l = Position.L;
          double a = p2.Y;
          double b = p2.X;
          double c = p1.Y;
          double d = p1.X;
          double u = 2 * (b - l);
          double v = 2 * (d - l);
          return  Math.Min( Math.Max(   -(Math.Sqrt(v * (Math.Pow(a, 2) * u - 2 * a * c * u + Math.Pow(b, 2) * (u - v) + Math.Pow(c, 2) * u) +
                                 Math.Pow(d, 2) * u * (v - u) + Math.Pow(l, 2) * Math.Pow((u - v), 2)) + a * v - c * u) /
                     (u - v) ,   Position.Lmin) , Position.Lmax);
          
       }
    }
    private double Przeciecie( Point p1 , Point p2)
    {
          
       if (p1.Y <= Position.Lmin) return Position.Lmin;
       if (p2.Y >= Position.Lmax) return Position.Lmax;
       if (Math.Abs(p2.X - Position.L) < TOLERANCE) return p2.Y;
       if (Math.Abs(p1.X - Position.L) < TOLERANCE) return p1.Y;
       double l = Position.L;
       double a = p2.Y;
       double b = p2.X;
       double c = p1.Y;
       double d = p1.X;
       double u = 2 * (b - l);
       double v = 2 * (d - l);
       return -(Math.Sqrt(v * (Math.Pow(a, 2) * u - 2 * a * c * u + Math.Pow(b, 2) * (u - v) + Math.Pow(c, 2) * u) +
                          Math.Pow(d, 2) * u * (v - u) + Math.Pow(l, 2) * Math.Pow((u - v), 2)) + a * v - c * u) /
              (u - v);
          
    }
    public struct Point
    {
       public double X, Y;
       public Point(double x, double y)
       {
          this.X = x;
          this.Y = y;
       }
       public Point()
       {
          this.X = 0;
          this.Y = 0;
       }
       public static Point operator +(Point a, Point b)
          => new Point(a.X+ b.X , a.Y + b.Y);

       public static Point operator -(Point a, Point b)
          => new Point(a.X- b.X , a.Y - b.Y);

       public static Point operator *(Point a, double b)
          => new Point(a.X* b , a.Y * b);
       public Point rot() {
          return new Point(-Y,X);
       }
       public double len() {
          return Double.Hypot(X,Y);
       }
    }
    public struct Circle
    {
       public Point p;
       public double r;

       public Circle(Point p, double r)
       {
          this.p = p;
          this.r = r;
       }
    }

    public class PointShape
    {
       public Point P;
       public PointShape Right;
       public PointShape Left;
       public int id;
       public int id2;

       public PointShape(Point p,int id,int id2)
       {
          P = p;
          Right = this;
          Left = this;
          this.id = id;
          this.id2 = id2;
       }
    }


    private double Distance(Point p1, Point p2)
    {
       return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
    }
    private Circle FindCircleEnd(Point p1, Point p2, bool left)
    {
       Console.WriteLine($"pun cri end  {p1.X} {p1.Y} and  {p2.X} {p2.Y}");
       double Ymax = 800;
       double ymin = 0;
       double Xmax = Ymax;
       double x = (p1.X + p2.X) / 2;
       double y = (p1.Y + p2.Y) / 2;
       double vy = p2.X - p1.X;
       double vx = p2.Y - p1.Y;
       vx *= -1;
       
       
       if (vx == 0)
       {
          if (left)
          {
             return new Circle(new Point(x, 0), y-ymin);
          }

          return new Circle(new Point(x, Ymax), Ymax - y);
       }

       if (vy == 0)
       {
          if (left)
          {
             return new Circle(new Point(0, y), x);
          }
          else
          {
             return new Circle(new Point(Xmax, y), Xmax - x);
          }
       }

       if (vx > 0)
       {
          vx *= -1;
          vy *= -1;
       }

       Circle c1;
       Circle c2;
       double ileX = (0 - x) / vx;
       double newY = y + vy * ileX;
       if (newY < ymin || newY > Ymax)
       {
          if(vy < 0)
          {
             double ileY = (0 - y) / vy;
             double newX = x + vx * ileY;
             var res = new Point(newX,ymin );
             c1 = new Circle(res, Distance(res, p1));
          }
          else
          {
             double ileY = (Ymax - y) / vy;
             double newX = x + vx * ileY;
             var res = new Point(newX,Ymax );
             c1 = new Circle(res, Distance(res, p1));
          }
       }
       else
       {
          var res = new Point(0, newY);
          c1 = new Circle(res, Distance(p1 , res )  );
       }
       vx *= -1;
       vy *= -1;
       
       ileX = (Xmax - x) / vx;
       newY = y + vy * ileX;
       if (newY < ymin || newY > Ymax)
       {
          if(vy < 0)
          {
             double ileY = (0 - y) / vy;
             double newX = x + vx * ileY;
             var res = new Point(newX,ymin );
             c2 = new Circle(res, Distance(res, p1));
          }
          else
          {
             double ileY = (Ymax - y) / vy;
             double newX = x + vx * ileY;
             var res = new Point(newX,Ymax );
             c2 = new Circle(res, Distance(res, p1));
          }
       }
       else
       {
          var res = new Point(Xmax, newY);
          c2 = new Circle(res, Distance(p1 , res )  );
       }
       

       if (left ^ (c1.p.Y < c2.p.Y) )
       {
          return c2;
       }
       else
       {
          return c1;
       }
       
    }

    private Circle FindCircle(Position position)
    {
       return FindCircle(position.P, position.Left, position.Right);
    }
    private Circle FindCircle(Point p1,Point p2,Point p3) {
      double x12 =p1.X - p2.X;
      double x13 =p1.X- p3.X;
 
      double y12 =p1.Y- p2.Y;
      double y13 =p1.Y- p3.Y;
 
      double y31 = p3.Y -p1.Y;
      double y21 = p2.Y -p1.Y;
 
      double x31 = p3.X -p1.X;
      double x21 = p2.X -p1.X;
      
      double sx13 = (Math.Pow(p1.X, 2) - Math.Pow(p3.X, 2));
      
      double sy13 = (Math.Pow(p1.Y, 2) - Math.Pow(p3.Y, 2));
 
      double sx21 = (Math.Pow(p2.X, 2) - Math.Pow(p1.X, 2));
                     
      double sy21 = (Math.Pow(p2.Y, 2) - Math.Pow(p1.Y, 2));
 
      double f = ((sx13) * (x12) + (sy13) * (x12) + (sx21) * (x13) + (sy21) * (x13)) / (2 * ((y31) * (x12) - (y21) * (x13)));
      double g = ((sx13) * (y12) + (sy13) * (y12) + (sx21) * (y13) + (sy21) * (y13)) / (2 * ((x31) * (y12) - (x21) * (y13)));
      double c = -Math.Pow(p1.X, 2) - Math.Pow(p1.Y, 2) - 2 * g * p1.X - 2 * f * p1.Y;
      double h = -g;
      double k = -f;
      double sqr_of_r = h * h + k * k - c;
      
      double r = Math.Round(Math.Sqrt(sqr_of_r), 5);
      return new Circle( new Point(-g, -f),r);
   }

   private void PrintPosition(Position p2)
   {
      Console.WriteLine($"({p2.P.X} {p2.P.Y}) {p2.Id} ({p2.Left.X} {p2.Left.Y}) {p2.IdLeft} ({p2.Right.X} {p2.Right.Y}) {p2.IdRight}");
   }
   // Function to calculate the circumcenter of the triangle formed by three points
 
    private PriorityQueue<Zdarzenie,Point > points;
      
    private class Zdarzenie
    {
       public int Id;
       public bool Typ = false;
       public Point P;
       public int IdLeft = -1;
       public int IdRight = -1;

       public Zdarzenie(int id, Point p)
       {
          Id = id;
          P = p;
          Typ = true;
       }
       
       public Zdarzenie(int id, int idLeft, int idRight,Point p)
       {
          Id = id;
          IdLeft = idLeft;
          IdRight = idRight;
          P = p;
       }

       public Zdarzenie(Position position, Point p)
       {
          Id = position.Id;
          IdLeft = position.IdLeft;
          IdRight = position.IdRight;
          P = p;
       }
    }
    public class Center
    {
       public Point P;
      
       public PointShape? Left = null;
       public PointShape? Right = null;
       public bool started = false;
       public Center(Point p)
       {
          P = p;
       }

       public void Add(Point p,int i,int j)
       {
          if (Left == null)
          {
             Left = new PointShape(p, i,j);
             Right = Left;
             return;
          }
          PointShape a = new PointShape(p, i,j);
          if (Left.id == i || Left.id2 == i || Left.id == j || Left.id2 == j )
          {
             a.Right = Left;
             a.Left = Left.Left;
             a.Left.Right = a;
             Left.Left = a;
             Left = a;
          }
          else
          {
             a.Left = Right;
             a.Right = Right.Right;
             a.Right.Left = a;
             Right.Right = a;
             Right = a;
          }
          
          
       }


    }
    private class line
    {
       public Point p;
       public line? top;
       public line? down;
    }
    double X0 = 0, X1 = 800, Y0 = 0, Y1 = 800;

    private struct luk
    {
       
    }
    private void hahaha(BspTree<Position> tree)
    {
       Console.WriteLine("root");
       var root = tree.GetRoot();
       var croot = root.Next;
     
       PrintPosition(root.Data);
          
       double tc = Przeciecie(root.Data.Left, root.Data.P);
       double td = Przeciecie(root.Data.P, root.Data.Right);
       Console.WriteLine($"prownanie  {tc} {td} dla l {Position.L}");
          
       while (!croot.Equals(root))
       {
          PrintPosition(croot.Data);
          tc = Przeciecie(croot.Data.Left, croot.Data.P); 
          td = Przeciecie(croot.Data.P, croot.Data.Right);
          Console.WriteLine($"prownanie  {tc} {td} dla l {Position.L}");
          croot = croot.Next;
             
       }
       Console.WriteLine("hah ended");
    }

    public List<Center> Solve(List<Point> Spoints)
    {/*
       points = new PriorityQueue<Zdarzenie, Point>(new _comparerPoint());
       List<Center> ans = new List<Center>();
       int j = 0;
       Spoints.ForEach( p => points.Enqueue(new Zdarzenie(j++,p),p));
       Spoints.ForEach(p => ans.Add(new Center(p)));
       BspTree<Position> tree = new BspTree<Position>(new ComparerPosition() );
       Zdarzenie cur = points.Dequeue();
       Position.L = cur.P.X;
       tree.Add(new Position(cur.P, cur.Id ,new Point(-1, -1), -1 ,new Point(-1, 801), -1 ));
       Console.WriteLine( FindCircleEnd(new Point(469, 612), new Point(72 ,279), true).r);
       Console.WriteLine( FindCircleEnd(new Point(469, 612), new Point(72 ,279), false).r);
       Console.WriteLine( FindCircleEnd(new Point(72 ,279),new Point(469, 612),  true).r);
       Console.WriteLine( FindCircleEnd(new Point(72 ,279),new Point(469, 612),  false).r);
       Point priority;
       Console.WriteLine("dsadasda "+ call());
       while (points.TryDequeue(out cur, out priority))
       {
          //if(cur.P.X < Position.L) continue;
          Position.L =  Math.Max(priority.X,Position.L );
          Console.WriteLine($"{cur.Typ} {cur.P.X } {cur.P.Y} {cur.Id}  {cur.IdLeft } {cur.IdRight} " );
          Console.WriteLine( $" root  {priority.X} {priority.Y}" );
          hahaha(tree);
         
          if (cur.Typ)
          {
             Console.WriteLine($"a1");
             var gdzie = tree.FindFirst(new Position(cur.P,cur.Id , new Point(cur.P.X -10,cur.P.Y) , cur.Id, new Point(cur.P.X-10,cur.P.Y), cur.Id));
             Console.WriteLine($"id zanalezionego {gdzie.Data.Id} ");
             PrintPosition(gdzie.Data);
             tree.Remove(gdzie);
             Position p1 = new Position(cur.P, cur.Id, gdzie.Data.P, gdzie.Data.Id, gdzie.Data.P, gdzie.Data.Id);
             Position p2 = new Position(gdzie.Data.P, gdzie.Data.Id, gdzie.Data.Left, gdzie.Data.IdLeft, cur.P, cur.Id);
             Position p3 = new Position(gdzie.Data.P, gdzie.Data.Id,cur.P, cur.Id, gdzie.Data.Right, gdzie.Data.IdRight);
             //Console.WriteLine($"({p2.P.X} {p2.P.Y}) {p2.Id} ({p2.Left.X} {p2.Left.Y}) {p2.IdLeft} ({p2.Right.X} {p2.Right.Y}) {p2.IdRight}");
             //Console.WriteLine($"{p3.Id} {p3.IdLeft} {p3.IdRight}");
             Circle r;
             Point rp;

             /*if (Distance(p3.Left, p3.P) > p3.P.X)
             {
                p1.Right = gdzie.Data.Right;
                p1.IdRight = gdzie.Data.IdRight;
                tree.Add(p1);
                tree.Add(p2);
           
             }else if (Distance(p2.P, p2.Right) > p2.P.X)
             {
                p1.Left = gdzie.Data.Left;
                p1.IdLeft = gdzie.Data.IdLeft;
                tree.Add(p1);
       
                tree.Add(p3);
             }
             else
             {
                tree.Add(p1);
                tree.Add(p2);
                tree.Add(p3);
             }#1#

             Console.WriteLine($"wpisywanie do drzewa");
             if (tree.GetRoot() != null)
             {
                hahaha(tree);
                Console.WriteLine("usuniecie wypisanie");
             }
             
             tree.Add(p1);
             hahaha(tree);
             
             Console.WriteLine($" przeciecie {Przeciecie(p2.P, p2.Right)}");
             tree.Add(p2);
             hahaha(tree);
             
             
             tree.Add(p3);
             hahaha(tree);
             
             
             /*var tesr  = tree.GetRoot();
             var ctests = tesr.Next;
             Console.Write($"rooot - ");
             PrintPosition(tesr.Data);
             while (ctests != tesr)
             {
                PrintPosition(ctests.Data);
                ctests = ctests.Next;
             }
             var compp = new ComparerPosition();
             Console.WriteLine( compp.Compare(tesr.Data, tesr.Right.Data));
             Console.WriteLine($"{tesr.Left == null}");
             Console.WriteLine(tree.Exist(p2));#1#
             Console.WriteLine($"specjalne kułka wej");
           
             if (p2.IdLeft == -1)
             {
                Console.WriteLine($"specjalne kułka");
                r = FindCircleEnd(p2.Right ,p2.P,true);
                rp = new Point(r.p.X + r.r, r.p.Y);
                points.Enqueue(new Zdarzenie( p2.Id, p2.IdLeft,p2.IdRight , r.p ),rp);
                Console.WriteLine($"puntk r {r.p.X} {r.p.Y}  r {rp.X}");
             }
             else
             {
                Console.WriteLine($"normalne kułka");
                r = FindCircle(p2);
                if (r.p.X +r.r > Position.L&&r.p.Y < p2.P.Y )
                {
                   rp = new Point(r.p.X + r.r, r.p.Y);
                   points.Enqueue(new Zdarzenie( p2.Id, p2.IdLeft,p2.IdRight , r.p ),rp);
                   Console.WriteLine($"puntk r {r.p.X} {r.p.Y}  r {rp.X}");
                }
                
             }
             
             
             if (p3.IdRight == -1)
             {
                Console.WriteLine($"specjalne kułka");
                r = FindCircleEnd(p3.Left ,p3.P,false);
                rp = new Point(r.p.X + r.r, r.p.Y);
                points.Enqueue(new Zdarzenie( p3.Id, p3.IdLeft,p3.IdRight , r.p ),rp);
                Console.WriteLine($"puntk r {r.p.X} {r.p.Y}  r {rp.X}");
             }
             else
             {
                Console.WriteLine($"normalne");
                r = FindCircle(p3);
                if (r.p.X +r.r > Position.L && r.p.Y > p3.P.Y)
                {
                   rp = new Point(r.p.X + r.r, r.p.Y);
                   points.Enqueue(new Zdarzenie( p3.Id, p3.IdLeft,p3.IdRight , r.p ),rp);
                }
             }
             
            
             
          }
          else
          {
             Console.WriteLine($"a2");
             Position szukane;
             if (cur.IdLeft != -1 && cur.IdRight!= -1)
             {
                Console.WriteLine($"flsa srodek");
                szukane = new Position(ans[cur.Id].P, cur.Id,ans[cur.IdLeft].P, cur.IdLeft ,ans[cur.IdRight].P, cur.IdRight );
                if (tree.Exist(szukane))
                {
                   ans[cur.Id].points.Add(cur.P);
                   ans[cur.IdLeft].points.Add(cur.P);
                   ans[cur.IdRight].points.Add(cur.P);
                   Console.WriteLine($"dodana punkt punkt {cur.P.X} {cur.P.Y}");
                   var operatin = tree.Get(szukane);
                   tree.Remove(szukane);
                   operatin.Last.Data.Right = operatin.Next.Data.P;
                   operatin.Last.Data.IdRight = operatin.Next.Data.Id;
                   operatin.Next.Data.Left = operatin.Last.Data.P ;
                   operatin.Next.Data.IdLeft = operatin.Last.Data.Id ;
                   Circle c;
                   Point cp;
                   if (operatin.Next.Data.IdRight != -1  )
                   {
                      Console.WriteLine("hdasdasdasd1");
                      c = FindCircle(operatin.Next.Data);
                      Console.WriteLine($"{c.p.X+c.r}  {Position.L}");
                      if (c.p.X+c.r > Position.L)/*(operatin.Next.Data.P.X < operatin.Next.Data.Left.X ||
                          operatin.Next.Data.P.X < operatin.Next.Data.Right.X)#1#
                      {
                         
                         cp = new Point(c.p.X + c.r, c.p.Y);
                         points.Enqueue(new Zdarzenie(operatin.Next.Data, c.p), cp);
                         Console.WriteLine($"dadanie kolo event {cp.X} {cp.Y} {c.p.X} {c.p.Y} {c.r}");
                      }
                   }

                   if (operatin.Last.Data.IdLeft != -1)
                   {
                      Console.WriteLine("hdasdasdasd2");
                      c = FindCircle(operatin.Last.Data);
                      if (c.p.X+c.r > Position.L)/*(operatin.Last.Data.P.X < operatin.Last.Data.Left.X ||
                          operatin.Last.Data.P.X < operatin.Last.Data.Right.X)#1#
                      {
                         
                         cp = new Point(c.p.X + c.r, c.p.Y);
                         points.Enqueue(new Zdarzenie(operatin.Last.Data, c.p), cp);
                      }
                   }
                   Console.WriteLine("hdasdasdasd");
                   if (operatin.Next.Data.IdRight == -1 || operatin.Last.Data.IdLeft == -1)
                   {
                      Circle r;
                      Point rp;

                      if (operatin.Next.Data.IdRight == -1)
                      {
                         r = FindCircleEnd(operatin.Next.Data.P ,operatin.Last.Data.P,false);
                         if (r.p.X <  Math.Max( operatin.Next.Data.P.X,operatin.Last.Data.P.X ) )  r = FindCircleEnd(operatin.Next.Data.P ,operatin.Last.Data.P,true);
                         rp = new Point(r.p.X + r.r, r.p.Y);
                         Console.WriteLine($" hejes {r.p.X} {r.p.Y}");
                         points.Enqueue(new Zdarzenie( operatin.Next.Data.Id, operatin.Next.Data.IdLeft ,operatin.Next.Data.IdRight , r.p ),rp);
                      }
                      else
                      {
                         r = FindCircleEnd(operatin.Next.Data.P ,operatin.Last.Data.P,true);
                         if (r.p.X < Math.Max( operatin.Next.Data.P.X,operatin.Last.Data.P.X ))  r = FindCircleEnd(operatin.Next.Data.P ,operatin.Last.Data.P,false);
                         rp = new Point(r.p.X + r.r, r.p.Y);
                         Console.WriteLine($" hejes {r.p.X} {r.p.Y}");
                         points.Enqueue(new Zdarzenie( operatin.Last.Data.Id, operatin.Last.Data.IdLeft ,operatin.Last.Data.IdRight , r.p ),rp);
                      }
             
                      
                   }
                }
                
             }else if (cur.IdRight == -1)
             {
                Console.WriteLine($"false granica prawa");
                szukane = new Position(ans[cur.Id].P, cur.Id,ans[cur.IdLeft].P, cur.IdLeft ,new Point(-1,801), -1);
                Console.WriteLine($"szukamy   ({szukane.P.X} {szukane.P.Y}) {szukane.Id} ({szukane.Left.X} {szukane.Left.Y}) {szukane.IdLeft} ({szukane.Right.X} {szukane.Right.Y}) {szukane.IdRight}");
                if (tree.Exist(szukane))
                {
                   Console.WriteLine($"false granica prawa robione");
                   var operatin = tree.Get(szukane);
                   Console.WriteLine($"szukanie");
                   tree.Remove(szukane);
                   Console.WriteLine($"usuniecie");
                   ans[cur.Id].points.Add(cur.P);
                   ans[cur.IdLeft].points.Add(cur.P);
                   Console.WriteLine($"dodana punkt punkt {cur.P.X} {cur.P.Y}");
                   operatin.Last.Data.Right = operatin.Data.Right;
                   operatin.Last.Data.IdRight = operatin.Data.IdRight;
                   Console.WriteLine($"das  {operatin.Last.Data.Id}, {operatin.Last.Data.IdLeft}, {operatin.Last.Data.IdRight}");
                   if (operatin.Last.Data.IdLeft != -1 )
                   {
                      Console.WriteLine("wedzo operacja dodanie koncaka prawda");
                      Circle r;
                      Point rp;
                      r = FindCircleEnd(operatin.Last.Data.P, operatin.Last.Data.Left, false);
                      if (r.p.X < Math.Max(operatin.Last.Data.P.X, operatin.Last.Data.Left.X))
                         r = FindCircleEnd(operatin.Last.Data.P, operatin.Last.Data.Left, true);
                      rp = new Point(r.p.X + r.r, r.p.Y);
                      Console.WriteLine($" hejes {r.p.X} {r.p.Y}");
                      points.Enqueue(
                         new Zdarzenie(operatin.Last.Data.Id, operatin.Last.Data.IdLeft, operatin.Last.Data.IdRight,
                            r.p), rp);
                   }

                }
             }
             else
             {
                Console.WriteLine($"false granica leva");
                szukane = new Position(ans[cur.Id].P, cur.Id,new Point(-1,-1),-1 ,ans[cur.IdRight].P, cur.IdRight );
                Console.WriteLine($"szukamy ({szukane.P.X} {szukane.P.Y}) {szukane.Id} ({szukane.Left.X} {szukane.Left.Y}) {szukane.IdLeft} ({szukane.Right.X} {szukane.Right.Y}) {szukane.IdRight}");
                if (tree.Exist(szukane))
                {
                   Console.WriteLine($"false granica leva robione");
                   var operatin = tree.Get(szukane);
                   Console.WriteLine($"szukanie");
                   tree.Remove(szukane);
                   Console.WriteLine($"usuniete");
                   ans[cur.Id].points.Add(cur.P);
                   ans[cur.IdRight].points.Add(cur.P);
                   operatin.Next.Data.Left = operatin.Data.Left;
                   operatin.Next.Data.IdLeft = operatin.Data.IdLeft;
                   
                   
                   if (operatin.Next.Data.IdRight != -1)
                   {
                      Circle r;
                      Point rp;
                      r = FindCircleEnd(operatin.Next.Data.P ,operatin.Next.Data.Right,true);
                      if (r.p.X < Math.Max( operatin.Next.Data.P.X,operatin.Next.Data.Right.X ))  r = FindCircleEnd(operatin.Next.Data.P ,operatin.Next.Data.Right,false);
                      rp = new Point(r.p.X + r.r, r.p.Y);
                      Console.WriteLine($" hejes {r.p.X} {r.p.Y}");
                      points.Enqueue(new Zdarzenie( operatin.Next.Data.Id, operatin.Next.Data.IdLeft ,operatin.Next.Data.IdRight , r.p ),rp);
                    
                   }
                   
                }
             }
          }
          
       }
      
       
       Console.WriteLine("hej endents");
       return ans;*/
       return new List<Center>();
    }
    private double call()
    {
       double l = 2.4;
       
       double c = 11.84;
       double d = 2.5;
       
       double a = 17.27;
       double b = 10.67;
       double u = 2 * (b - l);
       double v = 2 * (d - l);
       double x = -(Math.Sqrt(v * (Math.Pow(a, 2) * u - 2 * a * c * u + Math.Pow(b, 2) * (u - v) + Math.Pow(c, 2) * u) +
                              Math.Pow(d, 2) * u * (v - u) + Math.Pow(l, 2) * Math.Pow((u - v), 2)) + a * v - c * u) /
                  (u - v);


       return x;
    }
   
    
    
    
    private const double EPS = 1e-12, INF = 1e100;

    private double cross(Point a, Point b)
    {
       return a.X * b.Y - a.Y * b.X;
    }
    private bool collinear(Point a, Point b) {
       return Math.Abs(cross(a,b) ) < EPS;
    }
    

    // dot and cross products
    double dot(Point a ,Point b)  {
       return a.X * b.X + a.Y * b.Y;
    }
    private Point lineline(Point a, Point b, Point c, Point d) {
       return a + (b - a) * (   cross(c - a,d - c) / cross(b - a,d - c));
    }
    
    
    Point circumcenter(Point a, Point b, Point c) {
       b = (a + b) * 0.5;
       c = (a + c) * 0.5;
       return lineline(b, b + (b - a).rot(), c, c + (c - a).rot());
    }
    
    struct arc
    {
       public static double sweepline;
       public Point p, q;
       public int id = 0, i;

       public arc(Point p, Point q, int i)
       {
          this.q = q;
          this.p = p;
          this.i = i;
       }

       // get y coordinate of intersection with following arc.
       // don't question my magic formulas
       
       public double gety(double x) {
          if(q.Y == INF) return INF;
          x += EPS;
          Point med = (p + q) * 0.5;
          Point dir = (p - med).rot();
          double D = (x - p.X) * (x - q.X);
      
          return med.Y + ((med.X - x) * dir.X + Math.Sqrt(D) * dir.len()) / dir.Y;
       }
       public static bool operator <(arc lhs, arc rhs)
       {
          return lhs.gety(arc.sweepline) < rhs.gety(arc.sweepline); 
          
       }

       public static bool operator >(arc lhs, arc rhs)
       {
          return lhs.gety(arc.sweepline) > rhs.gety(arc.sweepline); 
       }
       public static bool operator <(arc lhs, double rhs)
       {
          return lhs.gety(arc.sweepline) < rhs; 
          
       }

       public static bool operator >(arc lhs, double rhs)
       {
          return lhs.gety(arc.sweepline) > rhs; 
       }

       private sealed class ArcRelationalComparer : IComparer<arc>
       {
          public int Compare(arc x, arc y)
          {
             if (x < y) return -1;
             if (x > y) return 1;
             return 0;
          }
       }
      private sealed class Comparer : ICompareDouble<arc>
      {
         public int Compare(arc w, double x)
         {
            if (w < x) return -1;
            if (w > x) return 1;
            return 0;
         }

         public int Compare(double x, arc w)
         {
            if (w > x) return -1;
            if (w < x) return 1;
            return 0;
         }
      }

      public static ICompareDouble<arc> CompareDouble { get; } = new Comparer();
      public static IComparer<arc> ArcComparer { get; } = new ArcRelationalComparer();

     
    };
      
    struct Zdara {
       public double x;
       public int id; 
       public BspTree<arc>.Vertex it;
       public Point point;

       public Zdara(double x, int id, BspTree<arc>.Vertex it, Point point)
       {
          this.x = x;
          this.id = id;
          this.it = it;
          this.point = point;
       }
       
    };
    private class _comparerZdara : IComparer<Zdara>
    {
       public int Compare(Zdara x, Zdara y)
       {
          return x.x.CompareTo(y.x);
       }
    }
   
   
    BspTree<arc> beach; // self explanatory
    private Point[] v;
    private PriorityQueue<Zdara, double> pr;
    // priority queue of point and vertex events
    
    //vector<pii> edges; // delaunay edges
    List<bool> valid; // valid[-id] == true if the vertex event with corresponding id is valid
    int n, ti; // number of points, next available vertex ID
    
    // update the remove event for the arc at position it
    void upd(BspTree<arc>.Vertex it) {
        if(it.Data.i == -1) return; // doesn't correspond to a real point
        valid[-it.Data.id] = false; // mark existing remove event as invalid
        var a = it.Last;
        if(collinear(it.Data.q - it.Data.p, a.Data.p - it.Data.p)) return; // doesn't generate a vertex event
        it.Data.id = --ti; // new vertex event ID
        valid.Add(true); // label this ID true
        Point c = circumcenter(it.Data.p, it.Data.q, a.Data.p);
        double x = c.X + (c - it.Data.p).len();
   
        // event is generated at time x.
        // make sure it passes the sweep-line, and that the arc truly shrinks to 0
        if(x > arc.sweepline - EPS && a.Data.gety(x) + EPS > it.Data.gety(x) && a.Data.gety(x) > -1e8 ) {

            pr.Enqueue(new Zdara(x, it.Data.id, it,c),x);
        }
        
    }
    // add Delaunay edge
    void add_edge(int i, int j) {
        if(i == -1 || j == -1) return;
        //edges.push_back({v[i].second, v[j].second});
    }
    // handle a point event
    void add(int i) {
        Point p = v[i];
        // find arc to split

        var c = beach.LowerBound(p.Y);
        
        // insert new arcs. passing the following iterator gives a slight speed-up
        var b = beach.Add(new arc(p, c.Data.p, i));
        var a = beach.Add(new arc(c.Data.p, p, c.Data.i) ) ;
 
        upd(a); upd(b); upd(c);
    }
    // handle a vertex event
    void remove(BspTree<arc>.Vertex it) {
        var a = it.Last;
        var b = it.Next;
      
        beach.Remove(it);

        a.Data.q = b.Data.p;
        
        add_edge(a.Data.i, b.Data.i );
        upd(a); upd(b);
    }
    // X is a value exceeding all coordinates
   
   public List<Center>  Solve2(List<Point> lp)
   {
      double X = 3e10;

      lp.Sort(new _comparerPoint()) ;

      n = lp.Count;
      v  = new Point[n];
      List<Center> ans = new List<Center>();
      beach = new BspTree<arc>(arc.ArcComparer,arc.CompareDouble);
      lp.ForEach(p=> ans.Add(new Center(p)));

      beach.Add( new arc(new Point (-X, -X), new Point(-X, X), -1));
      //beach.Add(new arc(new Point(-X, X), new Point(INF, INF), -1));

      // create all point events
      Point nic = new Point();
      pr = new PriorityQueue<Zdara, double>();
      int i = 0;
      lp.ForEach(p=> v[i++] = p);
      i = 0;
      lp.ForEach(p => pr.Enqueue( new Zdara(p.X,i++,beach.GetEnd(),nic ), p.X));
      
      ti = 0;
      valid = new List<bool>()
      {
        false
      };
      Zdara e;

      while(pr.Count > 0 ) {
        
         e =pr.Dequeue();
         arc.sweepline = e.x;
         
      
         if(e.id >= 0) {
           
             add(e.id);
         }else if(valid[-e.id])
         {
           
          Console.WriteLine("hescie");
            //ans[ e.it.Last.Data.i].points.Add(e.point);
            //ans[ e.it.Data.i].points.Add(e.point);
            //ans[ e.it.Next.Data.i].points.Add(e.point);
            if (e.it.Last.Data.i > -1 && e.it.Next.Data.i > -1 && e.it.Data.i > -1)
            {
               ans[ e.it.Last.Data.i].Add(e.point,e.it.Data.i , e.it.Next.Data.i); 
               ans[ e.it.Data.i].Add(e.point,e.it.Last.Data.i,e.it.Next.Data.i);
               ans[e.it.Next.Data.i].Add(e.point,e.it.Data.i,e.it.Last.Data.i);
            }
            //Console.WriteLine($"{e.it.Last.Data.i} ,{e.it.Data.i} ,{e.it.Next.Data.i} ");
            //Console.WriteLine("to ok ?");
            remove(e.it);
            
         }
      }
      Console.WriteLine("koniecasd");
      return ans;
      }
    

}