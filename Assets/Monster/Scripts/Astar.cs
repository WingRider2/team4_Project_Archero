using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Astar 
{
    static int[] dx = { 1, 0, -1, 0 };
    static int[] dy = { 0, 1, 0, -1 };
    static int[] ddx = { 1, 1, -1, -1 };
    static int[] ddy = { 1, -1, 1, -1 };
    private bool IsObs(int x,int  y)
    {
      

        int mask = LayerMask.GetMask("Obstacle");
        //장애물/벽 확인
        Collider2D col = Physics2D.OverlapBox(new Vector2(x,y),new Vector2(1,1), mask);

        if (col != null)
            Debug.Log($"장애물 발견{x},{y}: {col.name}");
        else
            Debug.Log($"({x},{y})에 장애물 없음");
        return col != null;
       
    }
   
    public List<Vector2> MakePath(Node lastNode)
    {
        List<Vector2> path=new List<Vector2>();
        while (lastNode != null) {
            Debug.Log(lastNode.x + ", " + lastNode.y);
            path.Add(new Vector2(lastNode.x,lastNode.y));
            lastNode = lastNode.parent;
            
        }
      
        path.Reverse();
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i], path[i + 1], Color.green, 2f);
        }
        return path;
    }
    
    public List<Vector2> FindPath(Vector2 start, Vector2 target)
    {

        Debug.Log("시작");
        List<Node> openNodes = new List<Node>();
        HashSet<Vector2> closedNodes = new HashSet<Vector2>();

        Node currentNode = new Node((int)start.x,(int)start.y,0);
        Node targetNode = new Node((int)target.x, (int)target.y, 0);
        currentNode.SetH(targetNode);
        openNodes.Add(currentNode);
      
        int loopNum = 0;
        while (openNodes.Count > 0)
        {
            currentNode = openNodes[0];
            foreach (Node node in openNodes) { 
                if(node.f<currentNode.f)
                    currentNode = node;
            }
            if (currentNode.x == targetNode.x && currentNode.y == targetNode.y) {
               
                return MakePath(currentNode);
                
            }
            closedNodes.Add(new Vector2(currentNode.x,currentNode.y));
            openNodes.Remove(currentNode);
            for (int i = 0; i < 4; i++)
            {
                int nx=dx[i]+currentNode.x;
                int ny=dy[i]+currentNode.y;
                if(IsObs(nx,ny))
                    continue;
                Node nowNode= new Node(nx,ny,currentNode.g+10);
                nowNode.SetH(targetNode);
                nowNode.parent = currentNode;
                if(closedNodes.Contains(new Vector2(nowNode.x,nowNode.y)))
                    continue;
                Node findN = openNodes.Find(n => n.x == nx && n.y == ny);
                if(findN == null)
                    openNodes.Add(nowNode);
                else if (nowNode.g < findN.g)
                {   
                    findN.g= nowNode.g;
                    findN.parent = currentNode;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                int nx = ddx[i] + currentNode.x;
                int ny = ddy[i] + currentNode.y;
                if (IsObs(nx, ny))
                    continue;
                Node nowNode = new Node(nx, ny, currentNode.g + 14);
                nowNode.SetH(targetNode);
                nowNode.parent = currentNode;
                if (closedNodes.Contains(new Vector2(nowNode.x, nowNode.y)))
                    continue;
                Node findN = openNodes.Find(n => n.x == nx && n.y == ny);
                if (findN == null)
                    openNodes.Add(nowNode);
                else if (nowNode.g < findN.g)
                {
                    findN.g = nowNode.g;
                    findN.parent = currentNode;
                }
            }
            if (loopNum++ > 10000)
                throw new Exception("Infinite Loop");
        }
        return null;
    }
}
public class Node
{
    public int x;
    public int y;
    public int f =>g+h;
    public int g;
    public int h;
    public Node parent;
    public Node(int x,int y,int g)
    {
        this.x = x;
        this.y = y;
        this.g = g;
  
    }
    public void SetH(Node target)
    {
        h = Mathf.Abs(target.x - x) + Mathf.Abs(target.y - y);
        h *= 10;
        
    }
    public bool IsEqual(Node other)
    {
        if(other == null) return false;
        return (this.x == other.x && this.y == other.y);
    }
}