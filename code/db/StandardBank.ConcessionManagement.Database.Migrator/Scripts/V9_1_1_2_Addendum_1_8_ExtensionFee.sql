create table rtblExtensionFee(
pkExtensionFeeId int primary key identity(1,1),
ExtensionFee decimal(18,3) not null,
IsActive bit not null
)

insert into rtblExtensionFee
values('0.63',1)

alter table tblConcessionLending
add ExtensionFee decimal(18,3) null