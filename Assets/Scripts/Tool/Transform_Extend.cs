using game;
using other;
using plug;
using UnityEngine;
using UnityEngine.Events;

namespace tool
{
    public static class Transform_Extend
    {
        public static void ForeachChild(this Transform transform, UnityAction<Transform> action)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                action(transform.GetChild(i));
            }
        }
        
        public static void ForeachChild(this Transform transform, UnityAction<int,Transform> doThing)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                doThing?.Invoke(i,transform.GetChild(i).GetComponent<RectTransform>());
            }
        }
        
        public static void LookAt2D(this Transform trans,Vector2 target) 
        {
            trans.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
        }
        
        public static void GetOutlineOutDirection(this Vector3 shotPos,Vector3 dirention,out Vector3 hitPos,out Vector3 outDirection) 
        {
            hitPos = shotPos;
            outDirection = -dirention;
            var hits = Physics2D.RaycastAll(shotPos,dirention,1000,LayerMask.GetMask("Outline"));
            if(hits.Length<=0)
                return;
            
            hitPos = hits[0].point;
            outDirection = Vector3.Reflect(dirention, hits[0].normal);
            hitPos+=outDirection*0.1f;
        }

        public static Vector3Int PointIntPos(this Transform trans)
        {
            return trans.position.PointIntPos();
        }
        
        public static Vector3 PointCenterPos(this Transform trans)
        {
            return trans.position.PointCenterPos();
        }

        public static bool DisLongerThan(this Transform a,Transform b,float dis)
        {
            return a.position.DisLongerThan(b.position, dis);
        }
        
        public static bool DisLongerThan(this Transform a,Vector3 b,float dis)
        {
            return a.position.DisLongerThan(b, dis);
        }
        
        public static float DisForm(this Transform trans,Transform target)
        {
            return trans.DisForm(target.position);
        }
        
        public static float DisForm(this Transform trans,Vector3 target)
        {
            return Vector3.Distance(trans.position, target);
        }
        
        public static float V2DisForm(this Transform trans,Vector2 target)
        {
            return Vector2.Distance(trans.position, target);
        }

        public static Vector2 Dir(this Transform trans,Vector3 target)
        {
            return (target - trans.position).normalized;
        }
        // public static void SetByCenterPoint(this Transform trans,int size)
        // {
        //     //获取中心作为中心点
        //     if(size %2==0)
        //         trans.position = trans.Point().pos;
        //     else
        //     {
        //         trans.position = trans.Point().centerPos;
        //     }
        // }
        
        public static void SetByCenterPoint(this Transform trans,Vector2Int size)
        {
            //获取中心作为中心点
            // Point point = trans.Point();
            // if(point == null)
            //     return;
            // Vector3 pos = point.pos;
            Vector3 pos = trans.position.PointCenterPos();
            if (size.x % 2 != 0)
                pos.x += 0.5f;
            if (size.y % 2 == 0)
                pos.y -= 0.5f;
            trans.position = pos;
        }
        
        public static void SetPosBySize(this Transform trans,Vector2Int size)
        {
            Vector3 pos = trans.PointIntPos();
            if (size.x % 2 != 0)
                pos.x += 0.5f;
            if (size.y % 2 != 0)
                pos.y += 0.5f;
            trans.position = pos;
        }
        public static FaceDir WatchToTarget(this Transform transform,Transform target)
        {
            return transform.WatchToTarget(target.position);
        }
        public static FaceDir WatchToTarget(this Transform transform,Vector3 target)
        {
            return transform.position.WatchToTarget(target);
        }

        #region transMove
        
        public static Vector3 MoveWithDir(this Vector3 pos, Vector3 dir, float speed)
        {
            return pos + dir * speed * Time.deltaTime;
        }
        
        public static void MoveWithDir(this Transform transform, Vector3 dir, float speed)
        {
            transform.position = transform.position.MoveWithDir(dir, speed);
        }

        private static void CurveMove(this MonoBehaviour mono,float speed,Vector3[] points,int index = 0,UnityAction complete = null)
        {
            mono.CurveMove(mono.transform,speed,points,index,complete); 
        }
        private static void CurveMove(this MonoBehaviour mono,Transform trans,float speed,Vector3[] points,int index = 0,UnityAction complete = null)
        {
            Vector3 end = points[index+1];
            mono.DoSpeedMove(trans,speed,end, () =>
            {
                if (index < points.Length - 2)
                {
                    CurveMove(mono,trans,speed,points,index+1,complete);
                }
                else
                {
                    complete?.Invoke();
                }
            });
        }

        public static void DoSpeedMove(this MonoBehaviour mono,float moveSpeed,Vector3 targetPos,UnityAction complete = null)
        {
            mono.DoSpeedMove(mono.transform,moveSpeed,targetPos,complete);
        }
        
        public static void DoSpeedMove(this MonoBehaviour mono,Transform trans,float moveSpeed,Vector3 targetPos,UnityAction complete = null)
        {
            mono.WaitExecute(Condition,Complete,Move);
            bool Condition()
            {
                return !DisLongerThan(trans, targetPos, 0.01f) || !trans.gameObject.activeSelf;
            }
            void Move()
            {
                trans.position = Vector2.MoveTowards(trans.position, 
                    targetPos, moveSpeed* Time.deltaTime);
            }

            void Complete()
            {
                complete?.Invoke();
            }
        }
        #endregion
        
        public static TextMesh CreateWorldText(string text,Transform parent = null,Vector3 localPosition = default,int fontSize = 40,Color color = default,TextAnchor textAnchor = TextAnchor.MiddleCenter,TextAlignment textAlignment = TextAlignment.Center, int sortingOrder = 5000)
        {
            return CreateWorldText(parent,text,localPosition,color,fontSize,textAnchor,textAlignment,sortingOrder);
        }
        
        public static TextMesh CreateWorldText(Transform parent,string text,Vector3 localPosition,Color color,int fontSize,TextAnchor textAnchor,TextAlignment textAlignment, int sortingOrder = 5000)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.color = color;
            textMesh.fontSize = fontSize;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }
        
        // Create a Text Popup in the World, no parent
        // public static void CreateWorldTextPopup(string text, Vector3 localPosition) {
        //     CreateWorldTextPopup(null, text, localPosition, 20, Color.white, localPosition + new Vector3(0, 10), 1f);
        // }
        //
        // public const int sortingOrderDefault = 5000;
        // // Create a Text Popup in the World
        // public static void CreateWorldTextPopup(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, Vector3 finalPopupPosition, float popupTime) {
        //     TextMesh textMesh = CreateWorldText(parent, text, localPosition, fontSize, color, TextAnchor.LowerLeft, TextAlignment.Left, sortingOrderDefault);
        //     Transform transform = textMesh.transform;
        //     Vector3 moveAmount = (finalPopupPosition - localPosition) / popupTime;
        //     FunctionUpdater.Create(delegate () {
        //         transform.position += moveAmount * Time.deltaTime;
        //         popupTime -= Time.deltaTime;
        //         if (popupTime <= 0f) {
        //             UnityEngine.Object.Destroy(transform.gameObject);
        //             return true;
        //         } else {
        //             return false;
        //         }
        //     }, "WorldTextPopup");
        // }
  
        public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
        {
            vertices = new Vector3[quadCount * 4];
            uvs = new Vector2[quadCount * 4];
            triangles = new int[quadCount * 6];
        }

        public static void AddToMeshArrays(Vector3[] vertices,Vector2[] uvs,int[] triangles,int index,Vector3 pos,float rot,Vector3 baseSize,Vector2 uv00,Vector2 uv11)
        {
            int vIndex = index * 4;
            int vIndex_0 = vIndex;
            int vIndex_1 = vIndex + 1;
            int vIndex_2 = vIndex + 2;
            int vIndex_3 = vIndex + 3;
            
            baseSize*=0.5f;

            bool skewed = baseSize.x != baseSize.y;

            if (skewed)
            {
                vertices[vIndex_0] = pos + Quaternion.Euler(0, 0, rot) * new Vector3(-baseSize.x, baseSize.y);
                vertices[vIndex_1] = pos + Quaternion.Euler(0, 0, rot) * new Vector3(-baseSize.x, -baseSize.y);
                vertices[vIndex_2] = pos + Quaternion.Euler(0, 0, rot) * new Vector3(baseSize.x, -baseSize.y);
                vertices[vIndex_3] = pos + Quaternion.Euler(0, 0, rot) * new Vector3(baseSize.x, baseSize.y);
            }
            else
            {
                vertices[vIndex_0] = pos + Quaternion.Euler(0, 0, rot - 270) * baseSize;
                vertices[vIndex_1] = pos + Quaternion.Euler(0, 0, rot - 180) * baseSize;
                vertices[vIndex_2] = pos + Quaternion.Euler(0, 0, rot - 90) * baseSize;
                vertices[vIndex_3] = pos + Quaternion.Euler(0, 0, rot) * baseSize;
            }
            //relocate uvs
            uvs[vIndex_0] = new Vector2(uv00.x, uv11.y);
            uvs[vIndex_1] = new Vector2(uv00.x, uv00.y);
            uvs[vIndex_2] = new Vector2(uv11.x, uv00.y);
            uvs[vIndex_3] = new Vector2(uv11.x, uv11.y);

            int tIndex = index * 6;
            triangles[tIndex + 0] = vIndex_0;
            triangles[tIndex + 1] = vIndex_1;
            triangles[tIndex + 2] = vIndex_2;
                    
            triangles[tIndex + 3] = vIndex_0;
            triangles[tIndex + 4] = vIndex_2;
            triangles[tIndex + 5] = vIndex_3;
        }
        
        public static Mesh CreateMesh(int width = 4, int height = 4,float tileSize = 10)
        {
            Mesh mesh = new Mesh();

            //顶点
            Vector3 [] vertices = new Vector3[4 * (width * height)];
            //xy坐标
            Vector2[] uv = new Vector2[4 * (width * height)];
            //三角形
            int[] triangles = new int[6 * (width * height)];


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int index = i * height + j;

                    vertices[index * 4 + 0] = new Vector3(tileSize * i, tileSize * j);
                    vertices[index * 4 + 1] = new Vector3(tileSize * i, tileSize * (j +1));
                    vertices[index * 4 + 2] = new Vector3(tileSize * (i +1), tileSize * (j +1));
                    vertices[index * 4 + 3] = new Vector3(tileSize * (i +1), tileSize * j);
                    
                    uv[index * 4 + 0] = new Vector2(0, 0);
                    uv[index * 4 + 1] = new Vector2(0, 1);
                    uv[index * 4 + 2] = new Vector2(1, 1);
                    uv[index * 4 + 3] = new Vector2(1, 0);
                    
                    triangles[index * 6 + 0] = index  * 4 + 0;
                    triangles[index * 6 + 1] = index  * 4 + 1;
                    triangles[index * 6 + 2] = index  * 4 + 2;
                    
                    triangles[index * 6 + 3] = index  * 4 + 0;
                    triangles[index * 6 + 4] = index  * 4 + 2;
                    triangles[index * 6 + 5] = index  * 4 + 3;
                }
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            return mesh;
        }
    }    
}
