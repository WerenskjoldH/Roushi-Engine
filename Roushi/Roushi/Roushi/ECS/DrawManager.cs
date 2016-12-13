using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{
        // 0 - Background
        // 1 - Stage
        // 2 - Foreground
        // 3 - Overlay

    public class DrawManager
    {

        ArrayList[] SortedEntityList;
        public DrawManager()
        {
            SortedEntityList = new ArrayList[4];
            SortedEntityList[0] = new ArrayList();
            SortedEntityList[1] = new ArrayList();
            SortedEntityList[2] = new ArrayList();
            SortedEntityList[3] = new ArrayList();
        }

        public void AddEntity(Entity e)
        {
            switch (e.Get<DrawablePart>().DepthLayer)
            {
                case DepthLayer.Stage:
                    SortedEntityList[1].Add(e);
                    break;
                case DepthLayer.Background:
                    SortedEntityList[0].Add(e);
                    break;
                case DepthLayer.Foreground:
                    SortedEntityList[2].Add(e);
                    break;
                case DepthLayer.Overlay:
                    SortedEntityList[3].Add(e);
                    break;   
            }
        }

        public void RemoveEntity(Entity e)
        {
            switch (e.Get<DrawablePart>().DepthLayer)
            {
                case DepthLayer.Stage:
                    SortedEntityList[1].Remove(e);
                    break;
                case DepthLayer.Background:
                    SortedEntityList[0].Remove(e);
                    break;
                case DepthLayer.Foreground:
                    SortedEntityList[2].Remove(e);
                    break;
                case DepthLayer.Overlay:
                    SortedEntityList[3].Remove(e);
                    break;   
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ArrayList a in SortedEntityList)
            {
                ArrayList temp = new ArrayList();
                QuickSort(a, 0, a.Count-1);
                foreach (Entity e in a)
                {
                    e.Draw(spriteBatch);
                }
                temp.Clear();
            }
        }

        private int Partition(ArrayList a, int left, int right)
        {

            int i = left, j = right;
            Entity temp;
            Entity e = (Entity)a[(left + right) / 2];
            float pivot = e.Get<TransformPart>().GetPositionY;

            while (i <= j)
            {
                Entity leftEntity = (Entity)a[i];
                Entity rightEntity = (Entity)a[j];
                while (leftEntity.Get<TransformPart>().GetPositionY < pivot)
                {
                    i++;
                    leftEntity = (Entity)a[i];
                }
                while (rightEntity.Get<TransformPart>().GetPositionY > pivot)
                {
                    j--;
                    rightEntity = (Entity)a[j];
                }
                if (i <= j)
                {
                    temp = (Entity)a[i];
                    a[i] = a[j];
                    a[j] = temp;
                    i++;
                    j--;
                }
            }

            return i;
        }

        private void QuickSort(ArrayList a, int left, int right)
        {
            if (a.Count < 1)
            {
                return;
            }
            int index = Partition(a, left, right);
            if (left < index - 1)
                QuickSort(a, left, index - 1);
            if (index < right)
                QuickSort(a, index, right);
        }

        // Merge Sort
        //private void SortEntities(ArrayList a, ArrayList temp, int left, int right)
        //{
        //    if (right > left)
        //    {

        //        int mid = (right + left) / 2;
        //        SortEntities(a, temp, left, mid);
        //        SortEntities(a, temp, mid + 1, right);

        //        Merge(a, temp, left, mid + 1, right);
        //    }
        //}

        //private void Merge(ArrayList a, ArrayList temp, int left, int mid, int right)
        //{
        //    int i, leftEnd, elements;
        //    leftEnd = mid - 1;
        //    elements = a.Count;

        //    while((left <= leftEnd) && (mid <= right))
        //    {
        //        Entity leftEnt = (Entity)a[left];
        //        Entity midEnt = (Entity)a[mid];
        //        float leftY = leftEnt.Get<TransformPart>().GetPositionY;
        //        float midY = midEnt.Get<TransformPart>().GetPositionY;
        //        if (leftY <= midY)
        //        {
        //            temp.Add(a[left]);
        //            left = left + 1;
        //        }
        //        else
        //        {
        //            temp.Add(a[mid]);
        //            mid += 1;
        //        }
        //    }

        //    while (left <= leftEnd)
        //    {
        //        temp.Add(a[left]);
        //        left += 1;
        //    }
        //    while (mid <= right)
        //    {
        //        temp.Add(a[mid]);
        //        mid += 1;
        //    }

        //    a.Clear();
        //    for (i = 0; i <= elements-2; i++)
        //    {
        //        a.Add(temp[i]);
        //    }
        //} 
    }
}
