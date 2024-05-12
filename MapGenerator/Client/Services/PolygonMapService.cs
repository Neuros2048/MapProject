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
       public static double Lmax = 80;
       public Point P = p;
       public Point Left = left;
       public Point Right = right;
       public int Id = id;
       public int IdLeft = idLeft;
       public int IdRight = idRight;
    }

    private class ComparerPosition : IComparer<Position>
    {
       public int Compare(Position x, Position y)
       {
          double a = Przeciecie(x.Left, x.P);
          double b = Przeciecie(x.P, x.Right);
          double c = Przeciecie(y.Left, y.P);
          double d = Przeciecie(y.P, y.Right);
          if ((a <= c && d <= b) || (c <= a && b <= d)) return 0;
          if (a < c) return -1;
          return 1;

       }
       
       private double Przeciecie( Point p1 , Point p2)
       {
          if (p1.Y <= Position.Lmin) return Position.Lmin;
          if (p2.Y >= Position.Lmax) return Position.Lmax;
          double l = Position.L;
          double a = p1.Y;
          double b = p1.X;
          double c = p1.Y;
          double d = p1.X;
          double u = 2 * (b - l);
          double v = 2 * (d - l);
          return -(Math.Sqrt(v * (Math.Pow(a, 2) * u - 2 * a * c * u + Math.Pow(b, 2) * (u - v) + Math.Pow(c, 2) * u) +
                                 Math.Pow(d, 2) * u * (v - u) + Math.Pow(l, 2) * Math.Pow((u - v), 2)) + a * v - c * u) /
                     (u - v);
          
       }
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

       public PointShape(Point p)
       {
          P = p;
          Right = this;
          Left = this;
       }
    }


    private double Distance(Point p1, Point p2)
    {
       return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
    }
    private Circle FindCircleEnd(Point p1, Point p2, bool left)
    {
       double Ymax = 800;
       double ymin = 0;
       double x = (p1.X + p2.X) / 2;
       double y = (p1.Y + p1.Y) / 2;
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


       if (left)
       {
          if (vy > 0)
          {
             vy *= -1;
             vx *= -1;
          }
       }
       else
       {
          if (vy < 0)
          {
             vy *= -1;
             vx *= -1;
          }
       }

       if (vx < 0)
       {
          double ileX = (0 - x) / vx;
          double newY = y + vy * ileX;
          if (newY < ymin || newY > Ymax)
          {
              if(vy < 0)
              {
                 double ileY = (0 - y) / vy;
                 double newX = x + vx * ileY;
                 var res = new Point(newX,Ymax );
                 return new Circle(res, Distance(res, p1));
              }
              else
              {
                 double ileY = (Ymax - y) / vy;
                 double newX = x + vx * ileY;
                 var res = new Point(newX,Ymax );
                 return new Circle(res, Distance(res, p1));
              }
          }
          else
          {
             var res = new Point(0, newY);
             return new Circle(res, Distance(p1 , res )  );
          }
       }
       else if(left)
       {
          double ileY = (0 - y) / vy;
          double newX = x + vx * ileY;
          var res = new Point(newX,Ymax );
          return new Circle(res, Distance(res, p1));
       }
       else
       {
          double ileY = (Ymax - y) / vy;
          double newX = x + vx * ileY;
          var res = new Point(newX,Ymax );
          return new Circle(res, Distance(res, p1));
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
          this.Id = id;
          this.P = p;
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
       public List<Point> points;
       public PointShape? Left;
       public PointShape? Right;
       public bool started = false;
       public Center(Point p)
       {
          P = p;
          points = new List<Point>();
       }
       
    }
    private class line
    {
       public Point p;
       public line? top;
       public line? down;
    }
    double X0 = 0, X1 = 800, Y0 = 0, Y1 = 800;
    

    public List<Center> Solve(List<Point> Spoints)
    {
       points = new PriorityQueue<Zdarzenie, Point>(new _comparerPoint());
       Point ps1 = new Point(-800, -800);
       Point ps2 = new Point(-800, 1600);
       List<Center> ans = new List<Center>();
       int j = 0;
       Spoints.ForEach( p => points.Enqueue(new Zdarzenie(j++,p),p));
       Spoints.ForEach(p => ans.Add(new Center(p)));
       BspTree<Position> tree = new BspTree<Position>(new ComparerPosition());
       Zdarzenie cur = points.Dequeue();
       Position.L = cur.P.X;
       tree.Add(new Position(cur.P, cur.Id ,new Point(-1, -1), -1 ,new Point(-1, 801), -1 ));
       
      
       Console.WriteLine("dsadasda "+ call());
       while (points.Count > 0)
       {
          cur = points.Dequeue();
          Position.L = cur.P.X;
          if (cur.Typ)
          {
             var gdzie = tree.FindFirst(new Position(cur.P,cur.Id , new Point(cur.P.X -10,cur.P.Y-10) , -1, new Point(cur.P.X-10,cur.P.Y+10), -1));
             tree.Remove(gdzie);
             Position p1 = new Position(cur.P, cur.Id, gdzie.Data.P, gdzie.Data.Id, gdzie.Data.P, gdzie.Data.Id);
             Position p2 = new Position(gdzie.Data.P, gdzie.Data.Id, gdzie.Data.Left, gdzie.Data.IdLeft, cur.P, cur.Id);
             Position p3 = new Position(gdzie.Data.P, gdzie.Data.Id,cur.P, cur.Id, gdzie.Data.Right, gdzie.Data.IdRight);
             tree.Add(p1);
             tree.Add(p2);
             tree.Add(p3);
             Circle r;
             Point rp;
             if (p2.IdLeft != -1)
             {
                r = FindCircleEnd(p3.Right ,p3.P,true);
             }
             else
             {
                r = FindCircle(p2);
                
             }
             r.p.X += r.r;
             rp = new Point(r.p.X + r.r, r.p.Y);
             points.Enqueue(new Zdarzenie( p2.Id, p2.IdLeft,p2.IdRight , r.p ),rp);
             if (p3.IdRight != -1)
             {
                r = FindCircleEnd(p3.Left ,p3.P,false);
             }
             else
             {
                r = FindCircle(p3);
             }
             r.p.X += r.r;
             rp = new Point(r.p.X + r.r, r.p.Y);
             points.Enqueue(new Zdarzenie( p3.Id, p3.IdLeft,p3.IdRight , r.p ),rp);
             
          }
          else
          {
             Position szukane;
             if (cur.IdLeft != -1 && cur.IdRight!= -1)
             {
                szukane = new Position(ans[cur.Id].P, cur.Id,ans[cur.IdLeft].P, cur.IdLeft ,ans[cur.IdRight].P, cur.IdRight );
                if (tree.Exist(szukane))
                {
                   ans[cur.Id].points.Add(cur.P);
                   ans[cur.IdLeft].points.Add(cur.P);
                   ans[cur.IdRight].points.Add(cur.P);
                   var operatin = tree.Get(szukane);
                   tree.Remove(szukane);
                   operatin.Last.Data.Right = operatin.Next.Data.P;
                   operatin.Last.Data.IdRight = operatin.Next.Data.Id;
                   operatin.Next.Data.Left = operatin.Last.Data.P ;
                   operatin.Next.Data.IdLeft = operatin.Last.Data.Id ;
                   Circle c;
                   Point cp;
                   if (operatin.Next.Data.P.X <  operatin.Next.Data.Left.X || operatin.Next.Data.P.X <  operatin.Next.Data.Right.X)
                   {
                      c = FindCircle(operatin.Next.Data);
                      cp = new Point(c.p.X + c.r , c.p.Y);
                      points.Enqueue(new Zdarzenie(operatin.Next.Data, c.p), cp);
                     
                   }
                   if(operatin.Last.Data.P.X <  operatin.Last.Data.Left.X || operatin.Last.Data.P.X <  operatin.Last.Data.Right.X)
                   {
                      c = FindCircle(operatin.Last.Data);
                      cp = new Point(c.p.X + c.r , c.p.Y);
                      points.Enqueue( new Zdarzenie(operatin.Last.Data ,c.p) ,  cp);
                   }
                }
                
             }else if (cur.IdRight == -1)
             {
                szukane = new Position(ans[cur.Id].P, cur.Id,ans[cur.IdLeft].P, cur.IdLeft ,new Point(-1,801), -1);
                if (tree.Exist(szukane))
                {
                   var operatin = tree.Get(szukane);
                   tree.Remove(szukane);
                   ans[cur.Id].points.Add(cur.P);
                   ans[cur.IdLeft].points.Add(cur.P);
                   operatin.Last.Data.Right = operatin.Data.Right;
                   operatin.Last.Data.IdRight = operatin.Data.IdRight;
                   if (operatin.Last.Data.Left.X > operatin.Last.Data.P.X)
                   {
                      var r = FindCircleEnd( operatin.Last.Data.Left , operatin.Last.Data.P,false);
                      r.p.X += r.r;
                      var rp = new Point(r.p.X + r.r, r.p.Y);
                      points.Enqueue(new Zdarzenie( operatin.Last.Data.Id, operatin.Last.Data.IdLeft,operatin.Last.Data.IdRight , r.p ),rp);
                   }
                }
             }
             else
             {
                szukane = new Position(ans[cur.Id].P, cur.Id,new Point(-1,-1),-1 ,ans[cur.IdRight].P, cur.IdRight );
                if (tree.Exist(szukane))
                {
                   var operatin = tree.Get(szukane);
                   tree.Remove(szukane);
                   ans[cur.Id].points.Add(cur.P);
                   ans[cur.IdRight].points.Add(cur.P);
                   operatin.Next.Data.Left = operatin.Data.Left;
                   operatin.Next.Data.IdLeft = operatin.Data.IdLeft;
                   if (operatin.Next.Data.Right .X > operatin.Next.Data.P.X)
                   {
                      var r = FindCircleEnd( operatin.Next.Data.Right , operatin.Next.Data.P,true);
                      r.p.X += r.r;
                      var rp = new Point(r.p.X + r.r, r.p.Y);
                      points.Enqueue(new Zdarzenie( operatin.Next.Data.Id, operatin.Next.Data.IdLeft,operatin.Next.Data.IdRight , r.p ),rp);
                   }
                }
             }
          }
          
       }
      
       
       Console.WriteLine("hej endents");
       return ans;
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
   /*
    var nowy = tree.Add(new Position(cur.P, cur.Id,cur.P, cur.Id,cur.P, cur.Id ));
             var cir = FindCircle(nowy.Last.Data.P, nowy.Data.P, nowy.Next.Data.P);
             cir.p.X += cir.r;
             Point cir_p = new Point(cir.p.X + cir.r, cir.p.Y);
             points.Enqueue(new Zdarzenie(nowy.Last.Data.Id,nowy.Data.Id,nowy.Next.Data.Id,cir.p),cir_p );
             if (nowy.Next.Data.Id != -1)
             {
                cir = FindCircle(nowy.Data.P, nowy.Next.Data.P, nowy.Next.Next.Data.P);
                cir_p = new Point(cir.p.X + cir.r, cir.p.Y);
                points.Enqueue(new Zdarzenie(nowy.Last.Data.Id,nowy.Data.Id,nowy.Next.Data.Id,cir.p),cir_p );
             }
             if (nowy.Last.Data.Id != -1)
             {
                cir = FindCircle(nowy.Last.Last.Data.P, nowy.Last.Data.P, nowy.Data.P);
                cir_p = new Point(cir.p.X + cir.r, cir.p.Y);
                points.Enqueue(new Zdarzenie(nowy.Last.Data.Id,nowy.Data.Id,nowy.Next.Data.Id,cir.p),cir_p );
             }
             
             
             
    private class _comparerEvents : IComparer<eventt>
    {
       public int Compare(eventt? x, eventt? y)
       {
          return x.x.CompareTo(y.x);
       }
    }
    
    public int Solve(List<Point> Spoints)
{

   
class eventt {

       public double x;
       public Point p;
       public arc a;
       public bool valid;

       public eventt(double x, Point p, arc a)
       {
          this.x = x;
          this.p = p;
          this.a = a;
          valid = true;
       }
       
    };
       private arc? root = null;
        private PriorityQueue<eventt,eventt > events;
    private static List<seg> output;
    class seg {
       public Point start, end;
       public bool done;

       public seg(Point p)
       {
          start = p;
          end.X = 0;
          end.Y = 0;
          done = false;
          output.Add(this);
       }
       // Set the end point and mark as "done."
       public void finish(Point p) { if (done) return; end = p; done = true; }
    };
    class arc {
         public Point p;
         public arc? prev;
         public arc? next;
         public eventt? e = null;
         public seg? s0 = null, s1=null;

         public arc(Point p, arc a = null, arc b = null)
         {
            this.p = p;
            this.prev = a;
            this.next = b;
         }
      
   };

   Console.WriteLine("uruchomione");
   Point p;
   output = new List<seg>();
   points = new PriorityQueue<Point, Point>(new _comparerPoint());
   events = new PriorityQueue<eventt, eventt>(new _comparerEvents());
   Spoints.ForEach( p => points.Enqueue(p,p));
   
   
   double dx = (X1-X0+1)/5.0, dy = (Y1-Y0+1)/5.0;
   X0 -= dx;  X1 += dx;  Y0 -= dy;  Y1 += dy;

   // Process the queues; select the top element with smaller x coordinate.
   while (points.Count > 0)
   {
      if (events.Count > 0 && events.Peek().x <= points.Peek().x)
         process_event();
      else
         process_point();
   }

   // After all points are processed, do the remaining circle events.
   while (events.Count > 0)
      process_event();

   finish_edges(); // Clean up dangling edges.
   print_output();
   return 1;
}

void process_point()
{
   // Get the next point from the queue.
   Point p = points.Dequeue();

   // Add a new arc to the parabolic front.
   front_insert(p);
}

void process_event()
{
   // Get the next event from the queue.
   eventt e = events.Dequeue();
   

   if (e.valid) {
      // Start a new edge.
      seg s = new seg(e.p);

      // Remove the associated arc from the front.
      arc a = e.a;
      if (a.prev != null) {
         a.prev.next = a.next;
         a.prev.s1 = s;
      }
      if (a.next != null) {
         a.next.prev = a.prev;
         a.next.s0 = s;
      }

      // Finish the edges before and after a.
      if (a.s0!= null) a.s0.finish(e.p);
      if (a.s1 != null) a.s1.finish(e.p);

      // Recheck circle events on either side of p:
      if (a.prev!=null) check_circle_event(a.prev, e.x);
      if (a.next!=null) check_circle_event(a.next, e.x);
   }
   
}

void front_insert(Point p)
{
   if (root == null) {
      root = new arc(p);
      return;
   }

   // Find the current arc(s) at height p.y (if there are any).
   arc i;
   for (i = root; i!=null; i = i.next) {
      Point z , zz;
      z = new Point();
      zz = new Point();
      if (intersect(p,i,z)) {
         // New parabola intersects arc i.  If necessary, duplicate i.
         if (i.next!=null && !intersect(p,i.next, zz)) {
            i.next.prev = new arc(i.p,i,i.next);
            i.next = i.next.prev;
         }
         else i.next = new arc(i.p,i);
         i.next.s1 = i.s1;

         // Add p between i and i.next.
         i.next.prev = new arc(p,i,i.next);
         i.next = i.next.prev;

         i = i.next; // Now i points to the new arc.

         // Add new half-edges connected to i's endpoints.
         i.prev.s1 = i.s0 = new seg(z);
         i.next.s0 = i.s1 = new seg(z);

         // Check for new circle events around the new arc:
         check_circle_event(i, p.x);
         check_circle_event(i.prev, p.x);
         check_circle_event(i.next, p.x);

         return;
      }
   }

   // Special case: If p never intersects an arc, append it to the list.
   for (i = root; i.next!=null; i=i.next) ; // Find the last node.

   i.next = new arc(p,i);
   // Insert segment between p and i
   Point start = new Point();
   start.x = X0;
   start.y = (i.next.p.y + i.p.y) / 2;
   i.s1 = i.next.s0 = new seg(start);
}

// Look for a new circle event for arc i.
void check_circle_event(arc i, double x0)
{
   // Invalidate any old event.
   
   if (i.e!=null && i.e.x != x0)
      i.e.valid = false;
   i.e = null;

   if (i.prev==null || i.next == null)
      return;

   double x = 0;
   Point o = new Point();

   if (circle(i.prev.p, i.p, i.next.p,ref x,o) && x > x0) {
      // Create new event.
      i.e = new eventt(x, o, i);
      events.Enqueue(i.e,i.e);
   }
}

// Find the rightmost point on the circle through a,b,c.
bool circle(Point a, Point b, Point c, ref double  x, Point o)
{
   // Check that bc is a "right turn" from ab.
   if ((b.x-a.x)*(c.y-a.y) - (c.x-a.x)*(b.y-a.y) > 0)
      return false;

   // Algorithm from O'Rourke 2ed p. 189.
   double A = b.x - a.x,  B = b.y - a.y,
          C = c.x - a.x,  D = c.y - a.y,
          E = A*(a.x+b.x) + B*(a.y+b.y),
          F = C*(a.x+c.x) + D*(a.y+c.y),
          G = 2*(A*(c.y-b.y) - B*(c.x-b.x));

   if (G == 0) return false;  // Points are co-linear.

   // Point o is the center of the circle.
   o.x = (D*E-B*F)/G;
   o.y = (A*F-C*E)/G;

   // o.x plus radius equals max x coordinate.
   x = o.x +   Math.Sqrt(  Math.Pow(a.x - o.x, 2) + Math.Pow(a.y - o.y, 2) );
   return true;
}

// Will a new parabola at point p intersect with arc i?
bool intersect(Point p, arc i, Point res)
{
   if (i.p.x == p.x) return false;

   double a =0 ,b = 0 ;
   if (i.prev!=null) // Get the intersection of i.prev, i.
      a = intersection(i.prev.p, i.p, p.x).y;
   if (i.next !=null) // Get the intersection of i.next, i.
      b = intersection(i.p, i.next.p, p.x).y;

   if ((i.prev==null || a <= p.y) && (i.next == null || p.y <= b)) {
      res.y = p.y;

      // Plug it back into the parabola equation.
      res.x = (i.p.x*i.p.x + (i.p.y-res.y)*(i.p.y-res.y) - p.x*p.x)
                / (2*i.p.x - 2*p.x);

      return true;
   }
   return false;
}

// Where do two parabolas intersect?
Point intersection(Point p0, Point p1, double l)
{
   Point res = new Point(), p = p0;

   if (p0.x == p1.x)
      res.y = (p0.y + p1.y) / 2;
   else if (p1.x == l)
      res.y = p1.y;
   else if (p0.x == l) {
      res.y = p0.y;
      p = p1;
   } else {
      // Use the quadratic formula.
      double z0 = 2*(p0.x - l);
      double z1 = 2*(p1.x - l);

      double a = 1/z0 - 1/z1;
      double b = -2*(p0.y/z0 - p1.y/z1);
      double c = (p0.y*p0.y + p0.x*p0.x - l*l)/z0
               - (p1.y*p1.y + p1.x*p1.x - l*l)/z1;

      res.y = ( -b - Math.Sqrt(b*b - 4*a*c) ) / (2*a);
   }
   // Plug back into one of the parabola equations.
   res.x = (p.x*p.x + (p.y-res.y)*(p.y-res.y) - l*l)/(2*p.x-2*l);
   return res;
}

void finish_edges()
{
   // Advance the sweep line so no parabolas can cross the bounding box.
   double l = X1 + (X1-X0) + (Y1-Y0);

   // Extend each remaining segment to the new parabola intersections.
   for (arc i = root; i.next!=null; i = i.next)
      if (i.s1!=null)
         i.s1.finish(intersection(i.p, i.next.p, l*2));
}


void print_output()
{
   // Bounding box coordinates.


   // Each output segment in four-column format.
   
   for (int i = 0 ; i < output.Count; i++) {
      Point p0 = output[i].start;
      Point p1 = output[i].end;
      Console.WriteLine(p0.x + " " + p0.y +" " +p1.x + " " + p1.y);
   }
}*/

   
    
}