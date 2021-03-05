
declare @fkChannelTypeId int

insert into [rtblChannelType] (Description,IsActive,StandardPricingTable)
values('ATM', 1 , 300)

 select @fkChannelTypeId = pkChannelTypeId from [rtblChannelType]
 where Description = 'ATM' --The description should alwais be unique 

insert into [rtblChannelTypeImport] (fkChannelTypeId,ImportFileChannel)
values (@fkChannelTypeId,122)

