USE CRM
MERGE INTO DMDIEM t1
      USING (
			 SELECT '2302' AS [MACN]
                  ,[LOAIKH]
				  ,[MACT]
				  ,'01/01/2017' AS [NgayBDHL]
				  ,[NgayHetHL]
				  ,[DIEM]
			  FROM [DMDIEM] 
			  WHERE MACN = '2300'
			 ) t2
      ON t1.MACN = t2.MACN
WHEN NOT MATCHED THEN
INSERT (
		MACN
		,LOAIKH
		,MACT
		,NgayBDHL
		,NgayHetHL
		,DIEM
	  ) 
VALUES (
		t2.MACN
		,t2.LOAIKH
		,t2.MACT
		,t2.NgayBDHL
		,t2.NgayHetHL
		,t2.DIEM
		);