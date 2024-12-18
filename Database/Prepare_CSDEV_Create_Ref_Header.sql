begin tran
--commit
--rollback
insert into PS_SIS_IC_STDRH
select EMPLID, SIS_INTPART_ID, 'PX', 'hendryw', GETDATE(), '', NULL
from PS_SIS_CS_PARTIC_V
where EMPLID ='01283196'