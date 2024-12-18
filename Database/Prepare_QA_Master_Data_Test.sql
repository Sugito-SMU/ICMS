BEGIN TRAN
-- COMMIT
-- ROLLBACK

INSERT [dbo].[PS_SIS_IC_LOS_VW] ([SIS_LOS_ID], [SIS_INTERN_TYPE], [DESCR50], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) VALUES (1, N'BA', N'Internship Learning Objective Set', N'A', N'DATA_IMPORT', CAST(N'2019-03-28 15:28:25.187' AS DateTime), N'DATA_IMPORT', CAST(N'2019-03-28 15:28:25.187' AS DateTime))
INSERT [dbo].[PS_SIS_IC_LOS_VW] ([SIS_LOS_ID], [SIS_INTERN_TYPE], [DESCR50], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) VALUES (2, N'CS', N'Community Service Learning Objective Set', N'A', N'DATA_IMPORT', CAST(N'2019-03-28 15:28:25.187' AS DateTime), N'DATA_IMPORT', CAST(N'2019-03-28 15:28:25.187' AS DateTime))
GO


INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (1, 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Apply knowledge, skills and abilities to address challenges in the workplace and to enhance understanding of the profession', 'Please contact your career coach should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (2, 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Produce a record of work experiences relevant to career aspiration or goals', 'Please contact your career coach should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (3, 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Demonstrate ability to solve problems of variable levels of complexity', 'Please contact your career coach should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (4, 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Generate buy-in from team for your initiatives suggested', 'Please contact your career coach should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (5, 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Communicate effectively in oral and written forms with different stakeholders as needed', 'Please contact your career coach should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (6, 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Assess potential ethical conflicts in  personal, professional and societal settings', 'Please contact your career coach should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (7, 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Demonstrate a sense of responsibility for the broader impact of individual and collective actions', 'Please contact your career coach should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (8, 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Assimilate work habits and attitudes necessary for job success', 'Please contact your career coach should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (9, 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Commit to personal development and lifelong learning', 'Please contact your career coach should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (10, 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Develop a positive mindset to overcome setbacks in the face of disruption and challenges', 'Please contact your career coach should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (11, 2, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Analyse the root causes of social issues faced by the community', 'Please contact your project leader or email <a href=\"mailto:commsvcs@smu.edu.sg\">C4SR</a> should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (12, 2, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Demonstrate the ability to solve problems of varying levels of complexity', 'Please contact your project leader or email <a href=\"mailto:commsvcs@smu.edu.sg\">C4SR</a> should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (13, 2, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Demonstrate the ability to recognise and leverage on strengths of others to achieve shared goals', 'Please contact your project leader or email <a href=\"mailto:commsvcs@smu.edu.sg\">C4SR</a> should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (14, 2, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Demonstrate the ability to communicate effectively with different stakeholders to address a problem/issue', 'Please contact your project leader or email <a href=\"mailto:commsvcs@smu.edu.sg\">C4SR</a> should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (15, 2, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Display sensitivity towards individual and cultural differences and respect diverse perspectives of others', 'Please contact your project leader or email <a href=\"mailto:commsvcs@smu.edu.sg\">C4SR</a> should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (16, 2, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Assess potential ethical conflicts in  personal, professional and societal settings', 'Please contact your project leader or email <a href=\"mailto:commsvcs@smu.edu.sg\">C4SR</a> should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (17, 2, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Demonstrate commitment to act responsibly to address social concerns', 'Please contact your project leader or email <a href=\"mailto:commsvcs@smu.edu.sg\">C4SR</a> should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (18, 2, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Demonstrate initiative to advance one''s skills and knowledge to explore and expand one’s own learning', 'Please contact your project leader or email <a href=\"mailto:commsvcs@smu.edu.sg\">C4SR</a> should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (19, 2, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Apply self-appraisal and reflective thinking in the learning process', 'Please contact your project leader or email <a href=\"mailto:commsvcs@smu.edu.sg\">C4SR</a> should you require any assistance.')
INSERT INTO PS_SIS_IC_LO_VW ([SIS_LO_ID], [SIS_LOS_ID], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_LO_TEXT], [SIS_LO_INSTR]) values (20, 2, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE(), 'Demonstrate ability to persevere through challenging circumstances', 'Please contact your project leader or email <a href=\"mailto:commsvcs@smu.edu.sg\">C4SR</a> should you require any assistance.')
GO

INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (1, 1, 'Disciplinary and multidisciplinary knowledge', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (2, 2, 'Disciplinary and multidisciplinary knowledge', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (3, 3, 'Intellectual and creative skills - Critical thinking and problem solving', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (4, 4, 'Interpersonal skills - Collaboration and leadership', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (5, 5, 'Interpersonal skills - Communication', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (6, 6, 'Global citizenship - Ethics and social responsibility', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (7, 7, 'Global citizenship - Ethics and social responsibility', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (8, 8, 'Personal mastery - Self-directedness and meta-learning', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (9, 9, 'Personal mastery - Self-directedness and meta-learning', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (10, 10, 'Personal mastery - Resilience and positivity', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (11, 11, 'Intellectual and creative skills - Critical thinking and problem solving', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (12, 12, 'Intellectual and creative skills - Critical thinking and problem solving', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (13, 13, 'Interpersonal skills - Collaboration and leadership', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (14, 14, 'Interpersonal skills - Communication', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (15, 15, 'Global citizenship - Intercultural understanding and sensitivity ', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (16, 16, 'Global citizenship - Ethics and social responsibility', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (17, 17, 'Global citizenship - Ethics and social responsibility', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (18, 18, 'Personal mastery - Self-directedness and meta-learning', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (19, 19, 'Personal mastery - Self-directedness and meta-learning', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
INSERT INTO PS_SIS_IC_LI_VW ([SIS_LI_ID], [SIS_LO_ID], [SIS_LI_TEXT], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM]) values (20, 20, 'Personal mastery - Resilience and positivity', 1, 'A', 'DATA_IMPORT', GETDATE(), 'DATA_IMPORT', GETDATE())
GO


CREATE TABLE #temp1(
	[SIS_LOQ_ID] [int] NOT NULL IDENTITY,
	[SIS_LO_ID] [int] NOT NULL,
	[SIS_R_STATUS] [nvarchar](2) NOT NULL,
	[SEQNUM] [int] NOT NULL,
	[SIS_LOQ_TEXT] [nvarchar](max) NULL
)

INSERT INTO #temp1 ([SIS_LO_ID], [SIS_R_STATUS], [SEQNUM], [SIS_LOQ_TEXT])
SELECT a.[SIS_LO_ID], b.[SIS_R_STATUS], b.[SEQNUM], b.[SIS_LOQ_TEXT]
FROM PS_SIS_IC_LO_VW a,
(
select 'PR' as [SIS_R_STATUS], 1 as [SEQNUM], 'How you would achieve the learning objective?' as [SIS_LOQ_TEXT] union
select 'PR' as [SIS_R_STATUS], 2 as [SEQNUM], 'How would success look like?' as [SIS_LOQ_TEXT] union
select 'MI' as [SIS_R_STATUS], 1 as [SEQNUM], 'Why Not?' as [SIS_LOQ_TEXT] union
select 'PO' as [SIS_R_STATUS], 1 as [SEQNUM], 'Why Not?' as [SIS_LOQ_TEXT]
) b
order by a.[SIS_LO_ID], b.[SIS_R_STATUS], b.[SEQNUM]

insert into [PS_SIS_IC_LOQ_VW] (
[SIS_LOQ_ID]
      ,[SIS_LO_ID]
      ,[SIS_R_STATUS]
      ,[SIS_INPUT_TYPE]
      ,[SEQNUM]
      ,[EFF_STATUS]
      ,[CREATEOPRID]
      ,[CREATED_DTTM]
      ,[LAST_UPDT_OPRID]
      ,[LAST_UPDT_DTTM]
      ,[SIS_LOQ_TEXT]
)
select [SIS_LOQ_ID]
      ,[SIS_LO_ID]
      ,[SIS_R_STATUS]
      ,'TX' as [SIS_INPUT_TYPE]
      ,[SEQNUM]
      ,'A' as [EFF_STATUS]
      ,'DATA_IMPORT' as [CREATEOPRID]
      ,getdate() as [CREATED_DTTM]
      ,'DATA_IMPORT' as [LAST_UPDT_OPRID]
      ,getdate() as [LAST_UPDT_DTTM]
      ,[SIS_LOQ_TEXT]
from #temp1

GO

INSERT [dbo].[PS_SIS_IC_RS_VW] ([SIS_RS_ID], [SIS_INTERN_TYPE], [DESCR50], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_POST_HEADER]) VALUES (1, N'BA', N'Internship Reflection Set', N'DATA_IMPORT', CAST(N'2019-03-28 15:31:18.580' AS DateTime), N'DATA_IMPORT', CAST(N'2019-03-28 15:31:18.580' AS DateTime), N'Congratulations on completing this internship!<br><br>Please complete the questions below for your reflection report.')
INSERT [dbo].[PS_SIS_IC_RS_VW] ([SIS_RS_ID], [SIS_INTERN_TYPE], [DESCR50], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_POST_HEADER]) VALUES (2, N'CS', N'Community Service Reflection Set', N'DATA_IMPORT', CAST(N'2019-03-28 15:31:18.580' AS DateTime), N'DATA_IMPORT', CAST(N'2019-03-28 15:31:18.580' AS DateTime), N'Congratulations on completing this Community Service Project!<br><br>Please complete the questions below for your reflection report.')
GO

INSERT [dbo].[PS_SIS_IC_RQ_VW] ([SIS_RQ_ID], [SIS_RS_ID], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_RQ_TEXT], [SIS_RQ_GUIDE]) VALUES (1, 1, 1, N'A', N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'What?', N'Report on your internship project objectively<br>How might you describe your internship experience? Was it a fulfilling one?<br>What were some of the activities you undertook during your work there that helped you learn more about the company, their culture and/or the industry?<br>What specific technical and soft skills have you learnt and applied?')
INSERT [dbo].[PS_SIS_IC_RQ_VW] ([SIS_RQ_ID], [SIS_RS_ID], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_RQ_TEXT], [SIS_RQ_GUIDE]) VALUES (2, 1, 2, N'A', N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'So What?', N'Analyse the experience<br>Do you believe you have accomplished your goals set for this internship? Please explain briefly.<br>What kind of resources and support were provided by the company to successfully complete your internship?<br>What did you do that seemed to be effective or ineffective at the workplace?')
INSERT [dbo].[PS_SIS_IC_RQ_VW] ([SIS_RQ_ID], [SIS_RS_ID], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_RQ_TEXT], [SIS_RQ_GUIDE]) VALUES (3, 1, 3, N'A', N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'Now What?', N'Consider the future impact of the experience on you and your career<br>Did this internship align with your long term career goals and how has it helped you with your future plans? What have you learned about yourself in this experience?<br>What kind of skills do you think you need to acquire further to build a career in this business? How do you intend to do so?<br>Do you wish to continue working in the future in this company or industry or have other plans?')
INSERT [dbo].[PS_SIS_IC_RQ_VW] ([SIS_RQ_ID], [SIS_RS_ID], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_RQ_TEXT], [SIS_RQ_GUIDE]) VALUES (4, 2, 1, N'A', N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'What?', N'Report on your community service project objectively<br>What did you observe about the beneficiary community your project was engaging?<br>What issue/needs in the community was being addressed by your project?<br>What specific skills have you used at your community site?')
INSERT [dbo].[PS_SIS_IC_RQ_VW] ([SIS_RQ_ID], [SIS_RS_ID], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_RQ_TEXT], [SIS_RQ_GUIDE]) VALUES (5, 2, 2, N'A', N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'So What?', N'Analyse the experience<br>How was the experience different from what you expected?<br>How did the project address the needs/issues in the community?<br>What did you learn about the people/community?<br>What did you do that seemed to be effective or ineffective in the community?')
INSERT [dbo].[PS_SIS_IC_RQ_VW] ([SIS_RQ_ID], [SIS_RS_ID], [SEQNUM], [EFF_STATUS], [CREATEOPRID], [CREATED_DTTM], [LAST_UPDT_OPRID], [LAST_UPDT_DTTM], [SIS_RQ_TEXT], [SIS_RQ_GUIDE]) VALUES (6, 2, 3, N'A', N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'DATA_IMPORT', CAST(N'2019-03-28 15:30:30.037' AS DateTime), N'Now What?', N'Consider the future impact of the experience on you and the community<br>What have you learned about yourself, the community, and the social issue in this experience? How may you apply your learning to other contexts?<br>If you could do the project again, what would you do differently?<br>What follow-up is needed to address the challenges/difficulties faced by the community?<br>Do you have any plans to continue your involvement with this community or social issue? How do you intend to do so?')
GO

