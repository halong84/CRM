USE CRM
MERGE INTO DMDIEMXL t1
      USING (
			 SELECT '2308' AS [MACN]
                  ,[LOAIKH]
				  ,[MALOAI]
				  ,'01/01/2017' AS [NGAYBD]
				  ,[NGAYKT]
				  ,[DIEM]
			  FROM [DMDIEMXL] 
			  WHERE MACN = '2300'
			 ) t2
      ON t1.MACN = t2.MACN
WHEN NOT MATCHED THEN
INSERT (
		MACN
		,LOAIKH
		,MALOAI
		,NGAYBD
		,NGAYKT
		,DIEM
	  ) 
VALUES (
		t2.MACN
		,t2.LOAIKH
		,t2.MALOAI
		,t2.NGAYBD
		,t2.NGAYKT
		,t2.DIEM
		);