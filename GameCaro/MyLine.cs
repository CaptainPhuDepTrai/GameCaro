using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    class MyLine : ISerializable
    {

    private static  long serialVersionUID = 1L;
    private DrawLine.Double l = new Line2D.Double();
    private int indexPointA, indexPointB;
    private int cost;

     int barb = 10;
     int r = 15;
     double phi = Math.PI / 6;

    public MyLine(Line2D.Double l, int indexPointA, int indexPointB, int cost)
    {
        this.cost = cost;
        this.indexPointA = indexPointA;
        this.indexPointB = indexPointB;
        this.l = l;
    }

    private void drawArrow(Graphics g, double theta, double x0, double y0,
            Color colorLine, int size)
    {
        double x = x0 - barb * Math.Cos(theta + phi);
        double y = y0 - barb * Math.Sin(theta + phi);
        g.stroke(new BasicStroke(size));
        g.draw(new Line2D.Double(x0, y0, x, y));
        x = x0 - barb * Math.cos(theta - phi);
        y = y0 - barb * Math.sin(theta - phi);
        g.draw(new Line2D.Double(x0, y0, x, y));
    }

    public void drawLine(Graphics g, Point p1, Point p2, Color colorCost,
            Color colorLine, int size, Boolean type)
    {
        String c = "";
        if (cost < 0)
        {
            c = "";
        }
        else
            c = String.valueOf(cost);
        g.GetNearestColor(colorLine);
        g.setStroke(new BasicStroke(size));
        double theta = Math.atan2(p2.y - p1.y, p2.x - p1.x);
        g.DrawArc(l);
        if (type && cost >= 0)
        {
            double x = p2.x - r * Math.Cos(theta);
            double y = p2.y - r * Math.Sin(theta);
            drawArrow(g, theta, x, y, colorLine, size);
        }

        g.GetNearestColor(colorCost);
        g.DrawString(c, (int)(Math.Abs(p1.x + p2.x) / 2),
                (int)(p1.y + p2.y) / 2);
    }

    public Boolean containerPoint(Point p)
    {
        Polygon poly = createPolygon(l);
        for (int i = 0; i < poly.npoints; i++)
        {
            double temp = (p.x - poly.xpoints[i])
                    * (poly.ypoints[(i + 1) % poly.npoints] - poly.ypoints[i])
                    - (p.y - poly.ypoints[i])
                    * (poly.xpoints[(i + 1) % poly.npoints] - poly.xpoints[i]);
            if (temp < 0)
                return false;
        }
        return true;
    }

    private Polygon createPolygon(Line2D line)
    {
        int barb = 5;
        double phi = Math.PI / 2;
        double theta = Math.atan2(line.getY2() - line.getY1(), line.getX2()
                - line.getX1());
        int x[] = new int[4];
        int y[] = new int[4];
        x[0] = (int)(line.getX1() - barb * Math.cos(theta + phi));
        y[0] = (int)(line.getY1() - barb * Math.sin(theta + phi));
        x[1] = (int)(line.getX1() - barb * Math.cos(theta - phi));
        y[1] = (int)(line.getY1() - barb * Math.sin(theta - phi));

        x[2] = (int)(line.getX2() - barb * Math.cos(theta - phi));
        y[2] = (int)(line.getY2() - barb * Math.sin(theta - phi));
        x[3] = (int)(line.getX2() - barb * Math.cos(theta + phi));
        y[3] = (int)(line.getY2() - barb * Math.sin(theta + phi));
        Polygon poly = new Polygon(x, y, 4);
        return poly;
    }

    public Line2D.Double getL()
    {
        return l;
    }

    public void setL(Line2D.Double l)
    {
        this.l = l;
    }

    public int getIndexPointA()
    {
        return indexPointA;
    }

    public void setIndexPointA(int indexPointA)
    {
        this.indexPointA = indexPointA;
    }

    public int getIndexPointB()
    {
        return indexPointB;
    }

    public void setIndexPointB(int indexPointB)
    {
        this.indexPointB = indexPointB;
    }

    public int getCost()
    {
        return cost;
    }

    public void setCost(int cost)
    {
        this.cost = cost;
    }
}
}
