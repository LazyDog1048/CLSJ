using System.Collections.Generic;
using game;
using game.manager;
using UnityEngine;

public static class VectorThing
{
    public static Vector3 GetAroundPos(this Vector3 point,PointDir pointDir)
    {
        switch (pointDir)
        {
            case PointDir.Up:
                point += Vector3Int.up;
                break;
            case PointDir.RUp:
                point += Vector3Int.right + Vector3Int.up;
                break;
            case PointDir.Right:
                point += Vector3Int.right;
                break;
            case PointDir.RDown:
                point += Vector3Int.right + Vector3Int.down;
                break;
            case PointDir.Down:
                point += Vector3Int.down;
                break;
            case PointDir.LDown:
                point += Vector3Int.left + Vector3Int.down;
                break;
            case PointDir.Left:
                point += Vector3Int.left;
                break;
            case PointDir.LUp:
                point += Vector3Int.left + Vector3Int.up;
                break;
        }
        return point;
    }

    public static Vector3Int GetAroundPos(this Vector3Int point, PointDir pointDir)
    {
        return GetAroundPos(point, pointDir, 1);
    }
    
    public static Vector3Int GetAroundPos(this Vector3Int point, PointDir pointDir,int length)
    {
        switch (pointDir)
        {
            case PointDir.Up:
                point += Vector3Int.up * length;
                break;
            case PointDir.RUp:
                point += (Vector3Int.right + Vector3Int.up) * length;
                break;
            case PointDir.Right:
                point += Vector3Int.right * length;
                break;
            case PointDir.RDown:
                point += (Vector3Int.right + Vector3Int.down) * length;
                break;
            case PointDir.Down:
                point += Vector3Int.down * length;
                break;
            case PointDir.LDown:
                point += (Vector3Int.left + Vector3Int.down) * length;
                break;
            case PointDir.Left:
                point += Vector3Int.left * length;
                break;
            case PointDir.LUp:
                point += (Vector3Int.left + Vector3Int.up) * length;
                break;
        }
        return point;
    }
    //将v3转化为pointInt
    public static Vector3Int PointIntPos(this Vector3 vector3)
    {
        int x = (int) vector3.x;
        int y = (int) vector3.y;

        if (vector3.x < x  && vector3.x < 0)
            x -= 1;
        if (vector3.y < y && vector3.y < 0)
            y -= 1;
        return new Vector3Int(x,y,0);
    }
    
    public static Vector3 PointPos(this Vector3 vector3)
    {
        int x = (int) vector3.x;
        int y = (int) vector3.y;
        if (vector3.x < x  && vector3.x < 0)
            x -= 1;
        if (vector3.y < y && vector3.y < 0)
            y -= 1;
        float overFlowX = Mathf.Abs(vector3.x - x);  
        float overFlowY = Mathf.Abs(vector3.y - y);
        overFlowX = halfOffset(overFlowX);
        overFlowY = halfOffset(overFlowY);
        return new Vector3(x + overFlowX,y + overFlowY,0);
    }

    private static float halfOffset(float overFlow)
    {
        if (overFlow > -0.75f && overFlow <= 0.25f)
        {
            return 0;
        }
        else if (overFlow > 0.25f && overFlow <= 0.75f)
        {
            return 0.5f;
        }
        else if (overFlow > 0.75f && overFlow <= 1.25f)
        {
            return 1;
        }

        return 0;
    }
    //将v3转化为pointCenterPos
    public static Vector3 PointCenterPos(this Vector3 vector3)
    {
        return PointIntPos(vector3) + MapManager.mapOffset;
    }
    //将v3转化为pointCenterPos
    public static Vector3 PointCenterPos(this Vector3Int vector3)
    {
        return vector3 + MapManager.mapOffset; 
    }
    
    public static Vector3 V2ToV3(this Vector2 vector2)
    {
        return vector2;
    }

    public static bool DisLongerThan(this Vector3 a,Vector3 b,float dis)
    {
        return ((a - b).sqrMagnitude > dis * dis);
    }

    public static List<Collider2D> FindCircleAllCollider(this Transform transform, float range, LayerMask layer, string tag)
    {
        return transform.position.FindCircleAllCollider(range,layer,tag);
    }
    public static List<Collider2D> FindCircleAllCollider(this Vector3 point,float range,LayerMask layer,string tag)
    {
        List<Collider2D> list = new List<Collider2D>();
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(point, range ,layer);
        collider2Ds.SortByDis(point);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if(!collider2Ds[i].CompareTag(tag))
                continue;
            list.Add(collider2Ds[i]);
        }
        return list;
    }
    
    public static List<Collider2D> FindCircleAllCollider(this Vector3 point,float range,LayerMask layer)
    {
        List<Collider2D> list = new List<Collider2D>();
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(point, range ,layer);
        collider2Ds.SortByDis(point);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            list.Add(collider2Ds[i]);
        }
        return list;
    }

    public static List<T> FindCircleAllCollider<T>(this Transform transform, float range, LayerMask layer, string tag)
    {
        return transform.position.FindCircleAllCollider<T>(range,layer,tag);
    }
    public static List<T> FindCircleAllCollider<T>(this Vector3 point,float range,LayerMask layer,string tag) 
    {
        List<T> list = new List<T>();
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(point, range ,layer);
        collider2Ds.SortByDis(point);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if(!collider2Ds[i].CompareTag(tag))
                continue;
            T t = collider2Ds[i].GetComponentInParent<T>();
            if(t!=null)
                list.Add(t);
        }
        return list;
    }

    public static List<T> FindCircleAllCollider<T>(this Vector3 point,float range,LayerMask layer) 
    {
        List<T> list = new List<T>();
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(point, range ,layer);
        collider2Ds.SortByDis(point);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            T t = collider2Ds[i].GetComponentInParent<T>();
            if(t!=null)
                list.Add(t);
        }
        return list;
    }
    
    public static List<T> FindBoxAllCollider<T>(this Transform transform, Vector2 size, LayerMask layer, string tag)
    {
        return transform.position.FindBoxAllCollider<T>(size,layer,tag);
    }
    public static List<T> FindBoxAllCollider<T>(this Vector3 point,Vector2 size,LayerMask layer,string tag)
    {
        List<T> list = new List<T>();
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(point, size, 0, layer);
        collider2Ds.SortByDis(point);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if(!collider2Ds[i].CompareTag(tag))
                continue;
            T t = collider2Ds[i].GetComponentInParent<T>();
            if(t!=null)
                list.Add(t);
        }
        return list;
    }
    

    /// <summary>
    /// 该扩展方法为静态类型 扩展二维向量的叉乘
    /// </summary>
    /// <param name="vectorA">第一个参数使用this + 扩展类型 + 该类型对象 </param>
    /// <param name="vectorB"></param>
    /// <returns>返回叉乘结果</returns>
    public static float Cross(this Vector2 vectorA, Vector2 vectorB)
    {
        return vectorA.x * vectorB.y - vectorB.x * vectorA.y;
    }
    
    //向量1旋转到向量2所需要旋转的角度
    public static float GetDir1To2Angle(Vector2 dir1,Vector2 dir2)
    {
        float angle = Vector2.Angle(dir1, dir2);
        //2在1的方向 小于0 顺时针  大于0 逆时针 0 直线
        float temp = Cross( dir1, dir2);
        return temp < 0 ? -angle : angle;
    }

    public static Vector3 Rotate_VectorAToB_WithAngle(Vector3 dir1,Vector3 dir2,float rotaAngle)
    {
        //2在1的方向 小于0 顺时针  大于0 逆时针 0 直线
        float temp = Cross( dir1, dir2);
        rotaAngle =temp < 0 ? -rotaAngle : rotaAngle;
        return dir1.Rota2DAxis(rotaAngle);
        // transform.LookAt2D(next.transform.position);
        
    }
    /// <summary>
    /// 围绕某点旋转指定角度
    /// </summary>
    /// <param name="position">自身坐标</param>
    /// <param name="center">旋转中心</param>
    /// <param name="axis">围绕旋转轴</param>
    /// <param name="angle">旋转角度</param>
    /// <returns></returns>
    public static Vector3 RotateRound(this Vector3 position, Vector3 center, Vector3 axis, float angle)
    {
        return Quaternion.AngleAxis(angle, axis) * (position - center) + center;
    }
    
    public static Vector3 Rota2DAxis(this Vector3 axis, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * axis;
        // return Quaternion.AngleAxis(angle, Vector3.forward) * axis;
    }

    public static Vector2 Rota2DAxis(this Vector2 axis, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * axis;
        // return Quaternion.AngleAxis(angle, Vector3.forward) * axis;
    }
    
    /// <summary>
    /// 三角函数
    /// </summary>
    public static float TriangleFunc(this Vector3 target,Vector3 p1, Vector3 p2)
    {
        //bullet 到 p1_p2 直线的最短距离
        //p1->p2的向量
        Vector3 p1_2 = p2 - p1;
        //p1->target向量
        Vector3 p1_target = target - p1;
        //前面两个向量的夹角
        float angle = Vector3.Angle(p1_2, p1_target);
        //获取斜边的长度
        float distance_p1_target = p1_target.magnitude;
        //Sin函数计算距离
        float distance = Mathf.Sin(angle * Mathf.Deg2Rad) * distance_p1_target;
        // Debug.Log("三角函数法：" + distance);
        return distance;
    }

    /// <summary>
    ///  
    /// </summary>
    /// <param name="point">旋转点</param>
    /// <param name="arroundCenter">围绕点</param>
    /// <param name="angle">旋转角度</param>
    /// <returns></returns>
    //点绕终点的连线旋转角度后得到的点
    public static Vector3 Rota2DPoint(this Vector3 point,Vector3 arroundCenter, float angle)
    {
        Vector3 newPoint =Quaternion.AngleAxis(angle, Vector3.forward) * (point - arroundCenter);
        return arroundCenter + newPoint;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start">起点</param>
    /// <param name="end">终点</param>
    /// <param name="precent">交线在哪个位置发出</param>
    /// <param name="angle">交线倾斜角度</param>
    /// <param name="length">交线长度</param>
    /// <returns></returns>
    public static Vector3 GetVerticalWithLine(Vector3 start,Vector3 end,float precent,float angle,float length)
    {
        //方向
        Vector3 normal = (end - start).normalized;
        //长度
        float distance = Vector3.Distance(start, end);
        //原有方向上的比例点
        Vector3 precentPoint = normal * (distance * precent) + start;
        Vector3 rotaPoint = precentPoint + (normal.Rota2DAxis(angle) * length);
        return rotaPoint;
    }
    
    public static Vector3 GetVerticalWithX(this Vector3 start,Vector3 end,float precent,float length)
    {
        float xlength = end.x - start.x;
        float x = start.x + (xlength * precent);
        float y = length + start.y;
        return new Vector3(x,y);
    }
    //两点连线上的点
    public static Vector3 GetBetweenPoint(this Vector3 start,Vector3 end,float precent = 0.5f)
    {
        Vector3 normal = (end - start).normalized;
        float distance = Vector3.Distance(start, end);
        return (normal * (distance * precent)) + start;
    }

    //某个方向上的距离
    public static Vector3 GetDirDistance(this Vector3 start,Vector3 target,float distance)
    {
        Vector3 normal = (target - start).normalized;
        return normal * distance + start;
    }
    
    // public static Vector3 GetDirDistance(this Vector3 start,Vector3 dir,float distance)
    // {
    //     Vector3 normal = (bullet - start).normalized;
    //     return dir * distance + start;
    // }

    
    public static FaceDir WatchToTargetFourDir(this Vector3 self,Vector3 target)
    {
        float angle = GetAngle.Angle(target, self);

        FaceDir faceDir = FaceDir.Right;

        //Up
        if(angle is > 45 and <= 135)
        {
            faceDir = FaceDir.Up;
        }
        //left
        else if(angle is > 135 and <= 225)
        {
            faceDir = FaceDir.Left;
        }
        //down
        else if(angle is > 225 and <= 315)
        {
            faceDir = FaceDir.Down;
        }
        // Debug.Log(angle);
        return faceDir;
    }

    
    public static FaceDir WatchToTargetTwoDir(float angle)
    {
        FaceDir faceDir = FaceDir.Left;

        //right
        if(angle is > 270 and <= 360 or > 0 and <= 90)
        {
            faceDir = FaceDir.Right;
        }
        return faceDir;
    }
    
    public static FaceDir BackDir(this FaceDir faceDir)
    {
        switch (faceDir)
        {
            case FaceDir.Down:
                faceDir = FaceDir.Up;
                break;
            case FaceDir.Up:
                faceDir = FaceDir.Down;
                break;
            case FaceDir.Right:
                faceDir = FaceDir.Left;
                break;
            case FaceDir.Left:
                faceDir = FaceDir.Right;
                break;
        }
        return faceDir;
    }
    public static FaceDir BackDir(this Vector3 self,Vector3 target)
    {
        FaceDir dir = self.WatchToTargetFourDir(target);
        return dir.BackDir();
    }
    
    public static Vector3 GetFontPos(this Vector3 self,Vector3 target)
    {
        Vector3 position = self;
        FaceDir fontDir = self.WatchToTargetFourDir(target);
        switch (fontDir)
        {
            case FaceDir.Down:
                position += Vector3.down;
                break;
            case FaceDir.Up:
                position += Vector3.up;
                break;
            case FaceDir.Right:
                position += Vector3.right;
                break;
            case FaceDir.Left:
                position += Vector3.left;
                break;
        }
        return position;
    }
    public static Vector3 GetBackPos(this Vector3 self,Vector3 target)
    {
        Vector3 position = self;
        FaceDir fontDir = self.WatchToTargetFourDir(target);
        switch (fontDir)
        {
            case FaceDir.Down:
                position += Vector3.up;
                break;
            case FaceDir.Up:
                position += Vector3.down;
                break;
            case FaceDir.Right:
                position += Vector3.left;
                break;
            case FaceDir.Left:
                position += Vector3.right;
                break;
        }
        return position;
    }

    public static Vector2 GetSize(this Vector2 form, Vector2 to)
    {
        var size = new Vector2
        {
            x = Mathf.Abs(form.x - to.x),
            y = Mathf.Abs(form.y - to.y)
        };
        return size;
    }
}