
declare @fkChannelTypeId int

if not exists(select pkChannelTypeId from [rtblChannelType] where Description = 'ATM')
begin
	insert into [rtblChannelType] (Description,IsActive,StandardPricingTable)
	values('ATM', 1 , 300)
end

select @fkChannelTypeId = pkChannelTypeId from [rtblChannelType]
where Description = 'ATM' --The description should alwais be unique 

if not exists(select pkChannelTypeImportId from [rtblChannelTypeImport] where fkChannelTypeId = @fkChannelTypeId)
begin
	insert into [rtblChannelTypeImport] (fkChannelTypeId,ImportFileChannel)
	values (@fkChannelTypeId,122)
end
