Excel to SQLite
=

[![](https://img.shields.io/github/downloads/cr545l/xlsToSqliteConverter/total.svg)](https://github.com/cr545l/xlsToSqliteConverter/releases)

GUI, CUI 기반의 Excel to SQLite 변환 프로그램

![Example](https://github.com/cr545l/xlsToSqliteConverter/blob/master/Example/Example.gif?raw=true)

---

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
> - **2행** : Column의 Type (하단 자료형 참고)
> - **3행** : Column의 설명문
>
>> **SQLite 자료형**
>>
>> - **INTEGER** : 1,2,3,4,6,8bytes의 정수값
>> - **REAL** : 8bytes의 부동소수점값
>> - **TEXT** : UTF-8, UTF-16BE, UTF-16LE인코딩의 문자열
>> - **BLOB** : 입력된 그대로 저장, 바이너리 파일 등
>> - **NULL** : 널값
>>
>> **정의된 자료형**
>>
>> - **integer primary key** : 기본 키
>> - **int, bigint** : 1,2,3,4,6,8bytes의 정수값
>> - **real, float** : 8bytes의 부동소수점값
>> - **text, string, varchar** : UTF-8, UTF-16BE, UTF-16LE인코딩의 문자열
>
> 위 규칙으로 Excel 파일에서 SQLite 파일로 변환

---

### **사용 예제**

> **files "C:\Example\Directory\1.xls" "C:\Example\Directory\2.xlsx" "C:\Example\Directory\3.xls" "C:\Example\Directory\4.xls"**
> 
> - 총 4개의 파일을 변환하는 동작

> **directorys "C:\Example\Directory" "C:\Example\Directory2"**
>
> - 총 2개 폴더 내부의 파일들을 변환하는 동작

---

### **확인된 문제**
- Excel의 Sheet 명의 처음 글자가 영문 이외 인 경우 변환 실패
- Excel의 셀을 지울 때 행을 선택하여 제거하지 않으면 빈 값을 참조하여 변환 실패