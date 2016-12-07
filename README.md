Excel to SQLite
=
## GUI, CUI 기반의 Excel to SQLite 변환 프로그램

### **확인된 문제**
- Excel의 Sheet 명의 처음 글자가 영문 이외 인 경우 변환 실패
- Excel의 셀을 지울 때 행을 선택하여 제거하지 않으면 빈 값을 참조하여 변환 실패
***

### **사용방법**

> **GUI**
>
> 대상 파일들을 Drag & Drop하면 Excel에서 SQLite 파일로 변환, 같은 경로의 같은 이름으로 저장됨
> - **체크 박스** : SQLite에 대응하는 C# 코드 생성기능 ( 파일 명을 namespace로 Sheet 명을 클래스명으로 생성 )

> **명령 인수**
>  
> - **files** : 파일을 대상으로 함
> - **directorys** : 폴더를 대상으로 함 ( 폴더 내 xls, xlsx 파일을 확인하여 순회처리 )

> **엑셀 파일규칙**
>  
> - **Sheet 명** : Table 명
> - **1행** : Column 명
> - **2행** : 해당 Column의 Type
>
>> **SQLite 자료형** (동적 타입이므로 꼭 맞추지 않아도 동작)
>>
>> - **INTEGER** : 1,2,3,4,6,8bytes의 정수값
>> - **REAL** : 8bytes의 부동소수점값
>> - **TEXT** : UTF-8, UTF-16BE, UTF-16LE인코딩의 문자열
>> - **BLOB** : 입력된 그대로 저장, 바이너리 파일 등
>> - **NULL** : 널값
>
> 위 규칙으로 Excel 파일에서 SQLite 파일로 변환


***


### **사용 예제**

> **files "C:\Example\Directory\1.xls" "C:\Example\Directory\2.xlsx" "C:\Example\Directory\3.xls" "C:\Example\Directory\4.xls"**
> 
> - 총 4개의 파일을 변환하는 동작

> **directorys "C:\Example\Directory" "C:\Example\Directory2"**
>
> - 총 2개 폴더 내부의 파일들을 변환하는 동작

***

### **버전 정보**

 [Release](https://github.com/cr545l/xlsToSqliteConverter/tree/master/Release)
 
> **0.0.5b**
>
> - SQLite 데이터에 대응하는 C# 코드 생성기능 추가
> - 컨버팅 성능 대폭향상
> - 정상적인 Type인지 검사처리

> **0.0.4b**
>
> - GUI 추가
> - 기존 Converter에 Progress를 얻을 수 있는 callback 추가

> **0.0.3b**
>
> - 프로그램 종료 시 남아있던 자원 반환처리
> - Column 타입을 두번째열 데이터 타입이 아닌, **두번째열에 명시적으로 정의한 값**으로 사용하도록 변경

> **0.0.2**
>
> - 폴더 순회기능 추가
> - '와 "처리 및 특수문자 처리

> **0.0.1**
> 
> - 최소 기능이 구현됨