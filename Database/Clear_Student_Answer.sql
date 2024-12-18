
declare @EMPLID_LIST table (EMPLID nvarchar(10))

insert into @EMPLID_LIST values ('XXXXXXXX')

begin tran

delete from [dbo].[PS_SIS_IC_STDRLOA] where EMPLID in (select EMPLID from @EMPLID_LIST) 
delete from [dbo].[PS_SIS_IC_STDRLO] where EMPLID in (select EMPLID from @EMPLID_LIST)
delete from [dbo].[PS_SIS_IC_STDRA] where EMPLID in (select EMPLID from @EMPLID_LIST)
delete from [dbo].[PS_SIS_IC_STDRH] where EMPLID in (select EMPLID from @EMPLID_LIST)

--commit
--rollback


delete from [dbo].[PS_SIS_IC_STDRLOA] 
delete from [dbo].[PS_SIS_IC_STDRLO]
delete from [dbo].[PS_SIS_IC_STDRA] 
delete from [dbo].[PS_SIS_IC_STDRH]

delete [dbo].[PS_SIS_IC_SRLOA_VW] where EMPLID in (select EMPLID from @EMPLID_LIST)
delete [dbo].[PS_SIS_IC_SRLO_VW] where EMPLID in (select EMPLID from @EMPLID_LIST)
delete [dbo].PS_SIS_IC_STDRA_VW where EMPLID in (select EMPLID from @EMPLID_LIST)
delete [dbo].[PS_SIS_IC_STDRH_VW] where EMPLID in (select EMPLID from @EMPLID_LIST)

--commit
--rollback
go