using System;
using System.Collections.Generic;

[Serializable]
public class EnemyManagerData
{
    public string enemyName;
    public int enemyCount;
    public List<bool> enemieStateList = new();

    public EnemyManagerData(string enemyName, int enemyCount, int count)
    {
        this.enemyName = enemyName;
        this.enemyCount = enemyCount;
        GetEnemyList(count);
    }

    public void GetEnemyList(int count)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            if (i == count)
            {
                enemieStateList.Add(false);
                continue;
            }
            enemieStateList.Add(true);
        }
    }
}
