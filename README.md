Excel to SQLite
=
CUI 기반의 Excel to SQLite 변환 프로그램

> **경고**
>
> - 아직 정상적으로 완성된 작업이 아님을 먼저 알립니다.



> **사용방법**
>
>> **명령 인수**
>>  
>> - files : 파일을 대상으로 함
>> - directorys : 폴더를 대상으로 함 ( 폴더 내 xls, xlsx 파일을 확인하여 순회처리 )
>
> <명령 인수> <대상 경로> .. 같이 사용 할 수 있음.
>

> **주의사항**
>
> 엑셀 시트명의 처음 글자가 영문 이외 인 경우 정상적으로 동작하지 않음

>
> **사용 예제**
>
> files "C:\Example\Directory\1.xls" "C:\Example\Directory\2.xlsx" "C:\Example\Directory\3.xls" "C:\Example\Directory\4.xls"
> 
> - 총 4개의 파일을 변환하는 동작
>
> directorys "C:\Example\Directory" "C:\Example\Directory2"
>
> - 총 2개 폴더 내부의 파일들을 변환하는 동작


> 버전 정보
>
>> 0.0.2 
>>
>> - 폴더 순회기능 추가
>> - '와 "처리 및 특수문자 처리
>
>> 0.0.1
>> 
>> - 최소 기능이 구현됨