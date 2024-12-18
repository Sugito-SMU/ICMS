--select top 10 * from PS_SIS_CS_PARTIC_V
--order by START_DATE desc

--select top 10 a.* 
--from PS_SIS_IC_PRGRD_VW a
--inner join [dbo].[PS_SIS_STDDEGR_VW] b on b.EMPLID = a.EMPLID and b.ACAD_CAREER = 'UGRD' and b.PROG_STATUS = 'AC'
--where SIS_INTERN_TYPE = 'BA' and SIS_UNITS_COMP is not null

insert into [PS_SIS_IC_STDRH]
(
	 [EMPLID]
      ,[SIS_INTPART_ID]
      ,[SIS_R_STATUS]
      ,[CREATEOPRID]
      ,[CREATED_DTTM]
      ,[LAST_UPDT_OPRID]
      ,[LAST_UPDT_DTTM]
)
select distinct
      a.[EMPLID]
      ,b.[SIS_INTPART_ID]
      ,'PX' [SIS_R_STATUS]
      ,c.OPRID [CREATEOPRID]
      ,getdate() [CREATED_DTTM]
      ,c.OPRID [LAST_UPDT_OPRID]
      ,getdate() [LAST_UPDT_DTTM]
from PS_SIS_IC_PRGRD_VW a
inner join [dbo].[PS_SIS_CS_PARTIC_V] b on b.EMPLID = a.EMPLID and b.SIS_INTERN_TYPE = a.SIS_INTERN_TYPE
inner join [dbo].[PS_SIS_STDBIO_VW] c on c.EMPLID = a.EMPLID
where a.EMPLID = '01202347'
GO


