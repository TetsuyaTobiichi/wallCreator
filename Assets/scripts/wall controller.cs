using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject wallPrefab; // Префаб стены
    private wall currentWall; // Текущая стена, которую создаем

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) // ЛКМ для начала/завершения стены
        {
            Vector3 clickPosition = GetMouseWorldPosition();

            if (currentWall == null)
            {
                // Создаем новую стену
                GameObject wallObject = Instantiate(wallPrefab, clickPosition, Quaternion.identity);
                currentWall = wallObject.GetComponent<wall>();
                currentWall.Initialize(clickPosition, clickPosition); // Начальные точки стены(место "клика")
            }
            else
            {
                // Завершаем стену
                currentWall.Initialize(currentWall.startPoint, clickPosition);//начальная точка из класса стены и конечное место клика
                currentWall = null;
            }
        }

        if (currentWall != null) // Если есть активная стена, обновляем конечную точку
        {
            Vector3 endPosition = GetMouseWorldPosition();
            currentWall.Initialize(currentWall.startPoint, endPosition);//аналогичное обновление как при завершении, для видимости, куда пойдет стена
        }
    }
    //придумать как проверять коллизию стен в контроллере, добавить проверку на расстояние, чтобы привязка была только при колизии с концами стен
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("enter");
        wall collisionWall = col.gameObject.GetComponent<wall>();
     
        Vector3 endPoint =collisionWall.endPoint;
        Vector3 startPoint =collisionWall.startPoint;

        if(Vector3.Distance(currentWall.startPoint, endPoint)<Vector3.Distance(currentWall.startPoint, startPoint)){

            currentWall.Initialize(currentWall.startPoint, endPoint);
        }
        else{
            currentWall.Initialize(currentWall.startPoint, startPoint);
        }
    }

    
    private Vector3 GetMouseWorldPosition()
    {
        //тут чутка сложнее, создаем плоскость через нормаль y и и точку xyz (0,0,0) создаем плоскость y 
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        // создаем луч из камеры который направлен в точку позиции мыши
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //проверяем пересекается ли луч с плоскостью и если да то в каком месте
        if (groundPlane.Raycast(ray, out float distance))
        {//берем точку пересечения луча и плоскости и отправляем их как начальные координаты стены
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }
}
