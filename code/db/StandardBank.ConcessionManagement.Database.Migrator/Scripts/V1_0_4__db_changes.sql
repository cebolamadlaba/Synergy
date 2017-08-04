ALTER TABLE [dbo].[tblConcessionCondition]
ADD [fkPeriodTypeId] int NULL

GO

ALTER TABLE [dbo].[tblConcessionCondition]
ADD [fkPeriodId] int NULL

GO

ALTER TABLE [dbo].[tblConcessionCondition]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionCondition_rtblPeriod] FOREIGN KEY([fkPeriodId])
REFERENCES [dbo].[rtblPeriod] ([pkPeriodId])
GO

ALTER TABLE [dbo].[tblConcessionCondition] CHECK CONSTRAINT [FK_tblConcessionCondition_rtblPeriod]
GO

ALTER TABLE [dbo].[tblConcessionCondition]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionCondition_rtblPeriodType] FOREIGN KEY([fkPeriodTypeId])
REFERENCES [dbo].[rtblPeriodType] ([pkPeriodTypeId])
GO

ALTER TABLE [dbo].[tblConcessionCondition] CHECK CONSTRAINT [FK_tblConcessionCondition_rtblPeriodType]
GO

