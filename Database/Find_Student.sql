select top 10 d.OPRID, *
from [dbo].[PS_SIS_STDDEGR_VW] a
inner join [dbo].[PS_SIS_CS_PARTIC_V] b on b.EMPLID = a.EMPLID
inner join [dbo].[PS_SIS_IC_PRGRD_VW] c on c.EMPLID = a.EMPLID and c.ACAD_PROG = a.ACAD_PROG
inner join [dbo].[PS_SIS_STDBIO_VW] d on d.EMPLID = a.EMPLID
where a.ADMIT_TERM = '1910'
and a.ACAD_CAREER = 'UGRD'
and a.PROG_STATUS = 'AC'

