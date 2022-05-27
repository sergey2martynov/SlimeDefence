using UnityEngine;

public static class RandomPositionFinder
{
    private static int _spawnCount;
    
    public static Transform FindRandomPosition(Transform transform, Transform player, int frontBound, int abackBound,  int leftBound, int rightBound)
    {
        Transform vector = transform;
        
        if (_spawnCount == 0)
        {
            vector.position = new Vector3(
                0,
                0,
                Random.Range(0, 2) == 0 ? Random.Range(frontBound -2f, frontBound +2f) : Random.Range(abackBound-2, abackBound +2));
            
            _spawnCount++;
        }
        else if (_spawnCount == 1)
        {
            vector.position = new Vector3(
                Random.Range(0, 2) == 0 ? Random.Range(rightBound -2, rightBound +2) : Random.Range(leftBound -2, leftBound +2),
                0,
                0);
            
            _spawnCount++;
        }
        else
        {
            vector.position = new Vector3(
                Random.Range(0, 2) == 0 ? Random.Range(rightBound -2, rightBound +2) : Random.Range(leftBound -2, leftBound +2),
                0,
                Random.Range(0, 2) == 0 ? Random.Range(frontBound -2f, frontBound +2f) : Random.Range(abackBound-2, abackBound +2));

            _spawnCount = 0;
        }

        vector.position += player.position;

        return vector;
    }
}
