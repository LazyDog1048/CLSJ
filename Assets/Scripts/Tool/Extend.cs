using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using tool;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;
using Random = UnityEngine.Random;


public static class Extend
{
    public static IEnumerable<Type> GetFilteredTypeList<T>()
    {
        var q = typeof(T).Assembly.GetTypes()
            .Where(x => !x.IsAbstract)                                          // Excludes BaseClass
            .Where(x => !x.IsGenericTypeDefinition)                             // Excludes C1<>
            .Where(x => typeof(T).IsAssignableFrom(x));                 // Excludes classes not inheriting from BaseClass
        // .Where(x => typeof(BaseBuff).IsAssignableFrom(x));                 // Excludes classes not inheriting from BaseClass
        // Adds various C1<T> type variants.
        // q = q.AppendWith(typeof(PlugBuff<>).MakeGenericType(typeof(Damager)));
        // q = q.AppendWith(typeof(C1<>).MakeGenericType(typeof(AnimationCurve)));
        // q = q.AppendWith(typeof(C1<>).MakeGenericType(typeof(List<float>)));
        return q;
    }
    
    public static float DisForm(this GameObject trans,Transform target)
    {
        return Transform_Extend.DisForm(trans.transform, target.position);
    }
    
    public static float DisForm(this GameObject trans,Vector3 target)
    {
        return Transform_Extend.DisForm(trans.transform, target);
    }
    
    public static float DisForm(this Transform trans,Vector3 target)
    {
        return Transform_Extend.DisForm(trans, target);
    }
   
    public static float DisForm(this Vector3 point,Vector3 target)
    {
        return Vector3.Distance(point, target);
    }

    public static void SortByDis<T>(this List<T> objList,Vector3 ori) where T : MonoBehaviour
    {
        objList.Sort((x,y)=> x.gameObject.DisForm(ori).CompareTo(y.gameObject.DisForm(ori)));
    }

    // public static void SortByDis(this Collider2D[] objList,Vector3 ori)
    // {
    //     Array.Sort(objList,(x,y)=>x.gameObject.DisForm(ori).CompareTo(y.gameObject.DisForm(ori)));
    // }
    
    public static void SortByDis(this Collider2D[] objList,Vector3 ori)
    {
        Array.Sort(objList,(x,y)=>x.gameObject.DisForm(ori).CompareTo(y.gameObject.DisForm(ori)));
    }
    
    public static void SortByDis(this Collider2D[] objList,Transform ori)
    {
        objList.SortByDis(ori.position);
    }
    
    public static void SortByDis(this List<Collider2D> objList,Vector3 ori)
    {
        objList.Sort((x,y)=> x.gameObject.DisForm(ori).CompareTo(y.gameObject.DisForm(ori)));
    }
    public static void SortByDis(this List<Collider2D> objList,Transform ori)
    {
        objList.SortByDis(ori.position);
    }

    public static void CheckAdd<T, T1>(this Dictionary<T, T1> Dic,T key,T1 val)
    {
        if(!Dic.ContainsKey(key))
            Dic.Add(key,val);
    }
    
    public static void CheckReplaceAdd<T, T1>(this Dictionary<T, T1> Dic,T key,T1 val)
    {
        if(!Dic.ContainsKey(key))
            Dic.Add(key,val);
        else
            Dic[key] = val;
    }
    
    public static void CheckAdd<T>(this List<T> list,T key)
    {
        if(!list.Contains(key))
            list.Add(key);
    }
    
    public static int Int(this float num)
    {
        return (int) num;
    }
    //百分数
    public static float percent(this int rate)
    {
        return rate/100f;
    }
    
    //百分数
    public static float percent(this float rate)
    {
        return rate/100f;
    }

    public static bool IsInLayerMask(this LayerMask layerMask,int layer)
    {
        // 根据Layer数值进行移位获得用于j运算的Mask值
        int objLayerMask = 1 << layer;
        return (layerMask.value & objLayerMask) > 0;
    }
    
    public static string LogStr(this string str)
    {
        Debug.Log($"Howji  {str}" );
        return str;
    }
    
    public static bool RandomBy100Percent(this int rate)
    {
        //概率大于随机得到的值 则判断成功
        int random = Random.Range(1,100);
        return rate >= random;
    }
    public static int AfterPercent(this int num,int rate)
    {
        return (int)(num * rate.percent());
    }
    
    public static float RandomFloat(this float num,float min,float max)
    {
        return Random.Range(min* num,max * num);
    }
    
    public static T RandomItem<T>(this List<T> list) 
    {
        if (list == null ||list.Count < 1) 
            return default;
        //概率大于随机得到的值 则判断成功
        int random = Random.Range(0,list.Count);
        return list[random];
    }


    public static int RangeOffSet(this int num, float upperRate, float lessRate)
    {
        int max = (int)(num * (1 + upperRate));
        int min = (int)(num * (1 - lessRate));
        return Random.Range(min,max);
    }
    
    public static int LengthOfText(this Text text)
    {
        int totalLength = 0;
        Font myFont = text.font;  //chatText is my Text component
        myFont.RequestCharactersInTexture(text.text, text.fontSize, text.fontStyle);
        // Debug.Log(text.text);
        CharacterInfo characterInfo = new CharacterInfo(); 
        char[] arr = text.text.ToCharArray(); 
        foreach (char c in arr)
        {
            myFont.GetCharacterInfo(c, out characterInfo, text.fontSize); 
            totalLength += characterInfo.advance;
            // Debug.Log($"{c}  {characterInfo.advance})");
        } 
        // Debug.Log($" {arr.Length}  {totalLength}");
        return totalLength;
    }

    public static void EditorDebugLog(string content)
    {
#if Unity_Editor
                    Debug.Log(content);
#endif
    }
    
    public static void EditorDebugLogError(string content)
    {
#if Unity_Editor
            Debug.LogError(content);
#endif
    }
}




public static class DoCopy
{
    #region Copy
    public static object CopyTest(this object oldObj)
    {
        object newObj;
        var type = oldObj.GetType();
        //值类型
        if (type.IsValueType)
        {
            newObj = oldObj;
            return newObj;
        }
        //引用类型
        newObj = Activator.CreateInstance(type);
        Debug.Log($"Cpoy  {type.Name}      {type.GetMembers().Length}");
        if (oldObj is IEnumerable)
        {
            Debug.Log($"IEnumerable   {oldObj.GetType()}");
            newObj = (oldObj as IEnumerable).GetEnumerator().Current;
        }
        foreach (MemberInfo member in type.GetMembers())
        {
            if (member.MemberType == MemberTypes.Field)
            {
                FieldInfo field = (FieldInfo)member;
                Object fieldValue = field.GetValue(oldObj);
                    Debug.Log($"FieldInfo  {field.Name}  {fieldValue}");
                    if (fieldValue is ICloneable)
                {
                    field.SetValue(newObj, (fieldValue as ICloneable).Clone());
                }
                else
                {
                    field.SetValue(newObj, CopyTest(fieldValue));
                }
            }
            
            else if (member.MemberType == MemberTypes.Property)
            {
                PropertyInfo prop = (PropertyInfo)member;
                
                MethodInfo info = prop.GetSetMethod(false);
                if (info != null)
                {
                    Debug.Log($"PropertyInfo {prop.Name}  {prop.PropertyType.Name}");
                    object propertyValue;
                    if (prop.GetIndexParameters().Length > 0)
                    {
                        propertyValue = prop.GetValue(oldObj,new object[]{0});
                    }
                    else
                    {
                        propertyValue = prop.GetValue(oldObj);
                    }
                    
                    if (propertyValue is ICloneable)
                    {
                        Debug.Log("ICloneable");
                        prop.SetValue(newObj, (propertyValue as ICloneable).Clone());
                    }
                    else
                    {
                        prop.SetValue(newObj, CopyTest(propertyValue));
                    }
                }
            }
        }
        return newObj;
    }
    
    public static object CloneObject(object objSource)
    {
        //step : 1 Get the type of source object and create a new instance of that type
        Type typeSource = objSource.GetType();
        object objTarget = Activator.CreateInstance(typeSource);
        //Step2 : Get all the properties of source object type
        PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        //Step : 3 Assign all source property to taget object 's properties
        foreach (PropertyInfo property in propertyInfo)
        {
            //Check whether property can be written to
            if (property.CanWrite)
            {
                //Step : 4 check whether property type is value type, enum or string type
                if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String)))
                {
                    property.SetValue(objTarget, property.GetValue(objSource, null), null);
                }
                //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                else
                {
                    object objPropertyValue = property.GetValue(objSource, null);
                    if (objPropertyValue == null)
                    {
                        property.SetValue(objTarget, null, null);
                    }
                    else
                    {
                        property.SetValue(objTarget, CloneObject(objPropertyValue), null);
                    }
                }
            }
        }
        return objTarget;
    }
    
    //深复制
    public static T DeepCopy<T>(this T obj)
    {
        using (var ms = new MemoryStream())
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(ms);
        }
    }
    //浅复制
    public static T ShallowCopy<T>(this T oldObj)
    {
        var type = typeof(T);
        T newObj = (T)Activator.CreateInstance(type);

        foreach (MemberInfo member in type.GetMembers())
        {
            if (member.MemberType == MemberTypes.Field)
            {
                FieldInfo field = (FieldInfo)member;
                field.SetValue(newObj, field.GetValue(oldObj));
            }
            else if (member.MemberType == MemberTypes.Property)
            {
                PropertyInfo prop = (PropertyInfo)member;
                MethodInfo info = prop.GetSetMethod(false);
                if (info != null)
                {
                    prop.SetValue(newObj, prop.GetValue(oldObj));
                }
            }
        }
        return newObj;
    }
    
    //浅复制
    public static List<FieldInfo> GetFieldContent<T>(this T oldObj)
    {
        var type = typeof(T);

        List<FieldInfo> fieldInfos = new List<FieldInfo>();
        
        foreach (MemberInfo member in type.GetMembers())
        {
            if (member.MemberType == MemberTypes.Field)
            {
                FieldInfo field = (FieldInfo)member;
                fieldInfos.Add(field);
            }
        }

        return fieldInfos;
    }
    #endregion
}

public static class Log
{
    public static object ObjLog(this object obj,string log)
    {
        Debug.Log(log +"    "+obj.ToString());
        return obj;
    }
    
    public static object ObjLogError(this object obj,string log)
    {
        Debug.LogError(log +"    "+obj.ToString());
        return obj;
    }

}
