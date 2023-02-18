using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingService<T> : MonoSingletonGeneric<PoolingService<T>>
    where T : class
{
    private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();
    public virtual T GetItem(Enum _itemType)
    {
        var itemType = _itemType;
        PooledItem<T> item = null;

        if (pooledItems.Count > 0)
        {
            foreach (PooledItem<T> i in pooledItems)
            {
                if (!i.IsUsed)
                {
                    if (i.Itemtype.ToString() == itemType.ToString())
                    {
                        item = i;
                    }
                }
            }
            if (item != null)
            {
                item.IsUsed = true;
                return item.Item;
            }
        }
        return CreateNewPooledItem(itemType);
    }

    private T CreateNewPooledItem(Enum itemType)
    {
        PooledItem<T> pooledItem = new PooledItem<T>();
        pooledItem.Item = CreateItem();
        pooledItem.IsUsed = true;
        pooledItem.Itemtype = itemType;
        pooledItems.Add(pooledItem);
        return pooledItem.Item;
    }

    public virtual void ReturnItem(T item)
    {
        PooledItem<T> pooledItem = pooledItems.Find(i => i.Item.Equals(item));
        pooledItem.IsUsed = false;
    }

    protected virtual T CreateItem()
    {
        return (T)null;
    }

    private class PooledItem<T>
    {
        public T Item;
        public bool IsUsed;
        public Enum Itemtype;
    }
}