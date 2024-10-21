using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GetMousePos
{
    public static Vector2 GetMousePosition()                                               //通过mainCamera获取
    {
        return  GetMousePositionWithZ();                                          //通过mainCamera获取
    }
    public static Vector3 GetMousePositionWithZ()                                               //通过mainCamera获取
    {
        // return GetMousePositionWithZ(Input.mousePosition, Camera.main);
        return GetMousePositionWithZ(Mouse.current.position.ReadValue(), Camera.main);
    }

    public static Vector3 GetMousePositionWithZ(Camera worldCamera)                 //通过指定Camera获取
    {
        return GetMousePositionWithZ(Mouse.current.position.ReadValue(), worldCamera);
    }

    public static Vector3 GetMousePositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
public static class GetAngle                                       //仅获取2d xy的夹角
{
    public static float Angle(Vector3 target, Vector3 start)
    {
        Vector3 aimDirection =(target - start).normalized;
        //向量与x轴之间的角度
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        if(angle < 0)
        {
            angle += 360;
        }
        return angle;
    }
    
    public static float Angle(Vector3 dir)
    {
        //向量与x轴之间的角度
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(angle < 0)
        {
            angle += 360;
        }
        return angle;
    }
    
    public static Vector3 GetAngleFormVectorFloat(float angle)
    {
        //angle = 0 - 360
        float angleRad = angle * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static void RotaWithZ(Transform target, Vector3 start,GameObject obj)
    {
        RotaWithZ(target.position, start, obj);
    }
    
    public static void RotaWithZ(Vector3 target, Vector3 start,GameObject obj)
    {
        //Vector2 relative = PointOne.transform.InverseTransformPoint(PointTwo).normalized;
        float angle = Angle(target, start);
        obj.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    public static void LookToTarget(this Transform transform,Vector3 target)
    {
        //Vector2 relative = PointOne.transform.InverseTransformPoint(PointTwo).normalized;
        float angle = Angle(target, transform.position);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

public static class Lookdir 
{
    public enum lookdir { down, right, up, left };
    public static lookdir GetLookDir(float angle)
    {

        float rota = angle - 90;
        if (-135 > rota && rota < 135)                 //下
        {
            return lookdir.down;
        }
        else if (-135 < rota && rota < -45)     //右
        {
            return lookdir.right;
        }
        else if (-45 < rota && rota < 45)    //上
        {
            return lookdir.up;
        }
        else                                                                    //左
        {
            return lookdir.left;
        }
    }
    
}

public class TimeToText
{
    //仅限分秒
    public static string time(int second)
    {
        int minute = 0;
        string minStr = "";
        string secStr = "";

        if (second > 60)
        {
            minute = second / 60;                   //119 /60 =1  119%60 =59 
            second %= 60;
        }
        if (minute < 10)
            minStr += "0";
        if (second < 10)
            secStr += "0";

        minStr += minute.ToString();
        secStr += second.ToString();

        return minStr + ":" + secStr;
    }
}

public class FindFiles 
{
    public static void FindObjPath(ref List<string> dirs, string targetPath = "")
    {
        DirectoryInfo root = new DirectoryInfo("Assets/Resources/" + targetPath);
        for (int i = 0; i < root.GetFiles().Length; i++)
        {
            if (Path.GetExtension(root.GetFiles()[i].Name) == ".prefab")
            {
                dirs.Add(targetPath +"/"+ Path.GetFileNameWithoutExtension(root.GetFiles()[i].Name));
                //Debug.Log(targetPath + Path.GetFileNameWithoutExtension(root.GetFiles()[i].Name));
            }
        }
    }
    #region GetDirs
    // 在菜单来创建 选项 ， 点击该选项执行搜索代码
    //[MenuItem("Tools/遍历项目所有文件夹")]
    //static void CheckSceneSetting()
    //{
    //    List<string> dirs = new List<string>();
    //    GetDirs(Application.dataPath, ref dirs);
    //}
    //参数1 为要查找的总路径， 参数2 保存路径
    /*
    public static void GetDirs(string dirPath, ref List<string> dirs, string targetPath = "")
    {

        foreach (string path in Directory.GetFiles(dirPath))
        {
            //    //获取所有文件夹中包含后缀为 .prefab 文件的路径
            //    //如果后缀为获取后缀为prefab的文件路径
            //Path.GetFileNameWithoutExtension(path)获取不带后缀的文件名
            if (path.Contains(targetPath) && Path.GetExtension(path) == ".prefab")
            {
                //添加相对路径 从Assests开始添加  
                //targetPath
                int startIndex = path.IndexOf(targetPath);
                int endIndex = path.IndexOf(".");
                dirs.Add(path.Substring(startIndex, endIndex - startIndex));
                Debug.Log(Path.GetFileNameWithoutExtension(path));
                Debug.Log(path.Substring(startIndex, endIndex-startIndex));

            }       

        }
        //遍历所有文件夹
        if (Directory.GetDirectories(dirPath).Length > 0)  
        {
            foreach (string path in Directory.GetDirectories(dirPath))
            {
                GetDirs(path, ref dirs, targetPath);
            }
        }
    }
    */
    #endregion
    
    

}
public class Swap
{
    public static void ValueType<T>(ref T a,ref T b)
    {
        (a, b) = (b, a);
    }

    public static void ReferenceType<T>(T a,T b)
    {
        ValueType(ref a, ref b);
    }
}

public class GetDir
{
     int dir;               //因为要保留上次的dir
    public  int Dir(float dir)
    {
        if (dir > 0)
        {
            this.dir = 1;
        }
        else if(dir<0)
        {
            this.dir = -1;
        }
        return this.dir;
    }
}

public class ColorTool
{
    static List<float> arr = new List<float>();
    public static Color RandomColor(float max = 1,float min = 0.7f,float alpha=0.7f)
    {
        arr = new List<float>();
        float random = Random.Range(min, max);
        Color randomColor;
        arr.Add(max);
        arr.Add(min);
        arr.Add(random);
        randomColor = Color.white;
        randomColor.a = alpha;
        for (int i = arr.Count - 1; i >= 0; i--)
        {
            int num = Random.Range(0, arr.Count);
            if (i == 0)
                randomColor.r = arr[num];
            else if (i == 1)
                randomColor.g = arr[num];
            else if (i == 2)
                randomColor.b = arr[num];
            arr.RemoveAt(num);
        }
        return randomColor;
    }

    public static Color ChangeAlpha(Color color, float alpha)
    {
        color = new Color(color.r,color.g,color.b,alpha);
        return color;
    }
   
    public static Color Trans16StrToColor(string colorStr)
    {
        
        ColorUtility.TryParseHtmlString(colorStr, out Color color);
        return color;
    }
}

public class CompareTool
{
    public static bool DisLongerThan(Transform a,Transform b,float dis)
    {
        return DisLongerThan(a.position, b.position, dis);
    }
    
    public static bool DisLongerThan(Vector3 a,Vector3 b,float dis)
    {
        return ((a - b).sqrMagnitude > dis * dis);
    }
}



public class Shuffle
{
    public static T[] ShuffleItem <T>(T[] dataArray)
    {
        for(int i = 0; i < dataArray.Length; i++)
        {
            int randomNum = Random.Range(i, dataArray.Length);
            //Swap ab互换
            (dataArray[randomNum], dataArray[i]) = (dataArray[i], dataArray[randomNum]);
        }

        return dataArray;
    }

    public static List<T> ShuffleItem<T>(List<T> dataList)
    {
        T[] tArr = dataList.ToArray();
        return ShuffleItem<T>(tArr).ToList();
    }
    
    public static Queue<T> ShuffleItem<T>(Queue<T> dataQueue)
    {
        T[] tArr = dataQueue.ToArray();
        return new Queue<T>(ShuffleItem<T>(tArr));
    }
}




