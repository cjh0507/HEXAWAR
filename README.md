# HEXAWAR (Madcamp2020s)
<del>핵싸워</del>
* Project HEXAWAR
* Contributors : 이현호 최진혁 조민규
* 7/29 ~ 8/7

## 요약
-----------
> QE를 통한 회전, WASD를 통한 이동.

> 여러 기능셀들과 구조셀들을 붙여가며 적을 제거하는 게임


## Cell 종류

1. Core Cell : 평범한 이동, 공격의 기능을 가진 셀

2. Structure Cell : GunCell, BoosterCell
* GunCell : GunCell, ShotgunCell, SniperCell, SubmachinegunCell
* BoosterCell : AccelerationCell, RotationCell
3. Feature Cell : HardCell, OverclockCell, FastCell, LiteCell, RepairCell, GrowCell

## Boss 종류

1. FidgetKing : <del>양심없는 몹</del>

2. SpinnerKing : <del>위에 있는 놈 친구</del>

3. B.F Sniper : <del>크고 아름다운 Wls</del>

## 개발과정
> 기본 구조인 Cell을 gameObject화 시켜서 면을 0부터 5로 할당. 각 면에 다른 셀의 면이 접근하면 collider와 vector3 계산을 통해 정해진 위치에 결합. rayCasting을 통해 인접 셀들의 리스트를 adjacentCells로 확인하여 스탯을 변경시킴. Cell 파괴시 인접한 셀들을 다시 확인하는 함수를 call. 많은 셀들이 한번에 같은 함수를 사용하여 Concurrency 문제가 발생해 coroutine화 시켜서 해결함

> 
