namespace Client.Logic;

public class Poly2
{
    public Poly2(int xMax, int xMin, int yMax, int yMin)
    {
        this.xMax = xMax;
        this.xMin = xMin;
        this.yMax = yMax;
        this.yMin = yMin;
    }

    public class Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point()
        {
            X = 0;
            Y = 0;
        }
    }

    private int xMax;
    private int xMin;
    private int yMax;
    private int yMin;
 
    
    public class Edge
    {
        public Edge Next;
        public Edge Previous;
        public Point Point;

        public Edge( Edge previous,Edge next, Point point)
        {
            Next = next;
            Previous = previous;
            Point = point;
            Next.Previous = this;
            previous.Next = this;
        }

        public Edge(Point point)
        {
            Next = this;
            Previous = this;
            Point = point;
        }
    }
    
    public class Center
    {
        public Point P;
        public Edge E;
        public List<Point> Points;

        public Center(Point p,int xMax,int xMin,int yMax,int yMin)
        {
            P = p;
            E = new Edge(new Point(xMin, yMin));
            E = new Edge(E, E.Next, new Point(xMin, yMax));
            E = new Edge(E, E.Next, new Point(xMax, yMax));
            E = new Edge(E, E.Next, new Point(xMax, yMin));
            Points = new List<Point>();

        }
    }
    
   
        
   
    
    public List<Center> Solve(List<Center> centers)
    {
        //List<Center> centers = new List<Center>();
       

        int n = centers.Count;
        Edge inter1 = centers[0].E, inter2;
        Edge end, curr;
        Point m = new Point(-1, -1);
        Point a,b,c,d = m;
        int o1, o2;
        bool one;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i != j)
                {
                    PunktyGraniczne(centers[i].P,centers[j].P,out a  ,out b);
                    /*centers[i].Points.Add(a);
                    centers[i].Points.Add(b);
                    centers[j].Points.Add(a);
                    centers[j].Points.Add(b);*/
                    end = centers[i].E;
                    curr = end;
                    one = false;
                    do
                    {

                        o1 = orientation(a, b, curr.Previous.Point);
                        o2 = orientation(a, b, curr.Point);
                        if (o1 == 0 || o2 == 0 || o1 != o2)
                        {
                            c = lineLineIntersection(a, b, curr.Previous.Point, curr.Point);

                            if (!one)
                            {
                                one = true;
                                d = c;
                                inter1 = curr;

                            }
                            else
                            {
                                inter2 = curr;
                              
                                if (orientation(d, c, centers[i].P) > 0)
                                {
                                    
                                    centers[i].E = new Edge(inter1.Previous, inter2, d);
                                    centers[i].E = new Edge(inter1.Previous.Next, inter2, c);
                                }
                                else
                                {
                                   
                                    centers[i].E = new Edge(inter2.Previous, inter1, c);
                                    centers[i].E = new Edge(inter2.Previous.Next, inter1, d);
                                }

                                break;
                            }

                        }


                        /*if (c.X != -1)
                        {

                            else
                            {
                                inter2 = curr;
                                orientation(a,b)

                                Console.WriteLine("hej4");
                                if (CrossProduct(d, c,centers[i].P ) )
                                {
                                    inter1.Previous = inter2;
                                    inter2.Next = inter1;
                                    centers[i].E = new Edge(inter2, inter1, d);
                                    centers[i].E = new Edge(inter2, inter1.Previous, c);
                                }
                                else
                                {
                                    inter1.Next = inter2;
                                    inter2.Previous = inter1;
                                    centers[i].E = new Edge(inter1, inter2, c);
                                    centers[i].E = new Edge(inter1, inter2.Previous, d);
                                }
                                Console.WriteLine("hej5");
                                break;
                            }

                        }*/
                        curr = curr.Next;

                    } while (curr != end);

                }
            }
        }
        return centers;
    }
    
    public  int orientation(Point p1, Point p2,
        Point p3)
    {
        // See 10th slides from following link 
        // for derivation of the formula
        int val = (p2.Y - p1.Y) * (p3.X - p2.X) -
                  (p2.X - p1.X) * (p3.Y - p2.Y);
 
        if (val == 0) return 0; 
        return (val > 0)? 1: -1; 
    }
 
   
    
    public static Point DoLinesIntersect(Point x1, Point x2, Point x3, Point x4)
    {
        int dx1 = x2.X - x1.X;
        int dy1 = x2.Y - x1.Y;
        int dx2 = x4.X - x3.X;
        int dy2 = x4.Y - x3.Y;

        int crossProduct1 = dx1 * (x3.Y - x1.Y) - dy1 * (x3.X - x1.X);
        int crossProduct2 = dx1 * (x4.Y - x1.Y) - dy1 * (x4.X - x1.X);

        int crossProduct3 = dx2 * (x1.Y - x3.Y) - dy2 * (x1.X - x3.X);
        int crossProduct4 = dx2 * (x2.Y - x3.Y) - dy2 * (x2.X - x3.X);

        
        int d = dx1 * dy2 - dy1 * dx2;
        if (d == 0)
        {
            
            return new Point(-1, -1);
        }

       
        if (crossProduct1 * crossProduct2 <= 0 && crossProduct3 * crossProduct4 <= 0)
        {
            int px = (x1.X * dy1 * dx2 + x1.Y * dx1 * dx2 - x3.X * dy2 * dx1 + x3.Y * dy1 * dx2) / d;
            int py = (x1.Y * dx1 * dy2 + x3.X * dy2 * dy1 - x1.X * dy1 * dy2 + x3.Y * dx1 * dy2) / d;
            return new Point(px, py);
        }

        return new Point(-1, -1);
    }
    
    private bool CrossProduct(Point a, Point b, Point c)
    {
        return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X)<0;
    }

    public static Point lineLineIntersection(Point A, Point B, Point C, Point D)
    {
        double a1 = B.Y - A.Y;
        double b1 = A.X - B.X;
        double c1 = a1 * (A.X) + b1 * (A.Y);
        double a2 = D.Y - C.Y;
        double b2 = C.X - D.X;
        double c2 = a2 * (C.X) + b2 * (C.Y);
 
        double determinant = a1 * b2 - a2 * b1;
 
        if (determinant == 0)
        {
            return new Point(-1, -1);
        }
        else
        {
            double x = (b2 * c1 - b1 * c2) / determinant;
            double y = (a1 * c2 - a2 * c1) / determinant;
            return new Point((int) Math.Round(  x),(int) Math.Round(y));
        }
    }
    private void PunktyGraniczne(Point p1,Point p2, out Point p3, out Point p4)
    {

        int x = (p1.X + p2.X) / 2;
        int y = (p1.Y + p2.Y) / 2;
        int vy =-1* (p2.X - p1.X);
        int vx = p2.Y - p1.Y;
         
       
       
       if (vx == 0)
       {
           p3 = new Point(x, yMin);
           p4 = new Point(x, yMax); 
           return;
       }

       if (vy == 0)
       {
           p3 = new Point(xMax, y);
           p4 = new Point(yMin, y);
           return;
       }

       int wx, wy;
      
       if (vx < 0 && vy < 0)
       {
           if ((x - xMin) * vy * -1 < (y - yMin) * vx * -1)
           {
               wx = xMin;
               wy = y - (x - xMin) * vy / vx;
           }
           else
           {
               wx = x - (y - yMin) * vx / vy;
               wy = yMin;
           }
       }
       else if (vx < 0 && vy > 0)
       {
           if ((x- xMin) * vy < ( yMax - y) * vx *-1)
           {
               wx = xMin;
               wy = y - (x- xMin) * vy / vx;
           }
           else
           {
               wx = x + ( yMax - y) * vx / vy;
               wy = yMax;
           }
       }
       else if (vx > 0 && vy < 0)
       {
           if ((xMax - x) * vy * -1 < (y - yMin) * vx)
           {
               wx = xMax;
               wy = y + (xMax - x) * vy / vx;
           }
           else
           {
               wx = x - (y - yMin) * vx / vy;
               wy = yMin;
           }
       }
       else //if (vx > 0 && vy > 0)
       {
           if ((xMax - x) * vy < (yMax - y) * vx)
           {
               wx = xMax;
               wy = y + (xMax - x) * vy / vx;
           }
           else
           {
               wx = x + (yMax - y) * vx / vy;
               wy = yMax;
           }
       }
       p3 = new Point(wx,wy);
       vx *= -1;
       vy *= -1;
       if (vx < 0 && vy < 0)
       {
           if ((x - xMin) * vy * -1 < (y - yMin) * vx * -1)
           {
               wx = xMin;
               wy = y - (x - xMin) * vy / vx;
           }
           else
           {
               wx = x - (y - yMin) * vx / vy;
               wy = yMin;
           }

       }
       else if (vx < 0 && vy > 0)
       {
           if ((x- xMin) * vy < ( yMax - y) * vx *-1)
           {
               wx = xMin;
               wy = y - (x- xMin) * vy / vx;
           }
           else
           {
               wx = x + ( yMax - y) * vx / vy;
               wy = yMax;
           }
       }
       else if (vx > 0 && vy < 0)
       {
           if ((xMax - x) * vy * -1 < (y - yMin) * vx)
           {
               wx = xMax;
               wy = y + (xMax - x) * vy / vx;
           }
           else
           {
               wx = x - (y - yMin) * vx / vy;
               wy = yMin;
           }
       }
       else //if (vx > 0 && vy > 0)
       {
           if ((xMax - x) * vy < (yMax - y) * vx)
           {
               wx = xMax;
               wy = y + (xMax - x) * vy / vx;
           }
           else
           {
               wx = x + (yMax - y) * vx / vy;
               wy = yMax;
           }
       }
       p4 = new Point(wx,wy);
       return;
    }
}