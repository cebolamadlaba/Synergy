


insert into [rtblChannelType] (Description,IsActive,StandardPricingTable)
values('ATM', 1 , 300)

declare @fkrtblChannelTypeImport int = SCOPE_IDENTITY()

insert into [rtblChannelTypeImport] (fkChannelTypeId,ImportFileChannel)
values (@fkrtblChannelTypeImport,122)

