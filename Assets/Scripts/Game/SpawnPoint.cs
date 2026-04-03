using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPoint : MonoBehaviour {
    private List<Transform> _spawnPoints;

    [SerializeField]
    private List<EnemyController> _enemyList;
    private List<EnemyController> _aliveEnemys;

    private float _spawnMaxDistFromPoint = 2f;

    private float _spawnDelay = 2f;
    private float _lastSpawnTime;

    private MenuUI _menuUI;

    private void Awake() {
        _menuUI = GameObject.FindWithTag(Tags.Menu).GetComponent<MenuUI>();
        _spawnPoints = new List<Transform>();
        _aliveEnemys = new List<EnemyController>();

        for (int i = 0; i < transform.childCount; i++) {
            _spawnPoints.Add(transform.GetChild(i));
        }

        _lastSpawnTime = float.MinValue;
    }

    private void Update() {
        if (Time.time >= _lastSpawnTime + _spawnDelay) {
            _lastSpawnTime = Time.time;
            Spawn(1);
        }
    }

    public void Spawn(int spawnCount) {
        for (int i = 0; i < spawnCount; i++) {
            int pointNum = Random.Range(0, _spawnPoints.Count);
            int enemyNum = Random.Range(0, _enemyList.Count);

            if (NavMesh.SamplePosition(_spawnPoints[pointNum].position, out NavMeshHit hit, _spawnMaxDistFromPoint, NavMesh.AllAreas)) { 
                // 생성
                var enemy = Instantiate(_enemyList[enemyNum], hit.position, Quaternion.identity);

                // 리스트에 추가 및 이후 이벤트 기반 삭제 처리
                _aliveEnemys.Add(enemy);
                enemy.AddToOnDead(() => _aliveEnemys.Remove(enemy));

                // 볼륨 변경 처리 및 이벤트 기반 삭제 처리
                _menuUI.OnEffectVolumeChange.AddListener(enemy.SetEffectVolume);
                enemy.AddToOnDead(() => _menuUI.OnEffectVolumeChange.RemoveListener(enemy.SetEffectVolume));

                Debug.Log($"SpawnPoint적 스폰 : 남은 적 수 : {_aliveEnemys.Count}");
            }
        }
    }
}